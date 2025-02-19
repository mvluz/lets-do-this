using Infrastructure.EF.Contexts;
using Microsoft.Extensions.Diagnostics.HealthChecks;


public static class HealthCheckConfiguration
{
    public static void ConfigureHealthChecks(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<WriteDbContext>()
            .AddDbContextCheck<ReadOnlyDbContext>()
            .AddCheck("API Running", () => HealthCheckResult.Healthy("API is healthy"));
    }
}
