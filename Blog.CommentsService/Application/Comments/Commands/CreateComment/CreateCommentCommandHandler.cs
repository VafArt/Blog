using Blog.CommentsService.Application.Mappings;
using Blog.CommentsService.Domain.Comments;
using Blog.CommentsService.Domain.Errors;
using Blog.CommentsService.Domain.Repositories;
using Blog.CommentsService.Infrastructure.Repositories;
using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;

namespace Blog.CommentsService.Application.Comments.CreateComment
{
    public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand, Result<CreateCommentCommandResponse>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ICommentMapper _commentMapper;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, IUnitOfWorkFactory unitOfWorkFactory, ICommentMapper commentMapper, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _commentMapper = commentMapper;
            _postRepository = postRepository;
        }

        public async Task<Result<CreateCommentCommandResponse>> Handle(CreateCommentCommand command, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            if (await _commentRepository.ContainsAsync(CommentId.Create(command.CommentId))) 
                return Result.Failure(new CreateCommentCommandResponse(), DomainErrors.Comment.AlreadyExists());

            if (command.ReplyCommentId is not null && !await _commentRepository.ContainsAsync(CommentId.Create(command.ReplyCommentId ?? Guid.Empty)))
                return Result.Failure(new CreateCommentCommandResponse(), DomainErrors.Comment.NotFound(command.ReplyCommentId!.Value));

            if (!await _postRepository.ContainsAsync(PostId.Create(command.PostId)))
                return Result.Failure(new CreateCommentCommandResponse(), DomainErrors.Post.NotFound(command.PostId));

            var comment = _commentMapper.MapCreateCommentCommandToComment(command);
            comment.CreatedOnUtc = DateTime.UtcNow;

            await _commentRepository.CreateCommentAsync(comment);

            await unitOfWork.CommitAsync(cancellation);

            return _commentMapper.MapCommentToCreateCommentCommandResponse(comment);
        }
    }
}
