# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os projetos e restaura as dependências
COPY ["Chu.Bank.Inc.Api.csproj", "."]
RUN dotnet restore "Chu.Bank.Inc.Api.csproj"

# Copia todo o código e faz o build
COPY . .
RUN dotnet build "Chu.Bank.Inc.Api.csproj" -c Release -o /app/build

# Publica a aplicação
FROM build AS publish
RUN dotnet publish "Chu.Bank.Inc.Api.csproj" -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chu.Bank.Inc.Api.dll"]