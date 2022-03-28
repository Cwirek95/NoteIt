namespace NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage
{
    public class EventsInListByStorageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? ReminderDate { get; set; }
    }
}