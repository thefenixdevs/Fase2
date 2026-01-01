FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish src/UsersAPI.Api/UsersAPI.Api.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "UsersAPI.Api.dll"]