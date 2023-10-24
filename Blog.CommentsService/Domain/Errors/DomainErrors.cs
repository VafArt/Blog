using Blog.Common.Domain.Errors;

namespace Blog.CommentsService.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Comment
        {
            public static Error AlreadyExists() => new Error(
                // если код ошибки поменяется надо будет изменить его в FailureHandler потому что в case используются только константы и перечисления
                "Comment.AlreadyExists",
                "A comment with the same id already exists");

            public static NotFoundError NotFound(Guid id) => new NotFoundError(
                id.ToString(),
                "Comment.NotFound",
                "There is no comment with the specified id");
        }

        public static class Post
        {
            public static NotFoundError NotFound(Guid id) => new NotFoundError(
                id.ToString(),
                "Post.NotFound",
                "There is no post with the specified id");
        }
    }
}
