using NoteIt.Domain.Entities.Common;

namespace NoteIt.Domain.Entities
{
    public class Note : AuditableEntity, IEntity<int>
        {
        public Note()
        {
            IsHidden = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Content { get; set; }
        public bool IsImportant { get; set; }
        public bool IsHidden { get; set; }


        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
