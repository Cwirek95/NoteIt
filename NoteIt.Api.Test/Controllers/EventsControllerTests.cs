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
using Microsoft.Extensions.DependencyInjection;
using NoteIt.Persistence.EF;
using NoteIt.Domain.Entities;
using System.Threading.Tasks;

namespace NoteIt.Api.Controllers.Tests
{
    public class EventsControllerTests : IDisposable, IClassFixture<TestApplication>
    {
        private readonly HttpClient _client;
        private readonly TestApplication _testApplication;
        private readonly Storage _storage;

        private const string STORAGE_ADDRESS = "storage1";

        public EventsControllerTests(TestApplication testApplication)
        {
            _testApplication = testApplication;
            _client = _testApplication.CreateClient();

            _storage = new Storage()
            {
                Id = Guid.NewGuid(),
                Name = "Storage1",
                AddressName = STORAGE_ADDRESS,
                Password = "12345",
                CreatedAt = DateTimeOffset.Now
            };
            SeedStorage(_storage);
        }

        public void Dispose()
        {
            RemoveStorage(_storage);
        }

        private void SeedStorage(Storage storage)
        {
            var scopeFactory = _testApplication.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            _dbContext.Storages.Add(storage);
            _dbContext.SaveChanges();
        }

        private void RemoveStorage(Storage storage)
        {
            var scopeFactory = _testApplication.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            _dbContext.Storages.Remove(storage);
            _dbContext.SaveChanges();
        }

        private void SeedEvent(Event evt)
        {
            var scopeFactory = _testApplication.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            _dbContext.Events.Add(evt);
            _dbContext.SaveChanges();
        }


        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new CreateEventCommand()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/" + model.StorageAddress + "/Events", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateEventCommand()
            {
                Id = 0,
                Name = "",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/" + model.StorageAddress + "/Events", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact()]
        public async Task GetAllByStorageAddress_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Events/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Detail_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var evt = new Event()
            {
                Id = new Random().Next(),
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageId = Guid.NewGuid()
            };
            SeedEvent(evt);

            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Events/" + evt.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task Detail_ForNonExistingEvent_ReturnNotFoundResponse()
        {
            // Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Events/0");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var evt = new Event()
            {
                Id = new Random().Next(),
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageId = Guid.NewGuid()
            };
            SeedEvent(evt);

            var model = new UpdateEventCommand()
            {
                Id = evt.Id,
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Events/" + model.Id, httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ForNonExistingEvent_ReturnNotFoundResponse()
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
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Events/" + model.Id, httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateVisibility_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var evt = new Event()
            {
                Id = new Random().Next(),
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageId = Guid.NewGuid()
            };
            SeedEvent(evt);

            var model = new ChangeEventVisibilityCommand()
            {
                Id = evt.Id,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Events/" + model.Id + "/visibility", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var evt = new Event()
            {
                Id = new Random().Next(),
                Name = "Test",
                Location = "Location",
                Description = "Desc",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(5),
                StorageId = Guid.NewGuid()
            };
            SeedEvent(evt);

            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.DeleteAsync("/api/" + address + "/Events/" + evt.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingEvent_ReturnNotFoundResponse()
        {
            //Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.DeleteAsync("/api/" + address + "/Events/0");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}