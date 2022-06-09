FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Queue/AssignmentManager.Queue.Service/AssignmentManager.Queue.Service.csproj", "Queue/AssignmentManager.Queue.Service/"]
RUN dotnet restore "Queue/AssignmentManager.Queue.Service/AssignmentManager.Queue.Service.csproj"
COPY . .
WORKDIR "/src/Queue/AssignmentManager.Queue.Service"
RUN dotnet build "AssignmentManager.Queue.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssignmentManager.Queue.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssignmentManager.Queue.Service.dll"]