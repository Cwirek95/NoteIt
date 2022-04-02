using Xunit;
using System;
using NoteIt.Api.Test;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NoteIt.Persistence.EF;
using NoteIt.Domain.Entities;
using FluentAssertions;
using System.Net;
using NoteIt.Application.Functions.Notes.Commands.CreateNote;
using NoteIt.Api.Test.Helpers;
using NoteIt.Application.Functions.Notes.Commands.UpdateNote;
using NoteIt.Application.Functions.Notes.Commands.ChangeNoteImportance;
using NoteIt.Application.Functions.Notes.Commands.ChangeNoteVisibility;

namespace NoteIt.Api.Controllers.Tests
{
    public class NotesControllerTests
    {
        private readonly HttpClient _client;
        private readonly TestApplication _testApplication;

        public NotesControllerTests()
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
            var model = new CreateNoteCommand()
            {
                Id = id,
                Name = "Test",
                Content = "Content",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/Notes/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async void Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateNoteCommand()
            {
                Id = 987,
                Name = "",
                Content = "Content",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/Notes/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async void Update_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new UpdateNoteCommand()
            {
                Id = 20,
                Name = "Test",
                Content = "Content",
                IsImportant = false,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Notes/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void Update_ForNonExistingNote_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateNoteCommand()
            {
                Id = 0,
                Name = "Test",
                Content = "Content",
                IsImportant = false,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Notes/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void UpdateImportance_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new ChangeNoteImportanceCommand()
            {
                Id = 20,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Notes/importance", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void UpdateVisibility_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new ChangeNoteVisibilityCommand()
            {
                Id = 20,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Notes/visibility", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact()]
        public async void GetAllByStorageId_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync("/api/Notes/all/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory()]
        [InlineData(50)]
        [InlineData(70)]
        public async void Detail_WithQueryParameters_ReturnOkResponse(int id)
        {
            // Act
            var response = await _client.GetAsync("/api/Notes/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async void Detail_ForNonExistingNote_ReturnNotFoundResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/Notes/" + 1000000);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory()]
        [InlineData(30)]
        public async void Delete_ForValidModel_ReturnOkResponse(int id)
        {
            // Act
            var response = await _client.DeleteAsync("/api/Notes/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory()]
        [InlineData(0)]
        public async void Delete_ForNonExistingNote_ReturnNotFoundResponse(int id)
        {
            // Act
            var response = await _client.DeleteAsync("/api/Notes/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}