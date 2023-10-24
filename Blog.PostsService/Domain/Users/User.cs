using Blog.PostsService.Domain.Posts;

namespace Blog.PostsService.Domain.Users
{
    public sealed class User
    {
        public UserId Id { get; set; } = null!;

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public string UserName { get; set; } = string.Empty;
    }
}
