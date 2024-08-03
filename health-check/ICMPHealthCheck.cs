using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace health_check
{
    public class ICMPHealthCheck(string host, int timeout) : IHealthCheck
    {
        private string Host { get; set; } = host;
        private int Timeout { get; set; } = timeout;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(Host);
                    switch (reply.Status)
                    {
                        case IPStatus.Success:
                            var msg = String.Format("ICMP to {0} took {1} ms.", Host, reply.RoundtripTime);
                            return (reply.RoundtripTime > Timeout) ? HealthCheckResult.Degraded(msg) : HealthCheckResult.Healthy(msg);
                        default:
                            var err = String.Format("ICMP to {0} failed: {1}", Host, reply.Status);
                            return HealthCheckResult.Unhealthy(err);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = String.Format("ICMP to {0} failed: {1}", Host, ex.Message);
                return HealthCheckResult.Unhealthy(err);
            }
        }
    }
}
