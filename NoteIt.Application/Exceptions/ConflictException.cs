using NoteIt.Domain.Exceptions;

namespace NoteIt.Application.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message)
            : base("Conflict", message)
        {
        }
    }
}
