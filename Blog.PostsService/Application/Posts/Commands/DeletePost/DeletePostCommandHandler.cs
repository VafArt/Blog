using Blog.Common.CQRS;
using Blog.Common.Domain.Repositories;
using Blog.Common.Domain.Results;
using Blog.PostsService.Domain.Errors;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using System.Runtime.CompilerServices;

namespace Blog.PostsService.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, Result>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _postRepository = postRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Result> Handle(DeletePostCommand command, CancellationToken cancellation)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            if (!await _postRepository.ContainsAsync(PostId.Create(command.PostId))) return Result.Failure(DomainErrors.Post.NotFound(command.PostId));

            await _postRepository.DeleteAsync(PostId.Create(command.PostId));

            unitOfWork.Commit();

            return Result.Success();
        }
    }
}
