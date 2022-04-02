using Xunit;
using NoteIt.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteIt.Api.Test;
using System.Net.Http;
using NoteIt.Application.Functions.Contacts.Commands.CreateContact;
using NoteIt.Api.Test.Helpers;
using System.Net;
using FluentAssertions;
using NoteIt.Application.Functions.Contacts.Commands.UpdateContact;
using NoteIt.Application.Functions.Contacts.Commands.ChangeContactVisibility;

namespace NoteIt.Api.Controllers.Tests
{
    public class ContactsControllerTests
    {
        private readonly HttpClient _client;
        private readonly TestApplication _testApplication;

        public ContactsControllerTests()
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
            var model = new CreateContactCommand()
            {
                Id = id,
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageId = Guid.NewGuid()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/Contacts/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async void Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateContactCommand()
            {
                Id = 100,
                Name = "",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageId = Guid.NewGuid()
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/Contacts/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact()]
        public async void GetAllByStorageId_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync("/api/Contacts/all/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory()]
        [InlineData(50)]
        [InlineData(70)]
        public async void Detail_WithQueryParameters_ReturnOkResponse(int id)
        {
            // Act
            var response = await _client.GetAsync("/api/Contacts/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async void Detail_ForNonExistingContacts_ReturnNotFoundResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/Contacts/" + 1000000);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Update_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new UpdateContactCommand()
            {
                Id = 20,
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Contacts/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void Update_ForNonExistingContact_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateContactCommand()
            {
                Id = 0,
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Contacts/", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void UpdateVisibility_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new ChangeContactVisibilityCommand()
            {
                Id = 20,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/Contacts/visibility", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory()]
        [InlineData(1)]
        [InlineData(30)]
        public async void Delete_ForValidModel_ReturnOkResponse(int id)
        {
            // Act
            var response = await _client.DeleteAsync("/api/Contacts/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory()]
        [InlineData(0)]
        [InlineData(1500)]
        public async void Delete_ForNonExistingContact_ReturnNotFoundResponse(int id)
        {
            // Act
            var response = await _client.DeleteAsync("/api/Contacts/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}