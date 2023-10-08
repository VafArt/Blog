﻿using Blog.Common.Domain;

namespace Blog.Common.CQRS
{
    public interface ICommandHandler<in TCommand, TCommandResult>
        where TCommand : ICommand
        where TCommandResult : Result
    {
        Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
    }
}
