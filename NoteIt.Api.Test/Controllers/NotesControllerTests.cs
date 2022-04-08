using Xunit;
using NoteIt.Api.Test;
using System.Net.Http;
using FluentAssertions;
using System.Net;
using NoteIt.Application.Functions.Notes.Commands.CreateNote;
using NoteIt.Api.Test.Helpers;
using NoteIt.Application.Functions.Notes.Commands.UpdateNote;
using NoteIt.Application.Functions.Notes.Commands.ChangeNoteImportance;
using NoteIt.Application.Functions.Notes.Commands.ChangeNoteVisibility;
using NoteIt.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using NoteIt.Persistence.EF;
using System;
using System.Threading.Tasks;

namespace NoteIt.Api.Controllers.Tests
{
    public class NotesControllerTests : IDisposable, IClassFixture<TestApplication>
    {
        private readonly HttpClient _client;
        private readonly TestApplication _testApplication;
        private readonly Storage _storage;

        private const string STORAGE_ADDRESS = "storage1";

        public NotesControllerTests(TestApplication testApplication)
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

        private void SeedNote(Note note)
        {
            var scopeFactory = _testApplication.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new CreateNoteCommand()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/" + model.StorageAddress + "/Notes", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateNoteCommand()
            {
                Id = 9999,
                Name = "",
                Content = "Content",
                IsImportant = false,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/" + model.StorageAddress + "/Notes/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Update_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var note = new Note()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };
            SeedNote(note);

            var model = new UpdateNoteCommand()
            {
                Id = note.Id,
                Name = "Test1",
                Content = "Content",
                IsImportant = false,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Notes/" + model.Id, httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ForNonExistingNote_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateNoteCommand()
            {
                Id = 0,
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Notes/" + model.Id, httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateImportance_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var note = new Note()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };
            SeedNote(note);

            var model = new ChangeNoteImportanceCommand()
            {
                Id = note.Id,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Notes/" + model.Id + "/importance", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdateVisibility_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var note = new Note()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };
            SeedNote(note);

            var model = new ChangeNoteVisibilityCommand()
            {
                Id = note.Id,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Notes/" + model.Id + "/visibility", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact()]
        public async Task GetAllByStorageAddress_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Notes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task Detail_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var note = new Note()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };
            SeedNote(note);

            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Notes/" + note.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task Detail_ForNonExistingNote_ReturnNotFoundResponse()
        {
            //Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Notes/" + 1000000);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var note = new Note()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };
            SeedNote(note);

            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.DeleteAsync("/api/" + address + "/Notes/" + note.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingNote_ReturnNotFoundResponse()
        {
            //Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.DeleteAsync("/api/" + address + "/Notes/" + 0);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}