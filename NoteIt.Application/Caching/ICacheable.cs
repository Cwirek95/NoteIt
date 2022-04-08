namespace NoteIt.Application.Caching
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }
}
