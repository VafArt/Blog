using Blog.Common.ProblemDetailsImplementation;
using Blog.PostsService.Application.Posts.Commands.CreatePost;
using Blog.PostsService.Application.Posts.Commands.UpdatePost;
using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Application.Posts.Queries.GetAllPosts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.PostsService.Presentation.Examples
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

            public static class UpdatePost
            {
                public static UpdatePostCommandResponse Status200Ok = new()
                {
                    PostId = Guid.NewGuid(),
                    Title = "Title",
                    Content = "Content",
                    Tags = new List<string>() { "tag1", "tag2", "tag3" },
                    CreatedOnUtc = DateTime.Now,
                    ModifiedOnUtc = DateTime.Now - TimeSpan.FromSeconds(123434)
                };
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

            public static class CreatePost
            {
                public static CreatePostCommandResponse Status201Created = new()
                {
                    PostId = Guid.NewGuid(),
                    Title = "Title",
                    Content = "Content",
                    Tags = new List<string>() { "tag1", "tag2", "tag3" },
                    CreatedOnUtc = DateTime.Now,
                };

                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Validation Error",
                    Detail = "Error detail"
                };
            }

            public static class GetPostById
            {
                public static GetPostByIdQueryResponse Status200Ok = new GetPostByIdQueryResponse
                {
                    Id = Guid.NewGuid(),
                    Title = "Title",
                    Content = "Content",
                    Tags = new List<string>() { "tag1", "tag2", "tag3" },
                    PreviewImageUri = "imageUri",
                    CreatedOnUtc = DateTime.Now,
                    PublishedOnUtc = DateTime.Now - TimeSpan.FromSeconds(1234534),
                    ModifiedOnUtc = DateTime.Now - TimeSpan.FromSeconds(123434)
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
                            Id = Guid.NewGuid(),
                            Title = "title",
                            Content = "content",
                            Tags = new List<string>() { "tag1", "tag2" },
                            PreviewImageUri = "preview uri",
                            CreatedOnUtc = DateTime.Now,
                            PublishedOnUtc = DateTime.Now - TimeSpan.FromSeconds(1234534),
                            ModifiedOnUtc = DateTime.Now - TimeSpan.FromSeconds(123434)
                        },
                        new PostVm
                        {
                            Id = Guid.NewGuid(),
                            Title = "title1",
                            Content = "content1",
                            Tags = new List<string>() { "tag2", "tag3" },
                            PreviewImageUri = "preview uri",
                            CreatedOnUtc = DateTime.Now,
                            PublishedOnUtc = DateTime.Now - TimeSpan.FromSeconds(1534534),
                            ModifiedOnUtc = DateTime.Now - TimeSpan.FromSeconds(134346)
                        },
                    }
                };
            }
        }
    }
}
