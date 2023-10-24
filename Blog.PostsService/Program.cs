using Blog.Common.Logging;
using Blog.PostsService.Infrastructure;
using Carter;
using Blog.Common;
using Blog.PostsService.Application;
using Blog.PostsService.Presentation;
using Blog.PostsService.Application.JsonConverters;
using Blog.Common.ModelBinders;
using Blog.Common.Application.JsonConverters;
using MassTransit;
using Blog.Common.Application.Middlewares;
using Blog.Common.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new GuidModelBinderProvider());
});

builder.Services.AddCommon(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddPresentation();

builder.Services.AddCarter();

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.Converters.Add(new JsonStringGuidConverter());
    opt.SerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
    opt.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Host.UseCommonLogging();

var app = builder.Build();

app.UseGlobalExceptionHandling();

app.UseCommonLogging();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapCarter();

var dbInitializer = app.Services.GetRequiredService<IDbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
