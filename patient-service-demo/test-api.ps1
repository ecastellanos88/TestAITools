# Script para probar la API de Patient Service
Write-Host "Iniciando Patient Service API..." -ForegroundColor Green

# Iniciar la aplicacion en segundo plano
$job = Start-Job -ScriptBlock {
    Set-Location "C:\Users\ErvinYamitCASTELLANO\Hackathon\TestAITools\patient-service-demo"
    dotnet run --project src/PatientService.API --urls "http://localhost:5000"
}

Write-Host "Esperando que la API inicie..." -ForegroundColor Yellow
Start-Sleep -Seconds 8

try {
    Write-Host "`nProbando endpoint de la API..." -ForegroundColor Green

    # Test: Crear un paciente
    Write-Host "`nTest: Crear un paciente" -ForegroundColor Cyan
    $patient = @{
        firstName = "Juan"
        lastName = "Pérez"
        dateOfBirth = "1990-05-15T00:00:00"
        email = "juan.perez@example.com"
        phoneNumber = "+1234567890"
        address = "Calle Principal 123"
    } | ConvertTo-Json

    $response = Invoke-RestMethod -Uri "http://localhost:5000/api/patients" `
        -Method Post `
        -Body $patient `
        -ContentType "application/json"

    Write-Host "OK - Paciente creado exitosamente!" -ForegroundColor Green
    Write-Host "  ID: $($response.id)" -ForegroundColor White
    Write-Host "  Nombre: $($response.firstName) $($response.lastName)" -ForegroundColor White
    Write-Host "  Email: $($response.email)" -ForegroundColor White
    Write-Host "  Fecha de Nacimiento: $($response.dateOfBirth)" -ForegroundColor White

    Write-Host "`nEl test paso exitosamente!" -ForegroundColor Green
    Write-Host "`nResumen:" -ForegroundColor Yellow
    Write-Host "  [OK] Crear paciente" -ForegroundColor Green

    Write-Host "`nPuedes acceder a Swagger UI en: http://localhost:5000/swagger" -ForegroundColor Cyan

} catch {
    Write-Host "`nError al probar la API: $_" -ForegroundColor Red
} finally {
    Write-Host "`nDeteniendo la aplicacion..." -ForegroundColor Yellow
    Stop-Job -Job $job
    Remove-Job -Job $job
    Write-Host "Aplicacion detenida" -ForegroundColor Green
}

