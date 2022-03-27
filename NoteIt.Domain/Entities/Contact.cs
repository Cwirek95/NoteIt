using NoteIt.Domain.Entities.Common;

namespace NoteIt.Domain.Entities
{
    public class Contact : AuditableEntity, IEntity<Guid>
    {
        public Contact()
        {
            isHidden = false;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public bool isHidden { get; set; }


        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
