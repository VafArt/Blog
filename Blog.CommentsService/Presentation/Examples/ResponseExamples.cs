using Blog.CommentsService.Application.Comments.CreateComment;
using Blog.CommentsService.Application.Comments.Queries;
using Blog.CommentsService.Application.Comments.Queries.GetAllComments;
using Blog.CommentsService.Domain.Comments;
using Blog.Common.ProblemDetailsImplementation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.CommentsService.Presentation.Examples
{
    public static class ResponseExamples
    {
        public static class CommentsEndpoint
        {
            public static class GetCommentById
            {
                public static GetCommentByIdQueryResponse Status200Ok = new()
                {
                    CommentId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    PostId = Guid.NewGuid(),
                    ReplyCommentId = Guid.NewGuid(),
                    UserName = "user123",
                    Content = "Hello World!",
                    LikesCount = 2543,
                    CreatedOnUtc = DateTime.UtcNow - TimeSpan.FromSeconds(214432),
                    ModifiedOnUtc = DateTime.UtcNow - TimeSpan.FromSeconds(2343)
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
                    Type = "Comment.NotFound",
                    Detail = "There is no comment with the specified id",
                    Title = "Not Found Error",
                    Id = Guid.NewGuid()
                };
            }

            public static class CreateComment
            {
                public static CreateCommentCommandResponse Status201Created = new()
                {
                    CommentId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    PostId = Guid.NewGuid(),
                    ReplyCommentId = Guid.NewGuid(),
                    UserName = "user123",
                    Content = "Hello World!",
                    LikesCount = 532,
                    CreatedOnUtc = DateTime.UtcNow
                };

                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Validation Error",
                    Detail = "Error detail"
                };
            }

            public static class GetAllComments
            {
                public static GetAllCommentsQueryResponse Status200Ok = new()
                {
                    Comments = new List<CommentVm>
                    {
                        new CommentVm
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            ReplyCommentId = Guid.NewGuid(),
                            UserName = "user123",
                            Content = "Hello World!",
                            LikesCount = 4365,
                            CreatedOnUtc = DateTime.UtcNow - TimeSpan.FromSeconds(43252),
                            ModifiedOnUtc = DateTime.UtcNow - TimeSpan.FromSeconds(4322)
                        },
                        new CommentVm
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            ReplyCommentId = Guid.NewGuid(),
                            UserName = "Alice",
                            Content = "Hello Bitch!",
                            LikesCount = 365,
                            CreatedOnUtc = DateTime.UtcNow - TimeSpan.FromSeconds(432522),
                        },
                        new CommentVm
                        {
                            Id = Guid.NewGuid(),
                            PostId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            ReplyCommentId = Guid.NewGuid(),
                            UserName = "Ashot",
                            Content = "Waaaazuuup",
                            LikesCount = 0,
                            CreatedOnUtc = DateTime.UtcNow - TimeSpan.FromSeconds(843252),
                            ModifiedOnUtc = DateTime.UtcNow - TimeSpan.FromSeconds(14322)
                        }
                    },
                };
            }

            public static class DeleteComment
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
                    Type = "Comment.NotFound",
                    Detail = "There is no comment with the specified id",
                    Title = "Not Found Error",
                    Id = Guid.NewGuid()
                };
            }
        }
    }
}
