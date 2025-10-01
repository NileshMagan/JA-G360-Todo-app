using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TodoApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
});

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Response caching
builder.Services.AddResponseCaching();

builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

const string CorsPolicyName = "AllowFrontend";
builder.Services.AddCors(options =>
{
    var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy
            .WithOrigins(corsOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsPolicyName);
app.UseResponseCaching();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


