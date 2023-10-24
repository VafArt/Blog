using Blog.Common.CQRS;
using Blog.Common.Domain.Policies;
using Blog.Common.Domain.Results;
using Blog.Common.Domain.Roles;
using Blog.IdentityService.Application.Auth.Commands.RefreshToken;
using Blog.IdentityService.Application.Auth.Commands.Register;
using Blog.IdentityService.Application.Auth.Commands.RegisterAdmin;
using Blog.IdentityService.Application.Auth.Commands.Revoke;
using Blog.IdentityService.Application.Auth.Commands.RevokeAll;
using Blog.IdentityService.Application.Auth.Queries.Login;
using Blog.IdentityService.Presentation.Examples;
using Blog.IdentityService.Presentation.Services;
using Carter;
using Microsoft.AspNetCore.Mvc;
using IdentityResult = Blog.Common.Domain.Results.IdentityResult;

namespace Blog.IdentityService.Presentation.Authentication
{
    public class AuthenticationModule : CarterModule
    {
        public AuthenticationModule()
            : base("/auth")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/registration",
                async
                (RegisterCommand registerCommand,
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await commandDispatcher.Dispatch<RegisterCommand, IdentityResult>(registerCommand, cancellationToken);

                if (result.IsFailure)
                {
                    return failureHandler.HandleFailure(result);
                }

                return Results.Ok();
            })
                .WithOpenApi(OpenApiDescriptions.AuthEndpoint.UserRegistrationDescription)
                .Produces(200)
                .Produces(400, typeof(ProblemDetails), "application/json");

            app.MapPost("/login",
                async
                (LoginQuery loginQuery,
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await queryDispatcher.Dispatch<LoginQuery, Result<LoginQueryResponse>>(loginQuery, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            })
                .WithOpenApi(OpenApiDescriptions.AuthEndpoint.LoginDescription)
                .Produces(200, typeof(LoginQueryResponse), "application/json")
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(401);

            app.MapPost("/refresh-token", 
                async 
                (RefreshTokenCommand refreshTokenCommand,
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await commandDispatcher.Dispatch<RefreshTokenCommand, Result<RefreshTokenCommandResponse>>(refreshTokenCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            })
                .WithOpenApi(OpenApiDescriptions.AuthEndpoint.RefreshTokenDescription)
                .Produces(200, typeof(LoginQueryResponse), "application/json")
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(401)
                .RequireAuthorization();

            app.MapPost("/registration-admin", 
                async 
                (RegisterAdminCommand registerAdminCommand, 
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await commandDispatcher.Dispatch<RegisterAdminCommand, IdentityResult>(registerAdminCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok();
            })
                .WithOpenApi(OpenApiDescriptions.AuthEndpoint.AdminRegistrationDescription)
                .Produces(200)
                .Produces(400, typeof(ProblemDetails), "application/json"); ;

            app.MapPost("/revoke",
                async
                (RevokeCommand revokeCommand,
                CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var result = await commandDispatcher.Dispatch<RevokeCommand, Result>(revokeCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok();
            })
                .WithOpenApi(OpenApiDescriptions.AuthEndpoint.RevokeDescription)
                .Produces(200)
                .Produces(400, typeof(ProblemDetails), "application/json")
                .Produces(401)
                .RequireAuthorization();

            app.MapPost("/revoke-all",
                async
                (CancellationToken cancellationToken,
                ICommandDispatcher commandDispatcher,
                IFailureHandler failureHandler) =>
            {
                var revokeAllCommand = new RevokeAllCommand();

                var result = await commandDispatcher.Dispatch<RevokeAllCommand, Result>(revokeAllCommand, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok();
            })
                .WithOpenApi(OpenApiDescriptions.AuthEndpoint.RevokeAllDescription)
                .Produces(200)
                .Produces(401)
                .RequireAuthorization(ApplicationPolicyDefaults.RequireAdminRole);
        }
    }
}
