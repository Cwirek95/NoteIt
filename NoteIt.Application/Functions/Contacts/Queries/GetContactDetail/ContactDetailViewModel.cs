namespace NoteIt.Application.Functions.Contacts.Queries.GetContactDetail
{
    public class ContactDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}