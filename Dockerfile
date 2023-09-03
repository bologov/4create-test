#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "4create-test.sln" "4create-test.sln"

COPY "WebApi/WebApi.csproj" "WebApi/"

COPY "Application/Application.csproj" "Application/"
COPY "Application.Contracts/Application.Contracts.csproj" "Application.Contracts/"

COPY "Domain/Domain.csproj" "Domain/"
COPY "Domain.Shared/Domain.Shared.csproj" "Domain.Shared/"

COPY "Data/Data.csproj" "Data/"

COPY "Common/Common.csproj" "Common/"

#COPY "NuGet.Config" "NuGet.Config"

RUN dotnet restore "4create-test.sln"
COPY . .

WORKDIR /src/WebApi
#RUN dotnet build "WebApi.csproj" -c Release -o /app/build
RUN dotnet build -c Release -o /app/build

FROM build AS publish
#RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]