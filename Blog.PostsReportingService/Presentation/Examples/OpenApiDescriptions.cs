using Blog.Common.Application.JsonConverters;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization.Json;

namespace Blog.PostsReportingService.Presentation.Examples
{
    public static class OpenApiDescriptions
    {
        public static class PostsEndpoint
        {
            public static Func<OpenApiOperation, OpenApiOperation> GetPostByIdDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Gets post with specified id";
                var parameter = generatedOperation.Parameters[0];
                parameter.Description = "The ID associated with the requested post";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status200OK.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.PostsEndpoint.GetPostById.Status200Ok, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.PostsEndpoint.GetPostById.Status400BadRequest, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status404NotFound.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.PostsEndpoint.GetPostById.Status404NotFound, jsonOptions));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> GetAllPostsDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Gets all posts";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status200OK.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.PostsEndpoint.GetAllPosts.Status200Ok, jsonOptions));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> LikePostDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Creates PostEvent with Liked PostEventType";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.PostsEndpoint.LikePost.Status400BadRequest, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status404NotFound.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.PostsEndpoint.LikePost.Status404NotFound, jsonOptions));

                return generatedOperation;
            };
        }
    }
}
