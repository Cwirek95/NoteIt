﻿using NoteIt.Domain.Entities;

namespace NoteIt.Application.Contracts.Repositories
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<IReadOnlyList<Event>> GetAllAsyncByStorageId(Guid id);
    }
}
