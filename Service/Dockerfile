FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Service/AssignmentManager.Notification.Service/AssignmentManager.Notification.Service.csproj", "Service/AssignmentManager.Notification.Service/"]
RUN dotnet restore "Service/AssignmentManager.Notification.Service/AssignmentManager.Notification.Service.csproj"
COPY . .
WORKDIR "/src/Service/AssignmentManager.Notification.Service"
RUN dotnet build "AssignmentManager.Notification.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssignmentManager.Notification.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssignmentManager.Notification.Service.dll"]