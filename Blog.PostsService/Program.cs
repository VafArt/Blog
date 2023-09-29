using Blog.Common.Domain.Repositories;
using Blog.Common.Infrastructure;
using Blog.PostsService.Domain.Repositories;
using Blog.PostsService.Infrastructure;
using Carter;
using Blog.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommon();
builder.Services.AddInfrastructure();

builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapCarter();

app.Run();
