namespace Blog.PostsReportingService.Domain.PostEvents
{
    public sealed record PostEventId(Guid Value)
    {
        public static PostEventId Create(Guid id)
        {
            return new PostEventId(id);
        }
    }
}