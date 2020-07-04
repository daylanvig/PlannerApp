FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY *.sln .
COPY PlannerApp.Server/*.csproj PlannerApp.Server/
RUN dotnet restore "PlannerApp.Server/PlannerApp.Server.csproj"
COPY . .

WORKDIR /source/PlannerApp.Server
RUN dotnet build "PlannerApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlannerApp.Server.csproj" -c release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlannerApp.Server.dll"]
