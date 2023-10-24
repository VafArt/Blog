using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.Common.ProblemDetailsImplementation;
using Blog.PostsReportingService.Application.Posts.Commands.LikePost;
using Blog.PostsReportingService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsReportingService.Application.Posts.Queries.GetPostById;
using Blog.PostsReportingService.Presentation.Examples;
using Blog.PostsReportingService.Presentation.Services;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Blog.PostsReportingService.Presentation.Posts
{
    public class PostsModule : CarterModule
    {
        public PostsModule()
            : base("/posts")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", async (
                Guid id,
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher,
                IFailureHandler failureHandler
                ) =>
            {
                var getPostByIdQuery = new GetPostByIdQuery(id);

                var result = await queryDispatcher.Dispatch<GetPostByIdQuery, Result<GetPostByIdQueryResponse>>(getPostByIdQuery, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            })
                .RequireAuthorization()
                .WithName("GetPostById")
                .WithOpenApi(OpenApiDescriptions.PostsEndpoint.GetPostByIdDescription)
                .Produces(200, typeof(GetPostByIdQueryResponse), "application/json")
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(404, typeof(NotFoundProblemDetails), "application/json");


            app.MapGet("/", async (
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher,
                IFailureHandler failureHandler
                ) =>
            {
                var getAllPostsQuery = new GetAllPostsQuery();

                var result = await queryDispatcher.Dispatch<GetAllPostsQuery, Result<GetAllPostsQueryResponse>>(getAllPostsQuery, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.PostsEndpoint.GetAllPostsDescription)
                .Produces(200, typeof(GetAllPostsQueryResponse), "application/json");

            app.MapPost("/like/{id}", async (
                Guid id, 
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler
                ) =>
            {
                var likePostCommand = new LikePostCommand(id);

                var result = await commandDispatcher.Dispatch<LikePostCommand, Result>(likePostCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok();
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.PostsEndpoint.LikePostDescription)
                .Produces(200)
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(404, typeof(NotFoundProblemDetails), "application/json");
        }
    }
}
