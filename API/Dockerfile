FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy all files
COPY . .

# NuGet restore
RUN dotnet restore "API/AssignmentManager.API/AssignmentManager.API.csproj"

# Solution Build
WORKDIR "/src/API/AssignmentManager.API"
RUN dotnet build "AssignmentManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssignmentManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssignmentManager.API.dll"]