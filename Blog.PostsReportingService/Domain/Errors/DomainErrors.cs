using Blog.Common.Domain.Errors;

namespace Blog.PostsReportingService.Domain.Errors
{
    internal static class DomainErrors
    {
        internal static class Post
        {
            internal static NotFoundError NotFound(Guid id) => new NotFoundError(
                id.ToString(),
                "Post.NotFound",
                "There is no post with the specified id");

            internal static Error AlreadyExists() => new Error(
                // если код ошибки поменяется надо будет изменить его в FailureHandler потому что в case используются только константы и перечисления
                "Post.AlreadyExists",
                "A post with the same id already exists");
        }
    }
}
