public class ParameterStoreConfiguration
{
    private readonly IParameterStoreService _parameterStoreService;
    private readonly ILogger<ParameterStoreConfiguration> _logger;

    public ParameterStoreConfiguration(IParameterStoreService parameterStoreService, ILogger<ParameterStoreConfiguration> logger)
    {
        _parameterStoreService = parameterStoreService;
        _logger = logger;
    }

    public async Task ConfigureAppSettingsAsync(IConfiguration configuration)
    {
        try
        {
            // Lista de parâmetros esperados
            var requiredParameters = new Dictionary<string, string>
            {
                { "WriteConnectionString", "/auth/db/write-connection-string" },
                { "ReadConnectionString", "/auth/db/read-connection-string" },
                { "JwtKey", "/auth/security/jwt-secret-key" },
                { "JwtIssuer", "/auth/security/jwt-issuer" },
                { "JwtAudience", "/auth/security/jwt-audience" },
                { "JwtExpirationTime", "/auth/security/jwt-expiration-time" }
            };

            // Obter os valores dos parâmetros
            var parameterValues = new Dictionary<string, string>();

            foreach (var param in requiredParameters)
            {
                var value = await _parameterStoreService.GetParameterAsync(param.Value);
                if (string.IsNullOrWhiteSpace(value))
                {
                    _logger.LogError($"Parâmetro obrigatório '{param.Key}' está ausente ou vazio.");
                    throw new Exception($"Parâmetro obrigatório '{param.Key}' está ausente ou vazio.");
                }

                parameterValues[param.Key] = value;
            }

            // Configurar os valores no app.Configuration
            configuration["ConnectionStrings:WriteConnection"] = parameterValues["WriteConnectionString"];
            configuration["ConnectionStrings:ReadConnection"] = parameterValues["ReadConnectionString"];
            configuration["Jwt:Key"] = parameterValues["JwtKey"];
            configuration["Jwt:Issuer"] = parameterValues["JwtIssuer"];
            configuration["Jwt:Audience"] = parameterValues["JwtAudience"];
            configuration["Jwt:ExpirationTime"] = parameterValues["JwtExpirationTime"];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar parâmetros do Parameter Store.");
            throw;
        }
    }
}