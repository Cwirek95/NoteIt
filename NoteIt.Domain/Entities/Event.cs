using NoteIt.Domain.Entities.Common;

namespace NoteIt.Domain.Entities
{
    public class Event : AuditableEntity, IEntity<Guid>
    {
        public Event()
        {
            isHidden = false;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? ReminderDate { get; set; }
        public bool isHidden { get; set; }


        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
