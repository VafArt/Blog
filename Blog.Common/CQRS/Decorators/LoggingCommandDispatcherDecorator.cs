using Blog.Common.Domain.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS.Decorators
{
    internal class LoggingCommandDispatcherDecorator : ICommandDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger<LoggingCommandDispatcherDecorator> _logger;

        public LoggingCommandDispatcherDecorator(ICommandDispatcher commandDispatcher, ILogger<LoggingCommandDispatcherDecorator> logger)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        public async Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
            where TCommand : ICommand
            where TCommandResult : Result
        {
            _logger.LogInformation(
                "Starting request {@RequestName}, {@DateTimeUtc}",
                typeof(TCommand).Name,
                DateTime.UtcNow);

            var result = await _commandDispatcher.Dispatch<TCommand, TCommandResult>(command, cancellation);

            if(result.IsFailure)
            {
                _logger.LogError(
                    "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TCommand).Name,
                    result.Error,
                    DateTime.UtcNow);
            }

            _logger.LogInformation(
                "Completed request {@RequestName}, {@DateTimeUtc}",
                typeof(TCommand).Name,
                DateTime.UtcNow);

            return result;
        }
    }
}
