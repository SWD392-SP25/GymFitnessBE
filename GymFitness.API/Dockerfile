FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GymFitness.API/GymFitness.API.csproj", "GymFitness.API/"]
COPY ["GymFitness.Domain/GymFitness.Domain.csproj", "GymFitness.Domain/"]
COPY ["GymFitness.Application/GymFitness.Application.csproj", "GymFitness.Application/"]
COPY ["GymFitness.Infrastructure/GymFitness.Infrastructure.csproj", "GymFitness.Infrastructure/"]
RUN dotnet restore "GymFitness.API/GymFitness.API.csproj"
COPY . .
WORKDIR "/src/GymFitness.API"
RUN dotnet build "GymFitness.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GymFitness.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# This disables HTTPS requirement
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "GymFitness.API.dll"]
