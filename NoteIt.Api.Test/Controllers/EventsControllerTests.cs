using Xunit;
using System;
using System.Net.Http;
using NoteIt.Api.Test;
using System.Net;
using FluentAssertions;
using NoteIt.Application.Functions.Events.Commands.CreateEvent;
using NoteIt.Api.Test.Helpers;
using NoteIt.Application.Functions.Events.Commands.UpdateEvent;
using NoteIt.Application.Functions.Events.Commands.ChangeEventVisibility;

namespace NoteIt.Api.Controllers.Tests
{
    public class EventsControllerTests
    {
        private readonly HttpClient _client;
        private readonly TestApplication _testApplication;

        public EventsControllerTests()
        {
            _testApplication = new TestApplication();
            _client = _testApplication.CreateClient();
        }

        [Theory()]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(50)]
        [InlineData(70)]
        public async void Create_ForValidModel_ReturnOkResponse(int id)
        {
            // Arrange
            var model = new CreateEventCommand()
            {
                Id = id,
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageId = Guid.NewGuid()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/Events/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async void Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateEventCommand()
            {
                Id = 90,
                Name = "",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageId = Guid.NewGuid()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/Events/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact()]
        public async void GetAllByStorageId_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync("/api/Events/all/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory()]
        [InlineData(20)]
        [InlineData(50)]
        [InlineData(70)]
        public async void Detail_WithQueryParameters_ReturnOkResponse(int id)
        {
            // Act
            var response = await _client.GetAsync("/api/Events/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async void Detail_ForNonExistingEvent_ReturnNotFoundResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/Events/" + 1000000);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Update_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new UpdateEventCommand()
            {
                Id = 20,
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Events/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void Update_ForNonExistingEvent_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateEventCommand()
            {
                Id = 0,
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Events/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void UpdateVisibility_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new ChangeEventVisibilityCommand()
            {
                Id = 20,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Events/visibility", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory()]
        [InlineData(1)]
        [InlineData(30)]
        public async void Delete_ForValidModel_ReturnOkResponse(int id)
        {
            // Act
            var response = await _client.DeleteAsync("/api/Events/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory()]
        [InlineData(0)]
        [InlineData(1500)]
        public async void Delete_ForNonExistingEvent_ReturnNotFoundResponse(int id)
        {
            // Act
            var response = await _client.DeleteAsync("/api/Events/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}