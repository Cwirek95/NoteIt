using NoteIt.Domain.Entities.Common;

namespace NoteIt.Domain.Entities
{
    public class Storage : AuditableEntity, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddressName { get; set; }
        public string Password { get; set; }


        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
