using Blog.Common.Domain.Results;

namespace Blog.Common.CQRS
{
    public interface ICommandDispatcher
    {
        Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation = default)
            where TCommand : ICommand
            where TCommandResult : Result;
    }
}
