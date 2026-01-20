# ============================================================================
# Azure Backend Deployment Script (PowerShell)
# ============================================================================
# Description: Deploys .NET 9 API to Azure App Service (Free F1 tier)
# Prerequisites: Azure CLI installed and logged in
# Usage: .\deploy-backend-azure.ps1
# ============================================================================

param(
    [string]$ResourceGroup = "patient-portal-rg",
    [string]$Location = "eastus",
    [string]$AppServicePlan = "patient-api-plan",
    [string]$AppName = "patient-api-$(Get-Random -Minimum 1000 -Maximum 9999)",
    [string]$Runtime = "DOTNET:9.0"
)

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Azure Backend Deployment Script" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Check if Azure CLI is installed
Write-Host "[1/7] Checking Azure CLI..." -ForegroundColor Yellow
if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: Azure CLI is not installed!" -ForegroundColor Red
    Write-Host "Install from: https://docs.microsoft.com/cli/azure/install-azure-cli" -ForegroundColor Red
    exit 1
}
Write-Host "✓ Azure CLI found" -ForegroundColor Green

# Check if logged in
Write-Host "[2/7] Checking Azure login..." -ForegroundColor Yellow
$account = az account show 2>$null
if (-not $account) {
    Write-Host "Not logged in. Opening browser for login..." -ForegroundColor Yellow
    az login
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: Login failed!" -ForegroundColor Red
        exit 1
    }
}
Write-Host "✓ Logged in to Azure" -ForegroundColor Green

# Create Resource Group
Write-Host "[3/7] Creating Resource Group..." -ForegroundColor Yellow
az group create --name $ResourceGroup --location $Location --output none
if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Resource Group created: $ResourceGroup" -ForegroundColor Green
} else {
    Write-Host "ERROR: Failed to create Resource Group" -ForegroundColor Red
    exit 1
}

# Create App Service Plan (Free F1)
Write-Host "[4/7] Creating App Service Plan (Free F1)..." -ForegroundColor Yellow
az appservice plan create `
    --name $AppServicePlan `
    --resource-group $ResourceGroup `
    --sku FREE `
    --output none

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ App Service Plan created: $AppServicePlan (Free F1)" -ForegroundColor Green
} else {
    Write-Host "ERROR: Failed to create App Service Plan" -ForegroundColor Red
    exit 1
}

# Create Web App
Write-Host "[5/7] Creating Web App..." -ForegroundColor Yellow
az webapp create `
    --name $AppName `
    --resource-group $ResourceGroup `
    --plan $AppServicePlan `
    --runtime $Runtime `
    --output none

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Web App created: $AppName" -ForegroundColor Green
} else {
    Write-Host "ERROR: Failed to create Web App" -ForegroundColor Red
    exit 1
}

# Configure CORS (will be updated after frontend deployment)
Write-Host "[6/7] Configuring CORS..." -ForegroundColor Yellow
az webapp cors add `
    --name $AppName `
    --resource-group $ResourceGroup `
    --allowed-origins "http://localhost:4200" "https://*.azurestaticapps.net" `
    --output none

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ CORS configured" -ForegroundColor Green
} else {
    Write-Host "WARNING: CORS configuration failed (can be done manually)" -ForegroundColor Yellow
}

# Build and Deploy
Write-Host "[7/7] Building and deploying application..." -ForegroundColor Yellow
Write-Host "This may take 3-5 minutes..." -ForegroundColor Yellow

# Navigate to API project
$apiPath = Join-Path $PSScriptRoot "src\PatientService.API"
if (-not (Test-Path $apiPath)) {
    Write-Host "ERROR: API project not found at: $apiPath" -ForegroundColor Red
    exit 1
}

Push-Location $apiPath

# Build the project
Write-Host "Building .NET project..." -ForegroundColor Yellow
dotnet publish -c Release -o ./publish

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build failed!" -ForegroundColor Red
    Pop-Location
    exit 1
}

# Create deployment package
Write-Host "Creating deployment package..." -ForegroundColor Yellow
Compress-Archive -Path ./publish/* -DestinationPath ./deploy.zip -Force

# Deploy to Azure
Write-Host "Deploying to Azure..." -ForegroundColor Yellow
az webapp deployment source config-zip `
    --name $AppName `
    --resource-group $ResourceGroup `
    --src ./deploy.zip `
    --output none

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Deployment successful!" -ForegroundColor Green
} else {
    Write-Host "ERROR: Deployment failed!" -ForegroundColor Red
    Pop-Location
    exit 1
}

# Cleanup
Remove-Item ./deploy.zip -Force
Remove-Item ./publish -Recurse -Force
Pop-Location

# Display results
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Deployment Complete!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Backend URL: https://$AppName.azurewebsites.net" -ForegroundColor Green
Write-Host "API Endpoint: https://$AppName.azurewebsites.net/api/patients" -ForegroundColor Green
Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Yellow
Write-Host "1. Test API: curl https://$AppName.azurewebsites.net/api/patients" -ForegroundColor White
Write-Host "2. Update frontend environment.ts with API URL" -ForegroundColor White
Write-Host "3. Deploy frontend using deploy-frontend-azure.ps1" -ForegroundColor White
Write-Host "4. Update CORS with frontend URL after deployment" -ForegroundColor White
Write-Host ""
Write-Host "Azure Portal: https://portal.azure.com" -ForegroundColor Cyan
Write-Host ""

