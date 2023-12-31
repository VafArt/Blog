﻿using Blog.Common.Domain.Errors;
using Blog.PostsService.Domain.Posts;

namespace Blog.PostsService.Domain.Errors
{
    internal static class DomainErrors
    {
        internal static class Post
        {
            internal static Error AlreadyExists() => new Error(
                // если код ошибки поменяется надо будет изменить его в FailureHandler потому что в case используются только константы и перечисления
                "Post.AlreadyExists",
                "A post with the same id already exists");
            internal static NotFoundError NotFound(Guid id) => new NotFoundError(
                id.ToString(),
                "Post.NotFound",
                "There is no post with the specified id");

        }

        internal static class User
        {
            internal static Error AlreadyExists() => new Error(
                // если код ошибки поменяется надо будет изменить его в FailureHandler потому что в case используются только константы и перечисления
                "User.AlreadyExists",
                "A user with the same id already exists");
            internal static NotFoundError NotFound(Guid id) => new NotFoundError(
                id.ToString(),
                "User.NotFound",
                "There is no user with the specified id");

        }
    }
}
