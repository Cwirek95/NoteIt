using NoteIt.Domain.Entities.Common;

namespace NoteIt.Domain.Entities
{
    public class Contact : AuditableEntity, IEntity<int>
    {
        public Contact()
        {
            IsHidden = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsHidden { get; set; }


        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
