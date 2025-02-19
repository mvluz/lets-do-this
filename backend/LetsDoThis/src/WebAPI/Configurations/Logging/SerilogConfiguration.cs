using Serilog;

public static class SerilogConfiguration
{
    public static void ConfigureLogging(WebApplicationBuilder builder)
    {
        // Configuração do Serilog para enviar logs para o Fluentd
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Fluentd("tcp://fluentd-host:24224") // Substitua pelo endereço do seu Fluentd
            .CreateLogger();

        // Adicionar Serilog ao pipeline de logs do ASP.NET Core
        builder.Host.UseSerilog();
    }
}
