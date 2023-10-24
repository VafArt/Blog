using Blog.CommentsService.Domain.Comments;
using Blog.CommentsService.Infrastructure.Repositories;
using Blog.Common.Domain.Repositories;
using Blog.Contracts.Posts;
using MassTransit;
using System.Runtime.CompilerServices;

namespace Blog.CommentsService.Application.Posts.Viewed
{
    //public class PostViewedConsumer : IConsumer<PostViewedEvent>
    //{
    //    private readonly IPostRepository _postRepository;
    //    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    //    public PostViewedConsumer(IPostRepository postRepository, IUnitOfWorkFactory unitOfWorkFactory)
    //    {
    //        _postRepository = postRepository;
    //        _unitOfWorkFactory = unitOfWorkFactory;
    //    }

    //    public async Task Consume(ConsumeContext<PostViewedEvent> context)
    //    {
    //        var post = await _postRepository.GetPostByIdAsync(PostId.Create(context.Message.PostId));

    //        if(post is null)
    //        {
    //            return;
    //        }

    //        var postEvent 
    //    }
    //}
}
