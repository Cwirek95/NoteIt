using Xunit;
using System;
using NoteIt.Api.Test;
using System.Net.Http;
using NoteIt.Application.Functions.Storages.Commands.CreateStorage;
using NoteIt.Api.Test.Helpers;
using FluentAssertions;
using System.Net;

namespace NoteIt.Api.Controllers.Tests
{
    public class StoragesControllerTests
    {
        private readonly HttpClient _client;
        private readonly TestApplication _testApplication;

        public StoragesControllerTests()
        {
            _testApplication = new TestApplication();
            _client = _testApplication.CreateClient();
        }

        [Fact()]
        public async void Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Name1",
                Password = "Pass12345"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/storages", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async void Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Password = "Pass12345"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/storages", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}