using Blog.Common.Domain.Errors;
using Blog.Common.ProblemDetailsImplementation;
using Blog.IdentityService.Application.Auth.Queries.Login;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.IdentityService.Presentation.Examples
{
    public static class ResponseExamples
    {
        public static class AuthEndpoint
        {
            public static class Revoke
            {
                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "ApplicationUser.NotFound",
                    Detail = "There is no user with the specified username",
                    Title = "Bad Request"
                };
            }

            public static class RegisterUser
            {
                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Identity Error",
                    Detail = "An identity problem occured.",
                    Title = "Identity Error",
                    Extensions = 
                    { 
                        {
                            "errors",
                            new List<Error>()
                            {
                                new Error("DuplicateUserName","Username 'user123' is already taken."),
                                new Error("DuplicateEmail","Email 'string123@gmail.com' is already taken.")
                            }
                        }
                    }
                };
            }

            public static class RegisterAdmin
            {
                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Identity Error",
                    Detail = "An identity problem occured.",
                    Title = "Identity Error",
                    Extensions =
                    {
                        {
                            "errors",
                            new List<Error>()
                            {
                                new Error("DuplicateUserName","Username 'user123' is already taken."),
                                new Error("DuplicateEmail","Email 'string123@gmail.com' is already taken.")
                            }
                        }
                    }
                };
            }

            public static class Login
            {
                public static ProblemDetails Status400BadRequest = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Auth.InvalidCredentials",
                    Detail = "Invalid login or password",
                    Title = "Bad Request",
                };

                public static LoginQueryResponse Status200OK = new LoginQueryResponse
                {
                    AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjEyMyIsImp0aSI6IjMwOTRmZTc4LWI5ZjUtNGI5Ni05MzYyLTEwOGI2MDhkN2MyZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InVzZXIiLCJleHAiOjE2OTgxNzExNDIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjkwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo5MDAxIn0.DhWYKCwN9lXGHyXnSJPdNlpMqGhX4fLgPBvcYDg4ZKQ",
                    RefreshToken = "eYLhi+42StKdYBi7Md2ygEWGgl0eNju+LO9d4VbyAV0="
                };
            }

            public static class RefreshToken
            {
                public static LoginQueryResponse Status200OK = new LoginQueryResponse
                {
                    AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjEyMyIsImp0aSI6IjMwOTRmZTc4LWI5ZjUtNGI5Ni05MzYyLTEwOGI2MDhkN2MyZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InVzZXIiLCJleHAiOjE2OTgxNzExNDIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjkwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo5MDAxIn0.DhWYKCwN9lXGHyXnSJPdNlpMqGhX4fLgPBvcYDg4ZKQ",
                    RefreshToken = "eYLhi+42StKdYBi7Md2ygEWGgl0eNju+LO9d4VbyAV0="
                };
            }
        }
    }
}
