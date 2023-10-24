using Blog.Common.Application.JsonConverters;
using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.Common.ModelBinders;
using Blog.Common.ProblemDetailsImplementation;
using Blog.PostsService.Application.Posts.Commands.CreatePost;
using Blog.PostsService.Application.Posts.Commands.DeletePost;
using Blog.PostsService.Application.Posts.Commands.UpdatePost;
using Blog.PostsService.Application.Posts.GetPostById;
using Blog.PostsService.Application.Posts.Queries.GetAllPosts;
using Blog.PostsService.Domain.Posts;
using Blog.PostsService.Presentation.Examples;
using Blog.PostsService.Presentation.Services;
using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Any;
using System.Globalization;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blog.PostsService.Presentation.Posts
{
    public class PostsModule : CarterModule
    {
        public PostsModule()
            :base("posts/")
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

            app.MapPost("/", async (
                CreatePostCommand createPostCommand, 
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await commandDispatcher.Dispatch<CreatePostCommand, Result<CreatePostCommandResponse>>(createPostCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.CreatedAtRoute("GetPostById", new { Id = result.Value.PostId }, result.Value);
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.PostsEndpoint.CreatePostDescription)
                .Produces(201, typeof(CreatePostCommandResponse), "application/json")
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(409);

            app.MapPut("/", async (
                UpdatePostCommand updatePostCommand, 
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await commandDispatcher.Dispatch<UpdatePostCommand, Result<UpdatePostCommandResponse>>(updatePostCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.PostsEndpoint.UpdatePostDescription)
                .Produces(200, typeof(UpdatePostCommandResponse), "application/json")
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(404, typeof(NotFoundProblemDetails), "application/json");

            app.MapDelete("/{postId}", async (
                Guid postId, CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var deletePostCommand = new DeletePostCommand(postId);
                var result = await commandDispatcher.Dispatch<DeletePostCommand, Result>(deletePostCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok();
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.PostsEndpoint.DeletePostDescription)
                .Produces(200)
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(404, typeof(NotFoundProblemDetails), "application/json");
        }
    }
}
