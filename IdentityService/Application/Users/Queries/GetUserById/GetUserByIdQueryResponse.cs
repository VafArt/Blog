namespace Blog.IdentityService.Application.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQueryResponse
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}