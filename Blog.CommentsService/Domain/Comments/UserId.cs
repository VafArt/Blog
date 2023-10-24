namespace Blog.CommentsService.Domain.Comments
{
    public sealed record UserId(Guid Value)
    {
        public static UserId Create(Guid value) => new UserId(value);
    }
}
