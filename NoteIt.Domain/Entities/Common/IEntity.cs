namespace NoteIt.Domain.Entities.Common
{
    public interface IEntity
    {
    }

    public interface IEntity<Tkey> : IEntity
    {
        Tkey Id { get; set; }
    }

}
