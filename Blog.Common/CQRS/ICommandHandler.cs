using Blog.Common.Domain;

namespace Blog.Common.CQRS
{
    public interface ICommandHandler<in ICommand, TCommandResult>
    {
        Task<Result<TCommandResult>> Handle(ICommand command, CancellationToken cancellation);
    }
}
