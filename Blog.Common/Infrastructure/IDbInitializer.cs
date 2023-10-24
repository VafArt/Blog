namespace Blog.Common.Infrastructure
{
    public interface IDbInitializer
    {
        public Task InitializeAsync();
    }
}
