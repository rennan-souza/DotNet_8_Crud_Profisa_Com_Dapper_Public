# Etapa 1: Constru��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos do projeto para o cont�iner
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante dos arquivos e construir o projeto
COPY . ./
RUN dotnet publish -c Release -o /publish

# Etapa 2: Execu��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os bin�rios constru�dos na etapa anterior
COPY --from=build /publish .

# Expor a porta da aplica��o (mapeada pelo Render automaticamente)
EXPOSE 8080

# Comando de entrada para rodar a aplica��o
ENTRYPOINT ["dotnet", "CrudProfisaComDapper.dll"]
