using NoteIt.Domain.Entities.Common;

namespace NoteIt.Domain.Entities
{
    public class Note : AuditableEntity, IEntity<Guid>
    {
        public Note()
        {
            isHidden = false;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Content { get; set; }
        public bool isImportant { get; set; }
        public bool isHidden { get; set; }


        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
