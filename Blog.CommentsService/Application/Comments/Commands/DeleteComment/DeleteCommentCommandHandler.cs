using Blog.CommentsService.Domain.Errors;
using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.CommentsService.Domain.Repositories;
using Blog.CommentsService.Domain.Comments;
using Blog.Common.Domain.Results;

namespace Blog.CommentsService.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand, Result>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _commentRepository = commentRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Result> Handle(DeleteCommentCommand command, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            if (!await _commentRepository.ContainsAsync(CommentId.Create(command.CommentId))) return Result.Failure(DomainErrors.Comment.NotFound(command.CommentId));

            await _commentRepository.DeleteAsync(CommentId.Create(command.CommentId));

            await unitOfWork.CommitAsync(cancellation);

            return Result.Success();
        }
    }
}
