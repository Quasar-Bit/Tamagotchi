
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tamagotchi.Data.Repositories;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Startup;
public static class StartupApplication
{
    public static void ApplicationConfigureServices(this IServiceCollection services)
    {
        //services.AddScoped<IExternalAuthenticationService, ExternalAuthenticationService>();

        //services.AddSingleton<IHttpClient, HttpClientImplementation>();

        //services.AddMediatR(typeof(StartupApplication).GetTypeInfo().Assembly);
        //services.AddValidatorsFromAssembly(typeof(StartupApplication).Assembly);

        //services.AddScoped<LoggerMessage, LoggerMessage>();

        //service
        services.AddMediatR(typeof(StartupApplication).GetTypeInfo().Assembly);
        //services.AddTransient<IMediator, Mediator>();
        //services.AddScoped<IMapper, ServiceMapper>();
        //services.AddScoped<IIdentityService, IdentityService>();

        //services.AddTransient<IAnimalRepository, AnimalRepository>();
        //services.AddTransient<IAnimalTypeRepository, AnimalTypeRepository>();
        //services.AddTransient<IOrganizationRepository, OrganizationRepository>();
    }
}