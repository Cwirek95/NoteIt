using Microsoft.EntityFrameworkCore;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Exceptions;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Repositories
{
    public class StorageRepository : BaseRepository<Storage>, IStorageRepository
    {
        public StorageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Storage> GetByNameAsync(string name)
        {
            var storage = await _context.Storages
                .Include(x => x.Notes)
                .Include(x => x.Events)
                .Include(x => x.Contacts)
                .SingleOrDefaultAsync(x => x.Name == name);
            if (storage == null)
                throw new NotFoundException("The Storage with this name was not found");
            
            return storage;
        }
    }
}
