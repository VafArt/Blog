using Blog.IdentityService.Domain.Repositories;

namespace Blog.IdentityService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _dbContext;

        public UnitOfWork(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
