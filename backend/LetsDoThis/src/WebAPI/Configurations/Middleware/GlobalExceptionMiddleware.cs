public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        // catch (ValidationException ex)
        // {
        //     // Log da exceção (opcional)
        //     _logger.LogError(ex, "Erro de validação");

        //     // Formatar a resposta com os erros de validação
        //     var validationErrors = ex.Errors.Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });
        //     var response = new
        //     {
        //         status = "error",
        //         message = "Validação falhou",
        //         errors = validationErrors
        //     };

        //     // Definir o status code e retornar a resposta com erros
        //     context.Response.StatusCode = StatusCodes.Status400BadRequest;
        //     context.Response.ContentType = "application/json";
        //     await context.Response.WriteAsJsonAsync(response);
        // }
        catch (Exception ex)
        {
            // Para outras exceções, você pode seguir com o tratamento padrão
            _logger.LogError(ex, "Erro inesperado");

            var response = _env.IsDevelopment()
                ? new { message = ex.Message, stackTrace = ex.StackTrace }
                : new { message = "Ocorreu um erro interno no servidor.", stackTrace = (string?)null };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
