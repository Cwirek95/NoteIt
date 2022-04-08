using MediatR;
using Serilog;
using System.Diagnostics;

namespace NoteIt.Application.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger;

        public PerformanceBehaviour(ILogger logger)
        {
            _timer = new Stopwatch();   
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;

                _logger.Warning("NoteIt Long Running Request:" +
                    " {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    requestName, elapsedMilliseconds, request);
            }

            return response;
        }
    }
}
