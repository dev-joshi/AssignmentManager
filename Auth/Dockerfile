FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy all files
COPY . .

# NuGet restore
RUN dotnet restore "Auth/AssignmentManager.Auth.API/AssignmentManager.Auth.API.csproj"

# Solution Build
WORKDIR "/src/Auth/AssignmentManager.Auth.API"
RUN dotnet build "AssignmentManager.Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssignmentManager.Auth.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssignmentManager.Auth.API.dll"]