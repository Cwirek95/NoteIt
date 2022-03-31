using Moq;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteIt.Application.Test.Mocks.Repositories
{
    public class EventRepositoryMock
    {
        public static Mock<IEventRepository> GetEventRepository()
        {
            var events = GetEvents();
            var mockEventRepository = new Mock<IEventRepository>();

            mockEventRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(events);
            mockEventRepository.Setup(x => x.GetAllAsyncByStorageId(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    var eventsList = events.Where(x => x.StorageId == id).ToList();
                    return eventsList;
                });
            mockEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var evt = events.FirstOrDefault(x => x.Id == id);
                    return evt;
                });
            mockEventRepository.Setup(x => x.AddAsync(It.IsAny<Event>())).ReturnsAsync(
                (Event evt) =>
                {
                    events.Add(evt);
                    return evt;
                });
            mockEventRepository.Setup(x => x.UpdateAsync(It.IsAny<Event>())).Callback<Event>(
                (evt) =>
                {
                    events.RemoveAll(x => x.Id == evt.Id);
                    events.Add(evt);
                });
            mockEventRepository.Setup(x => x.DeleteAsync(It.IsAny<Event>())).Callback<Event>(
                (evt) =>
                {
                    events.Remove(evt);
                });

            return mockEventRepository;
        }

        private static List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();

            var event1 = new Event()
            {
                Id = 1,
                Name = "Event1",
                Location = "Location1",
                Description = "Desc1",
                StartDate = DateTimeOffset.Now.AddMinutes(-5),
                EndDate = DateTimeOffset.Now.AddMinutes(5),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-8),
                IsHidden = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-10),
                LastModified = DateTimeOffset.Now
            };
            var event2 = new Event()
            {
                Id = 2,
                Name = "Event2",
                Location = "Location2",
                Description = "Desc2",
                StartDate = DateTimeOffset.Now.AddMinutes(-5),
                EndDate = DateTimeOffset.Now.AddMinutes(5),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-8),
                IsHidden = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-10),
                LastModified = DateTimeOffset.Now
            };
            var event3 = new Event()
            {
                Id = 3,
                Name = "Event3",
                Location = "Location3",
                Description = "Desc3",
                StartDate = DateTimeOffset.Now.AddMinutes(-5),
                EndDate = DateTimeOffset.Now.AddMinutes(5),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-8),
                IsHidden = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-10),
                LastModified = DateTimeOffset.Now
            };
            var event4 = new Event()
            {
                Id = 4,
                Name = "Event4",
                Location = "Location4",
                Description = "Desc4",
                StartDate = DateTimeOffset.Now.AddMinutes(-5),
                EndDate = DateTimeOffset.Now.AddMinutes(5),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-8),
                IsHidden = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-10),
                LastModified = DateTimeOffset.Now
            };
            var event5 = new Event()
            {
                Id = 5,
                Name = "Event5",
                Location = "Location5",
                Description = "Desc5",
                StartDate = DateTimeOffset.Now.AddMinutes(-5),
                EndDate = DateTimeOffset.Now.AddMinutes(5),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-8),
                IsHidden = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-10),
                LastModified = DateTimeOffset.Now
            };

            events.Add(event1);
            events.Add(event2);
            events.Add(event3);
            events.Add(event4);
            events.Add(event5);

            return events;
        }
    }
}
