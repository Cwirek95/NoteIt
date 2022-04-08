using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoteIt.Api.Test.Helpers;
using NoteIt.Persistence.EF;
using System.Linq;

namespace NoteIt.Api.Test
{
    public class TestApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextOptions = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(dbContextOptions);

                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                services.AddMvc(option => option.Filters.Add(new FakePolicyFilter()));

                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ApplicationInMemoryDb"));
            });

            return base.CreateHost(builder);
        }
    }
}
