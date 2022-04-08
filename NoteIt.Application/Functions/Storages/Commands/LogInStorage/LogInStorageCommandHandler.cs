using MediatR;
using NoteIt.Application.Security.Contracts;
using NoteIt.Application.Security.Models;

namespace NoteIt.Application.Functions.Storages.Commands.LogInStorage
{
    public class LogInStorageCommandHandler : IRequestHandler<LogInStorageCommand, AuthenticationResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public LogInStorageCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<AuthenticationResponse> Handle(LogInStorageCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.AuthenticateAsync(request.StorageName, request.Password);

            return response;
        }
    }
}
