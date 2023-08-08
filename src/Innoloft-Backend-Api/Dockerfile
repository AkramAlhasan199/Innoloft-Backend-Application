#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Innoloft-Backend-Api/Innoloft-Backend-Api.csproj", "src/Innoloft-Backend-Api/"]
COPY ["src/Innoloft-Backend-Domain/Innoloft-Backend-Domain.csproj", "src/Innoloft-Backend-Domain/"]
COPY ["src/Innoloft-Backend-Persistence/Innoloft-Backend-Persistence.csproj", "src/Innoloft-Backend-Persistence/"]
RUN dotnet restore "src/Innoloft-Backend-Api/Innoloft-Backend-Api.csproj"
COPY . .
WORKDIR "/src/src/Innoloft-Backend-Api"
RUN dotnet build "Innoloft-Backend-Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Innoloft-Backend-Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Innoloft-Backend-Api.dll"]