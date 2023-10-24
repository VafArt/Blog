namespace Blog.CommentsService.Domain.Comments
{
    public sealed record PostId(Guid Value)
    {
        public static PostId Create(Guid value) => new PostId(value);
    }
}
