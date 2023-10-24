using Blog.Common;
using Blog.Common.Application.JsonConverters;
using Blog.Common.ModelBinders;
using Blog.PostsService.Application.JsonConverters;
using Carter;
using Blog.Common.Logging;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Blog.IdentityService.Presentation;
using Blog.IdentityService.Infrastructure;
using Blog.IdentityService.Application;
using Blog.Common.Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddApplication();
builder.Services.AddPresentation();
builder.Services.AddCommon(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new GuidModelBinderProvider());
});

builder.Services.AddCarter();

builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.Converters.Add(new JsonStringGuidConverter());
    opt.SerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm.ss"));
    opt.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Host.UseCommonLogging();

var app = builder.Build();

app.UseCommonLogging();

//app.UseGlobalExceptionHandling();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.MapCarter();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync();
}

app.Run();
