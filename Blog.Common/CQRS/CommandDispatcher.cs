using Blog.Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS
{
    class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task<Result<TCommandResult>> Dispatch<TCommandResult>(ICommand command, CancellationToken cancellation)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<ICommand, TCommandResult>>();
            return handler.Handle(command, cancellation);
        }
    }
}
