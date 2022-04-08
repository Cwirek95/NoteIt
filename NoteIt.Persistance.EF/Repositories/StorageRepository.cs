using Microsoft.EntityFrameworkCore;
using NoteIt.Application.Contracts.Repositories;
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
                .FirstOrDefaultAsync(x => x.Name == name);
            
            return storage;
        }

        public async Task<Storage> GetByAddressAsync(string address)
        {
            var storage = await _context.Storages
                .Include(x => x.Notes)
                .Include(x => x.Events)
                .Include(x => x.Contacts)
                .FirstOrDefaultAsync(x => x.AddressName == address);
            
            return storage;
        }
    }
}
