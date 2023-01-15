using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using Tamagotchi.Web.Services.Interfaces;

namespace Tamagotchi.Web.HealthChecks
{
    public class PetFinderTokenHealthCheck : IHealthCheck
    {
        private readonly ITokenService _tokenService;

        public PetFinderTokenHealthCheck(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = await IsPetFinderTokenOkAsync();

            return isHealthy
                ? HealthCheckResult.Healthy("Pet Finder Token is working well")
                : HealthCheckResult.Unhealthy("Something is not working for Pet Finder Token");
        }

        private async Task<bool> IsPetFinderTokenOkAsync()
        {
            try
            {
                var result = await _tokenService.GetPetFinderToken();
                Debug.WriteLine("Success pet finder token check");
                return result;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
