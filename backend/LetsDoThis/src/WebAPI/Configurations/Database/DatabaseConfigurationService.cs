
using Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

public class DatabaseConfigurationService
{
    private readonly IConfiguration _configuration;

    public DatabaseConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureDbContexts(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<WriteDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("WriteConnection"));
        });

        builder.Services.AddDbContext<ReadOnlyDbContext>(options =>
        {
            options.UseNpgsql(_configuration.GetConnectionString("ReadConnection"));
        });
    }
}
