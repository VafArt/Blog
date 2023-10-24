using Blog.PostsService.Domain.Posts;

namespace Blog.PostsService.Domain.Users
{
    public sealed class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public ICollection<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}
