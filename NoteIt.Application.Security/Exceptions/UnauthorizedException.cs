using NoteIt.Domain.Exceptions;

namespace NoteIt.Application.Security.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message)
            : base("Unauthorized", message)
        {
        }
    }
}
