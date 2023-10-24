namespace Blog.CommentsService.Domain.Comments
{
    public sealed record CommentId(Guid Value)
    {
        public static CommentId Create(Guid value) => new CommentId(value);
    }
}
