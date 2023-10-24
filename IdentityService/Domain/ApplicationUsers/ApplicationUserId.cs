namespace Blog.IdentityService.Domain.ApplicationUsers
{
    public sealed record ApplicationUserId
    {
        public Guid Value { get; set; }

        public ApplicationUserId Create(Guid id) => new () { Value = id };
    }
}
