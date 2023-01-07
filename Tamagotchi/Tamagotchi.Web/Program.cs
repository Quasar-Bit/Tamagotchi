using MediatR;
using Tamagotchi.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Startup;
using Tamagotchi.Web.Services.Interfaces;
using Tamagotchi.Web.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Tamagotchi.Web.HealthChecks;
//using FluentValidation.AspNetCore;
//using FluentValidation;
//using Tamagotchi.Application.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationConfigureServices();
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddMvc();
    //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidateModelStateAttribute>());
//builder.Services.AddValidatorsFromAssembly(typeof(StartupApplication).Assembly);

builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAnimalTypeService, AnimalTypeService>();
builder.Services.AddTransient<IOrganizationService, OrganizationService>();
builder.Services.AddTransient<IAnimalService, AnimalService>();

builder.Services.AddHealthChecks()
    .AddCheck<PetFinderApiConnectionHealthCheck>(nameof(PetFinderApiConnectionHealthCheck))
    .AddCheck<PetFinderTokenHealthCheck>(nameof(PetFinderTokenHealthCheck));

// Configure the HTTP request pipeline.
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();