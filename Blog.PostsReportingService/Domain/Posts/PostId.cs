﻿namespace Blog.PostsReportingService.Domain.Posts
{
    public record PostId(Guid Value)
    {
        public static PostId Create(Guid id)
        {
            return new PostId(id);
        }
    }
}