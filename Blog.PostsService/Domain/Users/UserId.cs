namespace Blog.PostsService.Domain.Users
{
    public sealed record UserId(Guid Value)
    {
        public static UserId Create(Guid id)
        {
            return new UserId(id);
        }
    }
}