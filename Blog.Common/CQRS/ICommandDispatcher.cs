using Blog.Common.Domain;

namespace Blog.Common.CQRS
{
    public interface ICommandDispatcher
    {
        Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
            where TCommand : ICommand
            where TCommandResult : Result;
    }
}
