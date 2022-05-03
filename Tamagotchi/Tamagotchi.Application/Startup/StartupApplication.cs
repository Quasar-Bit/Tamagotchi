using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tamagotchi.Application.Localization;
using Tamagotchi.Application.Localization.Interface;
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

        //service
        services.AddTransient<IMediator, Mediator>();

        services.AddTransient<IAnimalRepository, AnimalRepository>();
        services.AddTransient<IAnimalTypeRepository, AnimalTypeRepository>();
        services.AddTransient<IOrganizationRepository, OrganizationRepository>();
        services.AddTransient<IAppSettingRepository, AppSettingRepository>();

        services.AddSingleton<ILocalizationService, LocalizationService>();
        services.AddMemoryCache();
    }
    private static TypeAdapterConfig GetConfiguredMappingConfig()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Apply(config.Scan(Assembly.GetExecutingAssembly()));

        return config;
    }
}