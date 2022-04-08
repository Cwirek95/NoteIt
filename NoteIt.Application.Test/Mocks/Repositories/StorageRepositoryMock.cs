using Moq;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteIt.Application.Test.Mocks.Repositories
{
    public class StorageRepositoryMock
    {
        public static Mock<IStorageRepository> GetStorageRepository()
        {
            var storages = GetStorages();
            var mockStorageRepository = new Mock<IStorageRepository>();

            mockStorageRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(storages);
            mockStorageRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    var storage = storages.FirstOrDefault(x => x.Id == id);
                    return storage;
                });
            mockStorageRepository.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(
                (string name) =>
                {
                    var storage = storages.FirstOrDefault(x => x.Name == name);
                    return storage;
                });
            mockStorageRepository.Setup(x => x.GetByAddressAsync(It.IsAny<string>())).ReturnsAsync(
                (string address) =>
                {
                    var storage = storages.FirstOrDefault(x => x.AddressName == address);
                    return storage;
                });
            mockStorageRepository.Setup(x => x.AddAsync(It.IsAny<Storage>())).ReturnsAsync(
                (Storage storage) =>
                {
                    storages.Add(storage);
                    return storage;
                });
            mockStorageRepository.Setup(x => x.UpdateAsync(It.IsAny<Storage>())).Callback<Storage>(
                (storage) => 
                { 
                    storages.RemoveAll(x => x.Id == storage.Id); 
                    storages.Add(storage); 
                });
            mockStorageRepository.Setup(x => x.DeleteAsync(It.IsAny<Storage>())).Callback<Storage>(
                (storage) =>
                {
                    storages.Remove(storage);
                });

            return mockStorageRepository;
        }

        private static List<Storage> GetStorages()
        {
            List<Storage> storages = new List<Storage>();

            var storage1 = new Storage()
            {
                Id = Guid.NewGuid(),
                Name = "Storage1",
                Password = "Pass1",
                AddressName = "storage1",
                CreatedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
                LastModified = DateTimeOffset.UtcNow,
            };
            var storage2 = new Storage()
            {
                Id = Guid.NewGuid(),
                Name = "Storage2",
                Password = "Pass2",
                AddressName = "storage2",
                CreatedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
                LastModified = DateTimeOffset.UtcNow,
            };
            var storage3 = new Storage()
            {
                Id = Guid.NewGuid(),
                Name = "Storage3",
                Password = "Pass3",
                AddressName = "storage3",
                CreatedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
                LastModified = DateTimeOffset.UtcNow,
            };
            var storage4 = new Storage()
            {
                Id = Guid.NewGuid(),
                Name = "Storage4",
                Password = "Pass4",
                AddressName = "storage4",
                CreatedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
                LastModified = DateTimeOffset.UtcNow,
            };
            var storage5 = new Storage()
            {
                Id = Guid.NewGuid(),
                Name = "Storage5",
                Password = "Pass5",
                AddressName = "storage5",
                CreatedAt = DateTimeOffset.UtcNow.AddMinutes(-1),
                LastModified = DateTimeOffset.UtcNow,
            };
            storages.Add(storage1);
            storages.Add(storage2);
            storages.Add(storage3);
            storages.Add(storage4);
            storages.Add(storage5);

            return storages;
        }
    }
}
