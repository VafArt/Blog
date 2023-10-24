using Blog.CommentsService.Domain.Users;

namespace Blog.CommentsService.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();

        public Task<User?> GetUserByIdAsync(UserId id);

        public Task CreateUserAsync(User user);

        public Task UpdateUserAsync(User user);

        public Task<bool> ContainsAsync(UserId id);
    }
}
