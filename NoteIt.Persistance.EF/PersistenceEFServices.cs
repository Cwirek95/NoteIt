using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Persistence.EF.Repositories;

namespace NoteIt.Persistence.EF
{
    public static class PersistenceEFServices
    {
        public static IServiceCollection AddPersistenceEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("NoteItTestDbConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
