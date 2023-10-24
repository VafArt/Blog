using Blog.Common.Application.JsonConverters;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Blog.IdentityService.Application.Auth.Commands.Register;
using Blog.IdentityService.Application.Auth.Queries.Login;
using Blog.IdentityService.Application.Auth.Commands.RefreshToken;
using Blog.IdentityService.Application.Auth.Commands.Revoke;

namespace Blog.IdentityService.Presentation.Examples
{
    public static class OpenApiDescriptions
    {
        public static class AuthEndpoint
        {
            public static Func<OpenApiOperation, OpenApiOperation> UserRegistrationDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Registers user";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.AuthEndpoint.RegisterUser.Status400BadRequest, jsonOptions));

                generatedOperation.RequestBody.Content["application/json"].Example = 
                new OpenApiString(JsonSerializer.Serialize(new RegisterCommand
                {
                    Email = "user123@gmail.com",
                    Username = "user123",
                    Password = "user1989"
                }));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> AdminRegistrationDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Registers admin";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.AuthEndpoint.RegisterAdmin.Status400BadRequest, jsonOptions));

                generatedOperation.RequestBody.Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(new RegisterCommand
                {
                    Email = "admin123@gmail.com",
                    Username = "admin123",
                    Password = "admin1989"
                }));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> LoginDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Creates access and refresh tokens.";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.AuthEndpoint.Login.Status400BadRequest, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status200OK.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.AuthEndpoint.Login.Status200OK, jsonOptions));

                generatedOperation.RequestBody.Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(new LoginQuery
                {
                    Username = "user123",
                    Password = "user1989"
                }));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> RefreshTokenDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Refreshes access and refresh tokens.";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status200OK.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.AuthEndpoint.RefreshToken.Status200OK, jsonOptions));

                generatedOperation.RequestBody.Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(new RefreshTokenCommandResponse
                {
                    AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjEyMyIsImp0aSI6IjMwOTRmZTc4LWI5ZjUtNGI5Ni05MzYyLTEwOGI2MDhkN2MyZCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InVzZXIiLCJleHAiOjE2OTgxNzExNDIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjkwMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo5MDAxIn0.DhWYKCwN9lXGHyXnSJPdNlpMqGhX4fLgPBvcYDg4ZKQ",
                    RefreshToken = "eYLhi+42StKdYBi7Md2ygEWGgl0eNju+LO9d4VbyAV0="
                }));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> RevokeDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Revokes access token of specified user";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.AuthEndpoint.Revoke.Status400BadRequest, jsonOptions));

                generatedOperation.RequestBody.Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(new RevokeCommand("user123")));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> RevokeAllDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Revokes all access token of all users";

                return generatedOperation;
            };
        }
    }
}
