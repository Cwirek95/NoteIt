using AutoFixture;
using AutoFixture.AutoMoq;
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
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var event1 = fixture.Create<Event>();
            var event2 = fixture.Create<Event>();
            var event3 = fixture.Create<Event>();
            var event4 = fixture.Create<Event>();
            var event5 = fixture.Create<Event>();

            events.Add(event1);
            events.Add(event2);
            events.Add(event3);
            events.Add(event4);
            events.Add(event5);

            return events;
        }
    }
}
