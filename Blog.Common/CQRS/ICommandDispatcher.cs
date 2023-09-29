using Blog.Common.Domain;

namespace Blog.Common.CQRS
{
    public interface ICommandDispatcher
    {
        Task<Result<TCommandResult>> Dispatch<TCommandResult>(ICommand command, CancellationToken cancellation);
    }
}
