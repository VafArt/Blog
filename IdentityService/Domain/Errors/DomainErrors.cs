using Blog.Common.Domain.Errors;

namespace Blog.IdentityService.Domain.Errors
{
    internal static class DomainErrors
    {
        internal static class Post
        {
            internal static NotFoundError NotFoundById(Guid id) => new NotFoundError(
                id.ToString(),
                "Post.NotFound",
                "There is no post with the specified id");

            internal static Error AlreadyExists() => new Error(
                // если код ошибки поменяется надо будет изменить его в FailureHandler потому что в case используются только константы и перечисления
                "Post.AlreadyExists",
                "A post with the same id already exists");
        }

        internal static class ApplicationUser
        {
            internal static NotFoundError NotFoundById(Guid id) => new NotFoundError(
                id.ToString(),
                "ApplicationUser.NotFound",
                "There is no user with the specified id");

            internal static NotFoundError NotFoundByName(string username) => new NotFoundError(
                username,
                "ApplicationUser.NotFound",
                "There is no user with the specified username");

            internal static Error AlreadyExists() => new Error(
                // если код ошибки поменяется надо будет изменить его в FailureHandler потому что в case используются только константы и перечисления
                "User.AlreadyExists",
                "A user with the same id already exists");
        }

        internal static class Auth
        {
            internal static InvalidTokenError InvalidToken(string accessToken) => new InvalidTokenError(accessToken);

            internal static InvalidCredentialsError InvalidCredentials(string login, string password) 
                => new InvalidCredentialsError(login, password);
        }
    }
}
