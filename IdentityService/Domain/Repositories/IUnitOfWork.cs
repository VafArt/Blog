using Blog.IdentityService.Infrastructure;

namespace Blog.IdentityService.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
