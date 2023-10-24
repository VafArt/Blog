using Blog.Common.Application.JsonConverters;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blog.CommentsService.Presentation.Examples
{
    public static class OpenApiDescriptions
    {
        public static class CommentsEndpoint
        {
            public static Func<OpenApiOperation, OpenApiOperation> GetCommentByIdDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Gets comment with specified id";
                var parameter = generatedOperation.Parameters[0];
                parameter.Description = "The ID associated with the requested comment";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status200OK.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.GetCommentById.Status200Ok, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.GetCommentById.Status400BadRequest, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status404NotFound.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.GetCommentById.Status404NotFound, jsonOptions));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> CreateCommentDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Creates comment";
                var requestBody = generatedOperation.RequestBody;
                requestBody.Required = true;

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status201Created.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.CreateComment.Status201Created, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.CreateComment.Status400BadRequest, jsonOptions));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> GetAllCommentsDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Gets all comments";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status200OK.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.GetAllComments.Status200Ok, jsonOptions));

                return generatedOperation;
            };

            public static Func<OpenApiOperation, OpenApiOperation> DeleteCommentDescription = generatedOperation =>
            {
                generatedOperation.Summary = "Deletes comment with specified id";
                var parameter = generatedOperation.Parameters[0];
                parameter.Description = "The ID associated with the requested comment";

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                generatedOperation.Responses[StatusCodes.Status400BadRequest.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.DeleteComment.Status400BadRequest, jsonOptions));

                generatedOperation.Responses[StatusCodes.Status404NotFound.ToString()].Content["application/json"].Example =
                new OpenApiString(JsonSerializer.Serialize(ResponseExamples.CommentsEndpoint.DeleteComment.Status404NotFound, jsonOptions));

                return generatedOperation;
            };
        }
    }
}
