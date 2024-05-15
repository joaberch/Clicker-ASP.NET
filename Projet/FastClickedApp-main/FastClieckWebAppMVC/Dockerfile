# Choose the ASPNET image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

#Define the work repertory for the image
WORKDIR /app

# Open the ports
EXPOSE 80
EXPOSE 443

# Build the container from the image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Define a work repertory to build the project
WORKDIR /src

#Copy the csproj file in the repertory "FastClieckWebAppMVC"
COPY ["FastClieckWebAppMVC/FastClieckWebAppMVC.csproj", "FastClieckWebAppMVC/"]

# Restore project's dependencies
RUN dotnet restore "FastClieckWebAppMVC/FastClieckWebAppMVC.csproj"

# Copy every file
COPY . .

# Change repertory
WORKDIR "/src/FastClieckWebAppMVC"

# Rebuild the project with it's dependencies
RUN dotnet build "FastClieckWebAppMVC.csproj" -c Release -o /app/build

FROM build AS publish

#Publish the application and his dependencies in a folder
RUN dotnet publish "FastClieckWebAppMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

# Change repertory
WORKDIR /app

# Copy published file
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastClieckWebAppMVC.dll"]