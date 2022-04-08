using Xunit;
using System;
using System.Threading.Tasks;
using NoteIt.Api.Test;
using System.Net.Http;
using NoteIt.Application.Functions.Contacts.Commands.CreateContact;
using NoteIt.Api.Test.Helpers;
using System.Net;
using FluentAssertions;
using NoteIt.Application.Functions.Contacts.Commands.UpdateContact;
using NoteIt.Application.Functions.Contacts.Commands.ChangeContactVisibility;
using NoteIt.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using NoteIt.Persistence.EF;

namespace NoteIt.Api.Controllers.Tests
{
    public class ContactsControllerTests : IDisposable, IClassFixture<TestApplication>
    {
        private readonly HttpClient _client;
        private readonly TestApplication _testApplication;
        private readonly Storage _storage;

        private const string STORAGE_ADDRESS = "storage1";

        public ContactsControllerTests(TestApplication testApplication)
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

        private void SeedContact(Contact contact)
        {
            var scopeFactory = _testApplication.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Create_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var model = new CreateContactCommand()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageAddress = STORAGE_ADDRESS,
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/" + model.StorageAddress + "/Contacts", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task Create_ForInvalidModel_ReturnUnprocessableEntityResponse()
        {
            // Arrange
            var model = new CreateContactCommand()
            {
                Id = 0,
                Name = "",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageAddress = "storage1"
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PostAsync("/api/" + model.StorageAddress + "/Contacts", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact()]
        public async Task GetAllByStorageAddress_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Contacts");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Detail_WithQueryParameters_ReturnOkResponse()
        {
            // Arrange
            var contact = new Contact()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageId = Guid.NewGuid()
            };
            SeedContact(contact);

            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Contacts/" + contact.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact()]
        public async Task Detail_ForNonExistingContacts_ReturnNotFoundResponse()
        {
            //Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.GetAsync("/api/" + address + "/Contacts/" + 1000000);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var contact = new Contact()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageId = Guid.NewGuid()
            };
            SeedContact(contact);

            var model = new UpdateContactCommand()
            {
                Id = contact.Id,
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Contacts/" + model.Id, httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_ForNonExistingContact_ReturnNotFoundResponse()
        {
            // Arrange
            var model = new UpdateContactCommand()
            {
                Id = 0,
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageAddress= STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Contacts/" + model.Id, httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateVisibility_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var contact = new Contact()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageId = Guid.NewGuid()
            };
            SeedContact(contact);

            var model = new ChangeContactVisibilityCommand()
            {
                Id = contact.Id,
                StorageAddress = STORAGE_ADDRESS
            };
            var httpContent = model.ToJsonHttpContent();

            // Act
            var response = await _client.PutAsync("/api/" + model.StorageAddress + "/Contacts/" + model.Id + "/visibility", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForValidModel_ReturnOkResponse()
        {
            // Arrange
            var contact = new Contact()
            {
                Id = new Random().Next(1, 1000),
                Name = "Test",
                EmailAddress = "email@email.com",
                PhoneNumber = "+48123456789",
                StorageId = Guid.NewGuid()
            };
            SeedContact(contact);

            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.DeleteAsync("/api/" + address + "/Contacts/" + contact.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingContact_ReturnNotFoundResponse()
        {
            //Arrange
            string address = STORAGE_ADDRESS;

            // Act
            var response = await _client.DeleteAsync("/api/" + address + "/Contacts/" + 0);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}