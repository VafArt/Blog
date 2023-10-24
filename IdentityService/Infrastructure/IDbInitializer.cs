namespace Blog.IdentityService.Infrastructure
{
    public interface IDbInitializer
    {
        public Task InitializeAsync();
    }
}
