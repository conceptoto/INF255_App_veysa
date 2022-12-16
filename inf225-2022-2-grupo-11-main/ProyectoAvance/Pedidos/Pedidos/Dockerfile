FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5056

ENV ASPNETCORE_URLS=http://+:5056

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Pedidos/Pedidos/Pedidos.csproj", "Pedidos/Pedidos/"]
RUN dotnet restore "Pedidos/Pedidos/Pedidos.csproj"
COPY . .
WORKDIR "/src/Pedidos/Pedidos"
RUN dotnet build "Pedidos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pedidos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Pedidos/Pedidos/Scripts", "/app/Scripts"]
ENTRYPOINT ["dotnet", "Pedidos.dll"]
