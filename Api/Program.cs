using System;
using Api.Mapping;
using Application.service;
using Infrastructure;
using Infrastructure.repository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(TaskMappingProfile));

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

// DbContext
builder.Services.AddDbContext<TaskTrackerContext>(opts =>
{
    opts.UseInMemoryDatabase("TaskDb");
});

//Dependency Injection
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", p =>
    {
        p.WithOrigins("http://localhost:4200", "http://localhost:5173")
         .AllowAnyHeader()
         .AllowAnyMethod();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.0.0", new OpenApiInfo { Title = "Task Tracker API", Version = "v1.0.0" });
});

var app = builder.Build();

app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var problem = new ProblemDetails
        {
            Status = 500,
            Title = "An unexpected error occurred",
            Detail = exception?.Message
        };

        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(problem);
    });
});

//Seed in memory db
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskTrackerContext>();
    db.Database.EnsureCreated();
}


app.UseHttpsRedirection();    
app.UseCors("AllowFrontend"); 
app.UseHttpLogging();


app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1.0.0/swagger.json", "Task Tracker API v1.0.0");
    c.RoutePrefix = string.Empty; 
});

app.MapControllers();


app.Run();

public partial class Program { }
