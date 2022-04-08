using MediatR;
using NoteIt.Application.Security.Contracts;
using NoteIt.Application.Security.Exceptions;

namespace NoteIt.Application.Security.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentStorageService _getCurrentStorage;

        public AuthorizationBehaviour(ICurrentStorageService getCurrentStorage)
        {
            _getCurrentStorage = getCurrentStorage;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request.GetType().Name == "LogInStorageCommand" || request.GetType().Name == "CreateStorageCommand")
                return await next();

            foreach (var property in typeof(TRequest).GetProperties())
            {
                if (property.Name == "StorageAddress" && property.GetValue(request).ToString() != _getCurrentStorage.Storage)
                    throw new ForbiddenException("You don't have permission to access");
            }

            return await next();
        }
    }
}
