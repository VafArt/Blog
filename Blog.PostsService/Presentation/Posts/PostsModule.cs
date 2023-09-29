using Blog.Common.CQRS;
using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Domain.Posts;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blog.PostsService.Presentation.Posts
{
    public class PostsModule : CarterModule
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PostsModule(IQueryDispatcher queryDispatcher)
            : base("/posts")
        {
            _queryDispatcher = queryDispatcher;
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", async (Guid id, CancellationToken cancellationToken) =>
            {
                var getPostByIdQuery = new GetPostByIdQuery(id);

                var result = await _queryDispatcher.Dispatch<GetPostByIdResponse>(getPostByIdQuery, cancellationToken);

                if (result.IsFailure) return Results.Problem(result.Error);

                return Results.Ok(result.Value);
            });
        }
    }
}
