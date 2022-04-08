using NoteIt.Domain.Exceptions;

namespace NoteIt.Application.Security.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message)
            : base("Forbidden", message)
        {
        }
    }
}
