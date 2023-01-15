using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Tamagotchi.Web.HealthChecks
{
    public class PetFinderApiConnectionHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var result = await GetAnswer();
            return result
                ? HealthCheckResult.Healthy("PetFinder APIs connection is Ok.")
                : HealthCheckResult.Unhealthy("Something went wrong with PetFinder APIs.");
        }
        
        private async Task<bool> GetAnswer()
        {
            try
            {
                var ping = new Ping();
                string host = new Uri(Constants.BaseApiController + "types").Host;
                var result = await ping.SendPingAsync(host);
                return result.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}