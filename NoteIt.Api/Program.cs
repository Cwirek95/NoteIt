using Microsoft.OpenApi.Models;
using NoteIt.Api.Middleware;
using NoteIt.Api.Services;
using NoteIt.Application;
using NoteIt.Application.Security;
using NoteIt.Application.Security.Contracts;
using NoteIt.Infrastructure.Security;
using NoteIt.Persistence.EF;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.WithClientIp()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddSingleton(typeof(Serilog.ILogger), logger);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddApplicationSecurityServices();
builder.Services.AddPersistenceEFServices(builder.Configuration);
builder.Services.AddInfrastructureSecurityServices(builder.Configuration);
builder.Services.AddScoped<ICurrentStorageService, CurrentStorageService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
              Enter 'Bearer' [space] and then your token in the text input below.
              \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
          {
            {
              new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  },
                  Scheme = "oauth2",
                  Name = "Bearer",
                  In = ParameterLocation.Header,

                },
                new List<string>()
              }
            });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "NoteIt API",
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DisplayRequestDuration());
}

app.UseCors("Open");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Make the implicit Program class public so test projects can access it
public partial class Program { }
