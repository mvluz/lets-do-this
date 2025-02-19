using Amazon.SimpleSystemsManagement;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
SerilogConfiguration.ConfigureLogging(builder);

// Adicionar AWS, Parameter Store e outros serviços
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSimpleSystemsManagement>();
builder.Services.AddSingleton<IParameterStoreService, ParameterStoreService>();
builder.Services.AddSingleton<ParameterStoreConfiguration>();

// Configuração do banco de dados
var dbConfigService = new DatabaseConfigurationService(builder.Configuration);
dbConfigService.ConfigureDbContexts(builder);

// Configuração do JWT
var jwtConfigService = new JwtConfigurationService(builder.Configuration);
jwtConfigService.ConfigureJwt(builder);

// Configuração do Swagger
SwaggerConfiguration.ConfigureSwaggerServices(builder.Services);

// Adicionar Health Check
HealthCheckConfiguration.ConfigureHealthChecks(builder);

// Configuração de controle
builder.Services.AddControllers();

var app = builder.Build();

// Realiza a verificação de saúde durante a inicialização
// using (var scope = app.Services.CreateScope())
// {
//     var healthCheckService = scope.ServiceProvider.GetRequiredService<HealthCheckService>();
//     var healthReport = await healthCheckService.CheckHealthAsync();

//     // Se algum health check falhar, a aplicação irá lançar uma exceção e não iniciar
//     if (healthReport.Status != HealthStatus.Healthy)
//     {
//         throw new InvalidOperationException("A aplicação não pode iniciar, um serviço essencial não está saudável.");
//     }
// }

// Obter instância do serviço
var parameterStoreConfig = app.Services.GetRequiredService<ParameterStoreConfiguration>();
await parameterStoreConfig.ConfigureAppSettingsAsync(app.Configuration);

// Adicionar o middleware de tratamento global de exceções
app.UseMiddleware<GlobalExceptionMiddleware>();

SwaggerConfiguration.ConfigureSwaggerUI(app);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento dos controllers
app.MapControllers();

// Mapeamento do Health Check
app.MapHealthChecks("/health");

app.Run();