using Blog.Common.ProblemDetailsImplementation;
using Blog.PostsReportingService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsReportingService.Application.Posts.Queries.GetPostById;
using Blog.PostsReportingService.Domain.PostEventTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.PostsReportingService.Presentation.Examples
{
    public static class ResponseExamples
    {
        public static class PostsEndpoint
        {
            public static class DeletePost
            {
                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Validation Error",
                    Detail = "Error detail"
                };

                public static NotFoundProblemDetails Status404NotFound = new NotFoundProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = "Post.NotFound",
                    Detail = "There is no post with the specified id",
                    Title = "Not Found Error",
                    Id = Guid.NewGuid()
                };
            }

            //public static class UpdatePost
            //{
            //    public static UpdatePostCommandResponse Status200Ok = new()
            //    {
            //        PostId = Guid.NewGuid(),
            //        Title = "Title",
            //        Content = "Content",
            //        Tags = new List<string>() { "tag1", "tag2", "tag3" },
            //        CreatedOnUtc = DateTime.Now,
            //        ModifiedOnUtc = DateTime.Now - TimeSpan.FromSeconds(123434)
            //    };
            //    public static ProblemDetails Status400BadRequest = new ProblemDetails
            //    {
            //        Status = (int)HttpStatusCode.BadRequest,
            //        Type = "Validation Error",
            //        Detail = "Error detail"
            //    };

            //    public static NotFoundProblemDetails Status404NotFound = new NotFoundProblemDetails
            //    {
            //        Status = (int)HttpStatusCode.NotFound,
            //        Type = "Post.NotFound",
            //        Detail = "There is no post with the specified id",
            //        Title = "Not Found Error",
            //        Id = Guid.NewGuid()
            //    };
            //}

            //public static class CreatePost
            //{
            //    public static CreatePostCommandResponse Status201Created = new()
            //    {
            //        PostId = Guid.NewGuid(),
            //        Title = "Title",
            //        Content = "Content",
            //        Tags = new List<string>() { "tag1", "tag2", "tag3" },
            //        CreatedOnUtc = DateTime.Now,
            //    };

            //    public static ProblemDetails Status400BadRequest = new ProblemDetails
            //    {
            //        Status = (int)HttpStatusCode.BadRequest,
            //        Type = "Validation Error",
            //        Detail = "Error detail"
            //    };
            //}

            public static class GetPostById
            {
                public static GetPostByIdQueryResponse Status200Ok = new GetPostByIdQueryResponse
                {
                    PostId = Guid.NewGuid(),
                    Title = "Title",
                    Events = new List<PostEventResponse>
                    {
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(254324),
                            EventType = PostEventType.Created
                        },
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(254300),
                            EventType = PostEventType.Published
                        },
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(2400),
                            EventType = PostEventType.Viewed
                        },
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(2324),
                            EventType = PostEventType.Liked
                        },
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(2000),
                            EventType = PostEventType.Viewed
                        },
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(1324),
                            EventType = PostEventType.Liked
                        },
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(324),
                            EventType = PostEventType.Modified
                        },
                        new PostEventResponse
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            CreatedOnUtc = DateTime.Now,
                            EventType = PostEventType.UnPublished
                        },
                    },
                };
                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int) HttpStatusCode.BadRequest,
                    Type = "Validation Error",
                    Detail = "Error detail"
                };

                public static NotFoundProblemDetails Status404NotFound = new NotFoundProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = "Post.NotFound",
                    Detail = "There is no post with the specified id",
                    Title = "Not Found Error",
                    Id = Guid.NewGuid()
                };
            }

            public static class GetAllPosts
            {
                public static GetAllPostsQueryResponse Status200Ok = new GetAllPostsQueryResponse
                {
                    Posts = new List<PostVm>
                    {
                        new PostVm
                        {
                            PostId = Guid.NewGuid(),
                            Title = "title",
                            Events = new List<PostEventVm>
                            {
                                new PostEventVm
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = Guid.NewGuid(),
                                    CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(543532),
                                    EventType = PostEventType.Created
                                },
                                new PostEventVm
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = Guid.NewGuid(),
                                    CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(43532),
                                    EventType = PostEventType.Published
                                },
                                new PostEventVm
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = Guid.NewGuid(),
                                    CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(43532),
                                    EventType = PostEventType.Viewed
                                },
                                new PostEventVm
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = Guid.NewGuid(),
                                    CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(3532),
                                    EventType = PostEventType.Liked
                                },
                                new PostEventVm
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = Guid.NewGuid(),
                                    CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(532),
                                    EventType = PostEventType.Modified
                                },
                                new PostEventVm
                                {
                                    Id = Guid.NewGuid(),
                                    PostId = Guid.NewGuid(),
                                    CreatedOnUtc = DateTime.Now - TimeSpan.FromSeconds(32),
                                    EventType = PostEventType.UnPublished
                                },
                            }
                        },
                    }
                };
            }

            public static class LikePost
            {
                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Validation Error",
                    Detail = "Error detail"
                };

                public static NotFoundProblemDetails Status404NotFound = new NotFoundProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = "Post.NotFound",
                    Detail = "There is no post with the specified id",
                    Title = "Not Found Error",
                    Id = Guid.NewGuid()
                };
            }
        }
    }
}
