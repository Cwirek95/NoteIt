using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NoteIt.Application.Security.Behaviours;

namespace NoteIt.Application.Security
{
    public static class ApplicationSecurityServices
    {
        public static IServiceCollection AddApplicationSecurityServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

            return services;
        }
    }
}
