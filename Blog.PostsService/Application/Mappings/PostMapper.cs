using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Domain.Posts;
using Riok.Mapperly.Abstractions;
using System.Runtime.CompilerServices;

namespace Blog.PostsService.Application.Mappings
{
    [Mapper]
    public static partial class PostMapper
    {
        public static partial GetPostByIdResponse CreateGetPostByIdResponse(this Post post);

        private static Guid MapPostIdToGuid(PostId postId) => postId.Value;
    }
}
