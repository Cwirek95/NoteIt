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
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var storage1 = fixture.Create<Storage>();
            var storage2 = fixture.Create<Storage>();
            var storage3 = fixture.Create<Storage>();
            var storage4 = fixture.Create<Storage>();
            var storage5 = fixture.Create<Storage>();
            storages.Add(storage1);
            storages.Add(storage2);
            storages.Add(storage3);
            storages.Add(storage4);
            storages.Add(storage5);

            return storages;
        }
    }
}
