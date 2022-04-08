using System.Security.Claims;

namespace NoteIt.Application.Security.Contracts
{
    public interface ICurrentStorageService
    {
        ClaimsPrincipal User { get; }
        string? Storage { get; }
    }
}
