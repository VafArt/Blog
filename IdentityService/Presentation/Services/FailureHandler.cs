using Blog.Common.Domain.Errors;
using Blog.Common.Domain.Results;
using Blog.Common.ProblemDetailsImplementation;
using Microsoft.AspNetCore.Mvc;

namespace Blog.IdentityService.Presentation.Services
{
    public class FailureHandler : IFailureHandler
    {
        public IResult HandleFailure(Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),

                IValidationResult validationResult =>
                Results.BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),

                { Error.Code: "Post.AlreadyExists" } =>
                Results.Conflict(
                    CreateProblemDetails(
                        "Conflict Error",
                        StatusCodes.Status409Conflict,
                        result.Error)),

                { Error.Code: "Post.NotFound" } =>
                Results.NotFound(CreateNotFoundProblemDetails(
                    "Not Found Error",
                    StatusCodes.Status404NotFound,
                    (result.Error as NotFoundError)!)),

                IIdentityResult identityResult =>
                Results.BadRequest(
                    CreateProblemDetails(
                        "Identity Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        identityResult.Errors)),

                _ =>
                    Results.BadRequest(
                        CreateProblemDetails(
                            "Bad Request",
                            StatusCodes.Status400BadRequest,
                            result.Error))
            };


        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            IEnumerable<Error>? errors = null) =>
            new()
            {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status,
                Extensions = { { nameof(errors), errors } },
            };

        private static NotFoundProblemDetails CreateNotFoundProblemDetails(
            string title,
            int status,
            NotFoundError error,
            IEnumerable<Error>? errors = null) =>
            new()
            {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status,
                Extensions = { { nameof(errors), errors } },
                Id = Guid.Parse(error.Parameter)
            };
    }
}
