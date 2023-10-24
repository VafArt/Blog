using Blog.CommentsService.Domain.Posts;

namespace Blog.CommentsService.Domain.Users
{
    public sealed class User
    {
        public UserId Id { get; set; } = null!;

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public string UserName { get; set; } = string.Empty;
    }
}
