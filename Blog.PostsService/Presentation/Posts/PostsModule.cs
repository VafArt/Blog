using Blog.Common.CQRS;
using Blog.Common.Domain;
using Blog.Common.Domain.Repositories;
using Blog.PostsService.Application.Mappings;
using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsService.Domain.Errors;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Domain.Repositories;
using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Blog.PostsService.Presentation.Posts
{
    public class PostsModule : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", async (
                Guid id, 
                CancellationToken cancellationToken, 
                IQueryDispatcher queryDispatcher
                ) =>
            {
                var getPostByIdQuery = new GetPostByIdQuery(id);

                var result = await queryDispatcher.Dispatch<GetPostByIdQuery, Result<GetPostByIdResponse>>(getPostByIdQuery, cancellationToken);

                if (result.IsFailure) return FailureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            });

            app.MapGet("/", async (
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher
                ) =>
            {
                var getAllPostsQuery = new GetAllPostsQuery();

                var result = await queryDispatcher.Dispatch<GetAllPostsQuery, Result<GetAllPostsResponse>>(getAllPostsQuery, cancellationToken);

                if (result.IsFailure) return FailureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            });
        }
    }
}
