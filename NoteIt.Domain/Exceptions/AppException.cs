namespace NoteIt.Domain.Exceptions
{
    public abstract class AppException : Exception
    {
        public string Title { get; }

        protected AppException(string title, string message)
            : base(message) => Title = title;
    }
}
