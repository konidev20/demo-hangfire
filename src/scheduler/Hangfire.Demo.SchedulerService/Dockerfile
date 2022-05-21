#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Hangfire.Demo.SchedulerService.csproj", "."]
RUN dotnet restore "./Hangfire.Demo.SchedulerService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Hangfire.Demo.SchedulerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hangfire.Demo.SchedulerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hangfire.Demo.SchedulerService.dll"]