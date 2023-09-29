using System.Data.Common;

namespace Blog.Common.Infrastructure
{
    public interface IDbConnectionProvider
    {
        public DbConnection GetConnection();
    }
}
