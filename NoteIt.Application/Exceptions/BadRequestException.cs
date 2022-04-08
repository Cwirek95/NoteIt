using NoteIt.Domain.Exceptions;

namespace NoteIt.Application.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string title, string message) 
            : base("Bad Request", message)
        {
        }
    }
}
