#/******************************************************************************
#** PROGRAMME  Dockerfile.cs                                             **
#**                                                                           **
#** Lieu      : ETML - section informatique                                   **
#** Auteur    : Joachim Berchel                                               **
#** Date      : 29.05.2024                                                    **
#**                                                                           **
#** Modifications                                                             **
#**   Auteur  :                                                               **
#**   Version :                                                               **
#**   Date    :                                                               **
#**   Raisons :                                                               **
#**                                                                           **
#**                                                                           **
#******************************************************************************/

#/******************************************************************************
#** DESCRIPTION                                                               **
#** Application ASP de jeu de clicker                                         **     
#**                                                                           **
#**                                                                           **
#******************************************************************************/

# Choose the ASP image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Define work repertory for the image
WORKDIR /app

# Open ports
EXPOSE 80
EXPOSE 443

# Build container from the image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Define work diary to build project
WORKDIR /src

# Copy csproj files in the folder ""FastClieckWebAppMVC"
COPY ["FastClieckWebAppMVC/FastClieckWebAppMVC.csproj", "FastClieckWebAppMVC/"]

# Execute the command to restore the project's dependencies
RUN dotnet restore "FastClieckWebAppMVC/FastClieckWebAppMVC.csproj"

# Copy every file
COPY . .

# Change work diary
WORKDIR "/src/FastClieckWebAppMVC"

# rebuild the project and each dependencies
RUN dotnet build "FastClieckWebAppMVC.csproj" -c Release -o /app/build

FROM build AS publish

# Publish the application and his dependencies in a file
RUN dotnet publish "FastClieckWebAppMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

# Change work diary
WORKDIR /app

# Copy published file
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastClieckWebAppMVC.dll"]