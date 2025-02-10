# Usa a imagem oficial do .NET SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e da solução (ajuste o caminho se necessário)
COPY ./backend/BackendListaTarefas/*.csproj ./BackendListaTarefas/

# Restaura as dependências
WORKDIR /app/BackendListaTarefas
RUN dotnet restore

# Copia o restante dos arquivos do backend
COPY ./backend/BackendListaTarefas/ ./

# Compila a aplicação
RUN dotnet publish -c Release -o /out

# Usa a imagem runtime para rodar a API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expõe a porta da API
EXPOSE 5000
CMD ["dotnet", "BackendListaTarefas.dll"]
