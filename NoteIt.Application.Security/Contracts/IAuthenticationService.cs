using NoteIt.Application.Security.Models;

namespace NoteIt.Application.Security.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(string storageName, string password);
    }
}
