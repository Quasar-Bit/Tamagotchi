using MediatR;
using Tamagotchi.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Startup;
using Microsoft.Extensions.DependencyInjection;
using Tamagotchi.Api.HealthChecks;
using Tamagotchi.Api.Settings;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationConfigureServices();
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Add services to the container. Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1", // not shown
        Title = "Aggregate App Service",
        Description = "Tamagotchi Net Core 6 APIs" // not shown
    });
});

builder.Services.AddHealthChecks()
    .AddCheck<TamagotchiApisHealthCheck>(nameof(TamagotchiApisHealthCheck));
    //.AddCheck<AnotherHealthCheck>(nameof(AnotherHealthCheck));

builder.Services.AddOptions();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();