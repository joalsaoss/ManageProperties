using ManageProperties.API.Middlewares;
using ManageProperties.Persist;
using ManagerProperties.Application;
using ManagerProperties.Identity;
using ManagerProperties.Identity.Models;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter("IsManager"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    // Título básico del doc
    opt.SwaggerDoc("v1", new() { Title = "ManageProperties API", Version = "v1" });

    // Incluir comentarios XML (controllers y DTOs)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    // Habilita anotaciones (SwaggerOperation/SwaggerResponse) si las usas
    opt.EnableAnnotations();

    // Opcional: Descripciones bonitas para archivos (IFormFile)
    // opt.SupportNonNullableReferenceTypes(); // si usas 'required' de C# 11
});

builder.Services.AddServiceRegistration();
builder.Services.AddPersistService();
builder.Services.AddIdentityServices();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManageProperties API v1");
    c.DisplayRequestDuration();
    // c.DefaultModelsExpandDepth(-1); // Oculta el schema panel si prefieres
});

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
