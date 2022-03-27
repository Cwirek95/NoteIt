namespace NoteIt.Domain.Entities.Common
{
    public class AuditableEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
