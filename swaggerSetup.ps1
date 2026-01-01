# SwaggerSetup.ps1

# Define o caminho relativo para o projeto de API
$apiPath = Join-Path (Get-Location) "UsersAPI/src/UsersAPI.Api"

# Verifica se a pasta existe antes de tentar entrar
if (Test-Path $apiPath) {
    Write-Host "Entrando na pasta: $apiPath" -ForegroundColor Cyan
    cd $apiPath

    Write-Host "Adicionando o pacote Swashbuckle.AspNetCore..." -ForegroundColor Yellow
    dotnet add package Swashbuckle.AspNetCore

    Write-Host "Pacote adicionado com sucesso!" -ForegroundColor Green
}
else {
    Write-Host "Erro: Não foi possível encontrar a pasta '$apiPath'." -ForegroundColor Red
    Write-Host "Certifique-se de executar este script na raiz do projeto (onde a pasta 'src' está)." -ForegroundColor Yellow
}