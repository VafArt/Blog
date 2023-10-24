using Blog.Common.Domain.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS.Decorators
{
    internal class LoggingQueryDispatcherDecorator : IQueryDispatcher
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<LoggingQueryDispatcherDecorator> _logger;

        public LoggingQueryDispatcherDecorator(IQueryDispatcher queryDispatcher, ILogger<LoggingQueryDispatcherDecorator> logger)
        {
            _queryDispatcher = queryDispatcher;
            _logger = logger;
        }

        public async Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
            where TQuery : IQuery
            where TQueryResult : Result
        {
            _logger.LogInformation(
                "Starting request {@RequestName}, {@DateTimeUtc}",
                typeof(TQuery).Name,
            DateTime.UtcNow);

            var result = await _queryDispatcher.Dispatch<TQuery, TQueryResult>(query, cancellation);

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TQuery).Name,
                    result.Error,
                    DateTime.UtcNow);
            }

            _logger.LogInformation(
                "Completed request {@RequestName}, {@DateTimeUtc}",
                typeof(TQuery).Name,
                DateTime.UtcNow);

            return result;
        }
    }
}
