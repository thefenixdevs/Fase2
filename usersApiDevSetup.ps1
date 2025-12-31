# Criação da estrutura de pastas
mkdir UsersAPI; cd UsersAPI; mkdir src; cd src;

# Criação da Solution
dotnet new sln -n UsersAPI;

# Criação dos Projetos (.NET 9)
dotnet new classlib -n UsersAPI.Domain -f net9.0;
dotnet new classlib -n UsersAPI.Application -f net9.0;
dotnet new classlib -n UsersAPI.Infrastructure -f net9.0;
dotnet new webapi -n UsersAPI.Api -f net9.0 --use-controllers;

# Adicionando à Solution
dotnet sln add UsersAPI.Domain/UsersAPI.Domain.csproj;
dotnet sln add UsersAPI.Application/UsersAPI.Application.csproj;
dotnet sln add UsersAPI.Infrastructure/UsersAPI.Infrastructure.csproj;
dotnet sln add UsersAPI.Api/UsersAPI.Api.csproj;

# Configurando Referências (DDD Dependency Flow)
dotnet add UsersAPI.Application/UsersAPI.Application.csproj reference UsersAPI.Domain/UsersAPI.Domain.csproj;
dotnet add UsersAPI.Infrastructure/UsersAPI.Infrastructure.csproj reference UsersAPI.Domain/UsersAPI.Domain.csproj;
dotnet add UsersAPI.Api/UsersAPI.Api.csproj reference UsersAPI.Application/UsersAPI.Application.csproj;
dotnet add UsersAPI.Api/UsersAPI.Api.csproj reference UsersAPI.Infrastructure/UsersAPI.Infrastructure.csproj;

Write-Host "Estrutura UsersAPI criada com sucesso!" -ForegroundColor Green