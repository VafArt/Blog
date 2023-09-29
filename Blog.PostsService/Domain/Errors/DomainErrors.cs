using Blog.Common.Domain;

namespace Blog.PostsService.Domain.Errors
{
    internal static class DomainErrors
    {
        internal static class Post
        {
            internal static readonly Error AlreadyExists = new Error(
                "Post.AlreadyExists",
                "A post with the same id already exists");
            internal static readonly Error NotFound = new Error(
                "Post.NotFound",
                "There is no post with the specified id");
        }
    }
}
