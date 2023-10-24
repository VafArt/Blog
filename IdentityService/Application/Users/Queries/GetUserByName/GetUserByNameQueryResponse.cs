namespace Blog.IdentityService.Application.Users.Queries.GetUserByName
{
    public sealed class GetUserByNameQueryResponse
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}