param(
    [Parameter(Mandatory=$true)]
    [string]$Entidad
)

$root = Get-Location

$application = Join-Path $root "MagisAuto.Application"
$domain = Join-Path $root "MagisAuto.Domain"
$infrastructure = Join-Path $root "MagisAuto.Infrastructure"
$api = Join-Path $root "MagisAutoAPI"

# Verificar proyectos
$projects = @(
    $application,
    $domain,
    $infrastructure,
    $api
)

foreach ($project in $projects) {
    if (!(Test-Path $project)) {
        Write-Host "ERROR: No se encontró $project" -ForegroundColor Red
        exit
    }
}

# Crear carpetas
New-Item -ItemType Directory -Force -Path "$application\DTOs\$Entidad" | Out-Null
New-Item -ItemType Directory -Force -Path "$application\Interfaces\$Entidad" | Out-Null
New-Item -ItemType Directory -Force -Path "$application\Validators\$Entidad" | Out-Null
New-Item -ItemType Directory -Force -Path "$application\Services" | Out-Null
New-Item -ItemType Directory -Force -Path "$domain\Entities" | Out-Null
New-Item -ItemType Directory -Force -Path "$infrastructure\Repositories" | Out-Null
New-Item -ItemType Directory -Force -Path "$api\Controllers" | Out-Null

# DOMAIN
New-Item -ItemType File -Force -Path "$domain\Entities\$Entidad.cs" | Out-Null

# DTOs
New-Item -ItemType File -Force -Path "$application\DTOs\$Entidad\${Entidad}Dto.cs" | Out-Null
New-Item -ItemType File -Force -Path "$application\DTOs\$Entidad\Crear${Entidad}Dto.cs" | Out-Null
New-Item -ItemType File -Force -Path "$application\DTOs\$Entidad\Actualizar${Entidad}Dto.cs" | Out-Null

# VALIDATORS
New-Item -ItemType File -Force -Path "$application\Validators\$Entidad\Crear${Entidad}Validator.cs" | Out-Null
New-Item -ItemType File -Force -Path "$application\Validators\$Entidad\Actualizar${Entidad}Validator.cs" | Out-Null

# INTERFACES
New-Item -ItemType File -Force -Path "$application\Interfaces\$Entidad\I${Entidad}Repository.cs" | Out-Null
New-Item -ItemType File -Force -Path "$application\Interfaces\$Entidad\I${Entidad}Service.cs" | Out-Null

# SERVICE
New-Item -ItemType File -Force -Path "$application\Services\${Entidad}Service.cs" | Out-Null

# REPOSITORY
New-Item -ItemType File -Force -Path "$infrastructure\Repositories\${Entidad}Repository.cs" | Out-Null

# CONTROLLER
New-Item -ItemType File -Force -Path "$api\Controllers\${Entidad}Controller.cs" | Out-Null

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "Entidad $Entidad creada correctamente" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""

Write-Host "DTOs:" -ForegroundColor Cyan
Write-Host "  $Entidad`Dto.cs"
Write-Host "  Crear$Entidad`Dto.cs"
Write-Host "  Actualizar$Entidad`Dto.cs"

Write-Host ""
Write-Host "Validators:" -ForegroundColor Cyan
Write-Host "  Crear$Entidad`Validator.cs"
Write-Host "  Actualizar$Entidad`Validator.cs"

Write-Host ""
Write-Host "Interfaces:" -ForegroundColor Cyan
Write-Host "  I$Entidad`Repository.cs"
Write-Host "  I$Entidad`Service.cs"

Write-Host ""
Write-Host "Service:" -ForegroundColor Cyan
Write-Host "  $Entidad`Service.cs"

Write-Host ""
Write-Host "Repository:" -ForegroundColor Cyan
Write-Host "  $Entidad`Repository.cs"

Write-Host ""
Write-Host "Controller:" -ForegroundColor Cyan
Write-Host "  $Entidad`Controller.cs"

Write-Host ""
Write-Host "Domain:" -ForegroundColor Cyan
Write-Host "  $Entidad.cs"