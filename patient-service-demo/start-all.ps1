# Script para iniciar el backend y frontend simultáneamente

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Patient Management System - Startup  " -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Iniciar el backend en una nueva ventana de PowerShell
Write-Host "Iniciando Backend (.NET API)..." -ForegroundColor Yellow
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\src\PatientService.API'; dotnet run"

# Esperar un momento para que el backend inicie
Write-Host "Esperando 5 segundos para que el backend inicie..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

# Iniciar el frontend en una nueva ventana de PowerShell
Write-Host "Iniciando Frontend (Angular)..." -ForegroundColor Yellow
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\patient-portal'; ng serve --open"

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "  Aplicaciones iniciadas correctamente  " -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""
Write-Host "Backend API: http://localhost:5000" -ForegroundColor Cyan
Write-Host "Frontend:    http://localhost:4200" -ForegroundColor Cyan
Write-Host ""
Write-Host "Presiona cualquier tecla para cerrar esta ventana..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

