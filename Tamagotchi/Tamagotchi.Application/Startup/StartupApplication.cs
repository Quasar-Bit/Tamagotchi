using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Tamagotchi.Application.Startup;
public static class StartupApplication
{
    public static void ApplicationConfigureServices(this IServiceCollection services)
    {
        //services.AddSingleton<IHttpClient, HttpClientImplementation>();

        services.AddMediatR(typeof(StartupApplication).GetTypeInfo().Assembly);
        //services.AddValidatorsFromAssembly(typeof(StartupApplication).Assembly);

        //service
        //services.AddTransient<IMediator, Mediator>();
        //services.AddScoped<IMapper, ServiceMapper>();
        //services.AddScoped<IExternalAuthenticationService, ExternalAuthenticationService>();
        //services.AddScoped<IIdentityService, IdentityService>();
    }
}