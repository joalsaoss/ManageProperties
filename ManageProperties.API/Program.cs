using ManageProperties.API.Middlewares;
using ManageProperties.Persist;
using ManagerProperties.Application;
using ManagerProperties.Identity;
using ManagerProperties.Identity.Models;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter("IsManager"));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddServiceRegistration();
builder.Services.AddPersistService();
builder.Services.AddIdentityServices();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseManagerExceptionsMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
