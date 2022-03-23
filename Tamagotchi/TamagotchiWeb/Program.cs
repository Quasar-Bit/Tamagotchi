using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TamagotchiWeb.Application.Animals.Queries.GetAll;
using TamagotchiWeb.Application.AnimalTypes.Quieries.GetAll;
using TamagotchiWeb.Data;
using TamagotchiWeb.Data.Repositories;
using TamagotchiWeb.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddMvc();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<IMediator, Mediator>();
builder.Services.AddMediatR(typeof(GetAnimalsHandler).GetTypeInfo().Assembly);

builder.Services.AddTransient<IAnimalRepository, AnimalRepository>();
builder.Services.AddTransient<IAnimalTypeRepository, AnimalTypeRepository>();
builder.Services.AddTransient<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<MapsterMapper.IMapper, MapsterMapper.Mapper>();
var app = builder.Build();

// Configure the HTTP request pipeline.
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
