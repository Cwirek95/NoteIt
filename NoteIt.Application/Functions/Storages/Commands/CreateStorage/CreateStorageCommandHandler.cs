using AutoMapper;
using MediatR;
using NoteIt.Application.Common;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Exceptions;
using NoteIt.Application.Responses;
using NoteIt.Domain.Entities;

namespace NoteIt.Application.Functions.Storages.Commands.CreateStorage
{
    public class CreateStorageCommandHandler : IRequestHandler<CreateStorageCommand, ICommandResponse>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateStorageCommandHandler(IStorageRepository storageRepository, IMapper mapper)
        {
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
        {
            var existStorage = await _storageRepository.GetByNameAsync(request.Name);
            if (existStorage != null)
                throw new NotFoundException("The Storage with this name is already exist");

            var storage = _mapper.Map<Storage>(request);

            storage.AddressName = ChangeWhitespace.ChangeToDash(request.Name);
            storage.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            storage = await _storageRepository.AddAsync(storage);

            return new CommandResponse(storage.Id);
        }
    }
}
