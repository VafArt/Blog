using Blog.Common.CQRS;
using Blog.Common.Domain.Results;
using Blog.IdentityService.Application.Users.Queries.GetUserById;
using Blog.IdentityService.Application.Users.Queries.GetUserByName;
using Blog.IdentityService.Presentation.Services;
using Carter;

namespace Blog.IdentityService.Presentation.Users
{
    public class UserModule : CarterModule
    {
        public UserModule()
            :base("user/")
        {
            
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("get-by-id/{id}/", async (
                Guid id, 
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher,
                IFailureHandler failureHandler) =>
            {
                var getUserByIdQuery = new GetUserByIdQuery(id);

                var result = await queryDispatcher.Dispatch<GetUserByIdQuery, Result<GetUserByIdQueryResponse>>(getUserByIdQuery, cancellationToken);

                if(result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            });

            app.MapGet("get-by-name/{name}/", async (
                string userName,
                CancellationToken cancellationToken,
                IQueryDispatcher queryDispatcher,
                IFailureHandler failureHandler) =>
            {
                var getUserByNameQuery = new GetUserByNameQuery(userName);

                var result = await queryDispatcher.Dispatch<GetUserByNameQuery, Result<GetUserByNameQueryResponse>>(getUserByNameQuery, cancellationToken);

                if (result.IsFailure) return failureHandler.HandleFailure(result);

                return Results.Ok(result.Value);
            });
        }
    }
}
