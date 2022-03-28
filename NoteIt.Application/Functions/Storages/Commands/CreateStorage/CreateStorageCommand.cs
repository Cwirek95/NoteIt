using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Storages.Commands.CreateStorage
{
    public class CreateStorageCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
