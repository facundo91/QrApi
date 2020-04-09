using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using qrAPI.DAL.Data;

namespace qrAPI.HealthChecks
{
    public class DataBaseHealthCheck : IHealthCheck
    {
        private readonly IDataContext _context;

        public DataBaseHealthCheck(IDataContext context)
        {
            _context = context;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                _context.HealthCheck();
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch
            {
                return Task.FromResult(HealthCheckResult.Unhealthy());
            }
        }
    }
}