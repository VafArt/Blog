using Blog.Common.CQRS;
using Carter;
using Blog.CommentsService.Presentation.Services;
using Blog.CommentsService.Application.Comments.CreateComment;
using Blog.CommentsService.Domain.Comments;
using Blog.CommentsService.Application.Comments.Queries;
using Blog.Common.ProblemDetailsImplementation;
using Microsoft.AspNetCore.Mvc;
using Blog.CommentsService.Presentation.Examples;
using Blog.CommentsService.Application.Comments.Queries.GetAllComments;
using System.Threading;
using Blog.CommentsService.Application.Comments.Commands.DeleteComment;
using Blog.Common.Domain.Results;

namespace Blog.CommentsService.Presentation.Comments
{
    public class CommentsModule : CarterModule
    {
        public CommentsModule()
            :base("comments/")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/", async (
                CreateCommentCommand createCommentCommand, 
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await commandDispatcher.Dispatch<CreateCommentCommand, Result<CreateCommentCommandResponse>>(createCommentCommand, cancellationToken);

                if(result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.CreatedAtRoute("GetCommentById", new { Id = result.Value.CommentId }, result.Value);
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.CommentsEndpoint.CreateCommentDescription)
                .Produces(201, typeof(CreateCommentCommandResponse), "application/json")
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(409);

            app.MapGet("/{id}", async (
                Guid id, 
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher,
                IFailureHandler failureHandler) =>
            {
                var getCommentByIdQuery = new GetCommentByIdQuery(id);

                var result = await queryDispatcher.Dispatch<GetCommentByIdQuery, Result<GetCommentByIdQueryResponse>>(getCommentByIdQuery, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            })
                .RequireAuthorization()
                .WithName("GetCommentById")
                .WithOpenApi(OpenApiDescriptions.CommentsEndpoint.GetCommentByIdDescription)
                .Produces(200, typeof(GetCommentByIdQueryResponse), "application/json")
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(404, typeof(NotFoundProblemDetails), "application/json");

            app.MapGet("/", async (
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher,
                IFailureHandler failureHandler) =>
            {
                var getAllCommentsQuery = new GetAllCommentsQuery();
                var result = await queryDispatcher.Dispatch<GetAllCommentsQuery, Result<GetAllCommentsQueryResponse>>(getAllCommentsQuery, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.CommentsEndpoint.GetAllCommentsDescription)
                .Produces(200, typeof(GetCommentByIdQueryResponse), "application/json");

            app.MapDelete("/{id}", async (
                Guid id, 
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var deleteCommentCommand = new DeleteCommentCommand(id);
                var result = await commandDispatcher.Dispatch<DeleteCommentCommand, Result>(deleteCommentCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok();
                
            })
                .RequireAuthorization()
                .WithOpenApi(OpenApiDescriptions.CommentsEndpoint.DeleteCommentDescription)
                .Produces(200)
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(404, typeof(NotFoundProblemDetails), "application/json");
        }
    }
}
