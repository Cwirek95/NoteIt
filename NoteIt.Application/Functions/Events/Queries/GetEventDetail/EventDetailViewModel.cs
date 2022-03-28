namespace NoteIt.Application.Functions.Events.Queries.GetEventDetail
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? ReminderDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}