using MediatR;
using Tamagotchi.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Startup;
using TamagotchiWeb.Services.Interfaces;
using TamagotchiWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationConfigureServices();
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddMvc();
builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAnimalTypeService, AnimalTypeService>();

//builder.Services.AddHttpContextAccessor();

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

app.Run();