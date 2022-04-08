using MediatR.Pipeline;
using Serilog;

namespace NoteIt.Application.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : class
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.Information("NoteIt Request: {Name} {@Request}",
                requestName, request);
        }
    }   
}
