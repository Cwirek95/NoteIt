using NoteIt.Application.Security.Contracts;
using System.Security.Claims;

namespace NoteIt.Api.Services
{
    public class CurrentStorageService : ICurrentStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentStorageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public string? Storage =>
            User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
    }
}
