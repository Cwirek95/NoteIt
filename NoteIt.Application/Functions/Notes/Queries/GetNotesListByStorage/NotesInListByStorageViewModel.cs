namespace NoteIt.Application.Functions.Notes.Queries.GetNotesListByStorage
{
    public class NotesInListByStorageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Content { get; set; }
        public bool IsImportant { get; set; }
    }
}