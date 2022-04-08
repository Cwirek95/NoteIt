namespace NoteIt.Application.Security.Models
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string StorageName { get; set; }
        public string Token { get; set; }
    }
}
