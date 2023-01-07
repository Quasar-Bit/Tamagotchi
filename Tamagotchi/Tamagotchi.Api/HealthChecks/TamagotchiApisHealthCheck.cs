using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Tamagotchi.Api.Settings;

namespace Tamagotchi.Api.HealthChecks
{
    public class TamagotchiApisHealthCheck : IHealthCheck
    {
        private readonly ConnectionStrings _dbConnectionStrings;

        public TamagotchiApisHealthCheck(IOptions<ConnectionStrings> options)
        {
            _dbConnectionStrings = options.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = await IsTamagotchiApisConnectionOkAsync();

            return isHealthy
                ? HealthCheckResult.Healthy("Tamagotchi APIs is working well")
                : HealthCheckResult.Unhealthy("Something is not working for Tamagotchi APIs");
        }

        private async Task<bool> IsTamagotchiApisConnectionOkAsync()
        {
            using var connection = new SqlConnection(_dbConnectionStrings.DefaultConnection);

            //return await Task.FromResult(DateTime.Now.Millisecond % 2 == 0);
            try
            {
                if(connection.State == System.Data.ConnectionState.Closed)
                    await connection.OpenAsync();
                //logger - Checking the connection of the db is OK.
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //logger - Checking the connection of the db is OK. + ex.Message!
                return false;
            }
        }
    }
}
