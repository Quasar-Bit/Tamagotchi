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
        services.AddSingleton(GetConfiguredMappingConfig());
        //services.AddSingleton<IHttpClient, HttpClientImplementation>();

        services.AddMediatR(typeof(StartupApplication).GetTypeInfo().Assembly);
        services.AddScoped<IMapper, ServiceMapper>();
        //services.AddValidatorsFromAssembly(typeof(StartupApplication).Assembly);

        //service
        services.AddTransient<IMediator, Mediator>();
        //services.AddScoped<IExternalAuthenticationService, ExternalAuthenticationService>();
        //services.AddScoped<IIdentityService, IdentityService>();

        services.AddTransient<IAnimalRepository, AnimalRepository>();
        services.AddTransient<IAnimalTypeRepository, AnimalTypeRepository>();
        services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        services.AddTransient<IAppSettingsRepository, AppSettingsRepository>();
    }
    private static TypeAdapterConfig GetConfiguredMappingConfig()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Apply(config.Scan(Assembly.GetExecutingAssembly()));

        return config;
    }
}