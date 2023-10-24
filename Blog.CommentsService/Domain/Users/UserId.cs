namespace Blog.CommentsService.Domain.Users
{
    public sealed record UserId(Guid Value)
    {
        public static UserId Create(Guid value) => new UserId(value);
    }
}
