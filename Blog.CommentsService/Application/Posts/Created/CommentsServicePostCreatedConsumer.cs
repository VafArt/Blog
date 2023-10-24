using Blog.CommentsService.Domain.Posts;
using Blog.CommentsService.Domain.Repositories;
using Blog.CommentsService.Domain.Users;
using Blog.Common.Domain.Repositories;
using Blog.Contracts.Posts;
using MassTransit;

namespace Blog.CommentsService.Application.Posts.Created
{
    public class CommentsServicePostCreatedConsumer : IConsumer<PostCreatedEvent>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IPostRepository _postRepository;

        public CommentsServicePostCreatedConsumer(IUnitOfWorkFactory unitOfWorkFactory, IPostRepository postRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _postRepository = postRepository;
        }

        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var post = new Post
            {
                Id = PostId.Create(context.Message.PostId),
                UserId = UserId.Create(context.Message.UserId),
                Title = context.Message.Title,
                CreatedOnUtc = context.Message.CreatedOnUtc
            };

            await _postRepository.CreatePostAsync(post);

            await unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}
