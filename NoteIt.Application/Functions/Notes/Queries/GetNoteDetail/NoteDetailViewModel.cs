namespace NoteIt.Application.Functions.Notes.Queries.GetNoteDetail
{
    public class NoteDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Content { get; set; }
        public bool IsImportant { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}