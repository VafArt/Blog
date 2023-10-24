using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.Common.Domain.Roles;
using Blog.IdentityService.Application.Auth.Commands.RefreshToken;
using Blog.IdentityService.Application.Auth.Commands.Register;
using Blog.IdentityService.Application.Auth.Commands.RegisterAdmin;
using Blog.IdentityService.Application.Auth.Commands.Revoke;
using Blog.IdentityService.Application.Auth.Commands.RevokeAll;
using Blog.IdentityService.Application.Auth.Queries.Login;
using Blog.IdentityService.Presentation.Services;
using Carter;
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
            });

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
            });

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
            });

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
                .RequireAuthorization(ApplicationRoleDefaults.Admin);
        }
    }
}
