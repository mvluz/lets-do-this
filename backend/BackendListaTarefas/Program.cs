using Backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000); // Permite conexões de qualquer IP
});

// Configurar o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080")  // Frontend
                .AllowAnyOrigin()
                .AllowAnyMethod()                     // Permitir qualquer método HTTP
                .AllowAnyHeader();                     // Permitir qualquer cabeçalho
    });
});

// Adicionar serviços ao container.
builder.Services.AddControllers();

// Configurar as rotas para sempre usarem letras minúsculas
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Adicionar o serviço de Task
builder.Services.AddSingleton<TaskService>();

// Adicionar suporte ao Swagger para documentação de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Lista de Tarefas", Version = "v1" });
});

var app = builder.Build();

// Configurar o middleware para processar as requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Lista de Tarefas v1");
        c.RoutePrefix = string.Empty;  // Configura para que o Swagger UI seja a página inicial
    });
}

// Habilitar o CORS
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
