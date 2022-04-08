using MediatR;
using NoteIt.Application.Responses;
using NoteIt.Application.Security.Models;

namespace NoteIt.Application.Functions.Storages.Commands.LogInStorage
{
    public class LogInStorageCommand : IRequest<AuthenticationResponse>
    {
        public string StorageName { get; set; }
        public string Password { get; set; }
    }
}
