using Blog.CommentsService.Application;
using Blog.CommentsService.Application.Posts.Created;
using Blog.CommentsService.Infrastructure;
using Blog.CommentsService.Presentation;
using Blog.Common;
using Blog.Common.Application.JsonConverters;
using Blog.Common.Application.Middlewares;
using Blog.Common.Infrastructure;
using Blog.Common.Logging;
using Blog.PostsService.Application.JsonConverters;
using Carter;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.Converters.Add(new JsonStringGuidConverter());
    opt.SerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
    opt.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCarter();

builder.Services.AddInfrastructure();

builder.Services.AddPresentation();

builder.Services.AddApplication();

builder.Services.AddCommon(builder.Configuration);

builder.Host.UseCommonLogging();

var app = builder.Build();

//app.UseGlobalExceptionHandling();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();

app.UseCommonLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<IDbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
