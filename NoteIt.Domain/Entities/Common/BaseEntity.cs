namespace NoteIt.Domain.Entities.Common
{
    public abstract class BaseEntity<Tkey> : IEntity<Tkey>
    {
        public Tkey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
