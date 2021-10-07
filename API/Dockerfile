FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy all project files for NuGet restore
COPY ["AssignmentManager.API/AssignmentManager.API.csproj", "AssignmentManager.API/"]
COPY ["AssignmentManager.EF/AssignmentManager.EF.csproj", "AssignmentManager.EF/"]
COPY ["AssignmentManager.Entities/AssignmentManager.Entities.csproj", "AssignmentManager.Entities/"]

# NuGet restore
RUN dotnet restore "AssignmentManager.API/AssignmentManager.API.csproj"

COPY . .
WORKDIR "/src/AssignmentManager.API"
RUN dotnet build "AssignmentManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssignmentManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssignmentManager.API.dll"]