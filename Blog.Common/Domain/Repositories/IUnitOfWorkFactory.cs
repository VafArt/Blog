using System.Data;

namespace Blog.Common.Domain.Repositories
{
    public interface IUnitOfWorkFactory
    {
        public IUnitOfWork Create();
    }
}
