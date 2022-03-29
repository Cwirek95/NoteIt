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
    public class ContactRepositoryMock
    {
        public static Mock<IContactRepository> GetContactRepository()
        {
            var contacts = GetContacts();
            var mockContactRepository = new Mock<IContactRepository>();

            mockContactRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(contacts);
            mockContactRepository.Setup(x => x.GetAllAsyncByStorageId(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    var contactsList = contacts.Where(x => x.StorageId == id).ToList();
                    return contactsList;
                });
            mockContactRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var contact = contacts.FirstOrDefault(x => x.Id == id);
                    return contact;
                });
            mockContactRepository.Setup(x => x.AddAsync(It.IsAny<Contact>())).ReturnsAsync(
                (Contact contact) =>
                {
                    contacts.Add(contact);
                    return contact;
                });
            mockContactRepository.Setup(x => x.UpdateAsync(It.IsAny<Contact>())).Callback<Contact>(
                (contact) =>
                {
                    contacts.RemoveAll(x => x.Id == contact.Id);
                    contacts.Add(contact);
                });
            mockContactRepository.Setup(x => x.DeleteAsync(It.IsAny<Contact>())).Callback<Contact>(
                (contact) =>
                {
                    contacts.Remove(contact);
                });

            return mockContactRepository;
        }

        private static List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var contact1 = fixture.Create<Contact>();
            var contact2 = fixture.Create<Contact>();
            var contact3 = fixture.Create<Contact>();
            var contact4 = fixture.Create<Contact>();
            var contact5 = fixture.Create<Contact>();

            contacts.Add(contact1);
            contacts.Add(contact2);
            contacts.Add(contact3);
            contacts.Add(contact4);
            contacts.Add(contact5);

            return contacts;
        }
    }
}
