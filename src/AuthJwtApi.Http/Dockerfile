FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/AuthJwtApi.Http/AuthJwtApi.Http.csproj", "src/AuthJwtApi.Http/"]
RUN dotnet restore "src/AuthJwtApi.Http/AuthJwtApi.Http.csproj"
COPY . .
WORKDIR "/src/src/AuthJwtApi.Http"
RUN dotnet build "AuthJwtApi.Http.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthJwtApi.Http.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthJwtApi.Http.dll"]
