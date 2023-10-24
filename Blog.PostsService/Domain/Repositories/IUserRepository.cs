using Blog.PostsService.Domain.Users;

namespace Blog.PostsService.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserDto>> GetAllUsersAsync();

        public Task<UserDto?> GetUserByIdAsync(UserId id);

        public Task CreateUserAsync(User user);

        public Task UpdateUserAsync(User user);

        public Task<bool> ContainsAsync(UserId id);
    }
}
