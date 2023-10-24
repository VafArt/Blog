namespace Blog.PostsService.Domain.Posts
{
    public sealed record PostId(Guid Value)
    {
        public static PostId Create(Guid id)
        {
            return new PostId(id);
        }
    }
}
