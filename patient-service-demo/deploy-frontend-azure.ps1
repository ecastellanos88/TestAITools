# ============================================================================
# Azure Frontend Deployment Script (PowerShell)
# ============================================================================
# Description: Deploys Angular 18 app to Azure Static Web Apps (Free tier)
# Prerequisites: Azure CLI installed, logged in, and GitHub repository
# Usage: .\deploy-frontend-azure.ps1 -BackendUrl "https://your-api.azurewebsites.net"
# ============================================================================

param(
    [Parameter(Mandatory=$true)]
    [string]$BackendUrl,
    
    [string]$ResourceGroup = "patient-portal-rg",
    [string]$Location = "eastus2",
    [string]$AppName = "patient-portal-$(Get-Random -Minimum 1000 -Maximum 9999)",
    [string]$GitHubRepo = "",
    [string]$GitHubBranch = "main"
)

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Azure Frontend Deployment Script" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Validate Backend URL
if (-not $BackendUrl.StartsWith("https://")) {
    Write-Host "ERROR: Backend URL must start with https://" -ForegroundColor Red
    exit 1
}

# Check if Azure CLI is installed
Write-Host "[1/6] Checking Azure CLI..." -ForegroundColor Yellow
if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: Azure CLI is not installed!" -ForegroundColor Red
    Write-Host "Install from: https://docs.microsoft.com/cli/azure/install-azure-cli" -ForegroundColor Red
    exit 1
}
Write-Host "✓ Azure CLI found" -ForegroundColor Green

# Check if logged in
Write-Host "[2/6] Checking Azure login..." -ForegroundColor Yellow
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

# Update environment.ts with Backend URL
Write-Host "[3/6] Updating environment configuration..." -ForegroundColor Yellow
$envPath = Join-Path $PSScriptRoot "patient-portal\src\environments\environment.ts"
if (-not (Test-Path $envPath)) {
    Write-Host "ERROR: environment.ts not found at: $envPath" -ForegroundColor Red
    exit 1
}

$envContent = @"
export const environment = {
  production: true,
  apiUrl: '$BackendUrl/api'
};
"@

Set-Content -Path $envPath -Value $envContent -Force
Write-Host "✓ Environment configured with API URL: $BackendUrl/api" -ForegroundColor Green

# Check if GitHub repo is configured
Write-Host "[4/6] Checking GitHub repository..." -ForegroundColor Yellow
if ([string]::IsNullOrEmpty($GitHubRepo)) {
    # Try to get from git remote
    Push-Location $PSScriptRoot
    $remoteUrl = git remote get-url origin 2>$null
    Pop-Location
    
    if ($remoteUrl) {
        # Extract owner/repo from URL
        if ($remoteUrl -match "github\.com[:/](.+/.+?)(\.git)?$") {
            $GitHubRepo = $matches[1] -replace "\.git$", ""
            Write-Host "✓ Detected GitHub repo: $GitHubRepo" -ForegroundColor Green
        } else {
            Write-Host "ERROR: Could not parse GitHub repository from: $remoteUrl" -ForegroundColor Red
            Write-Host "Please specify -GitHubRepo parameter (format: owner/repo)" -ForegroundColor Red
            exit 1
        }
    } else {
        Write-Host "ERROR: No GitHub repository configured!" -ForegroundColor Red
        Write-Host "Please push code to GitHub first or specify -GitHubRepo parameter" -ForegroundColor Red
        exit 1
    }
}

# Create Static Web App
Write-Host "[5/6] Creating Static Web App..." -ForegroundColor Yellow
Write-Host "This will open GitHub for authorization..." -ForegroundColor Yellow

$createResult = az staticwebapp create `
    --name $AppName `
    --resource-group $ResourceGroup `
    --location $Location `
    --source "https://github.com/$GitHubRepo" `
    --branch $GitHubBranch `
    --app-location "/patient-portal" `
    --output-location "dist/patient-portal/browser" `
    --login-with-github `
    2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Static Web App created: $AppName" -ForegroundColor Green
} else {
    Write-Host "ERROR: Failed to create Static Web App" -ForegroundColor Red
    Write-Host $createResult -ForegroundColor Red
    exit 1
}

# Get the Static Web App URL
Write-Host "[6/6] Getting deployment URL..." -ForegroundColor Yellow
$appUrl = az staticwebapp show `
    --name $AppName `
    --resource-group $ResourceGroup `
    --query "defaultHostname" `
    --output tsv

if ($LASTEXITCODE -eq 0) {
    $fullUrl = "https://$appUrl"
    Write-Host "✓ Deployment URL: $fullUrl" -ForegroundColor Green
} else {
    Write-Host "WARNING: Could not retrieve URL (check Azure Portal)" -ForegroundColor Yellow
    $fullUrl = "https://$AppName.azurestaticapps.net"
}

# Display results
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Deployment Complete!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Frontend URL: $fullUrl" -ForegroundColor Green
Write-Host "Backend URL: $BackendUrl" -ForegroundColor Green
Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Yellow
Write-Host "1. Wait 2-3 minutes for GitHub Actions to complete deployment" -ForegroundColor White
Write-Host "2. Update backend CORS to allow: $fullUrl" -ForegroundColor White
Write-Host "   Run: az webapp cors add --name YOUR_API_NAME --resource-group $ResourceGroup --allowed-origins $fullUrl" -ForegroundColor White
Write-Host "3. Test application: $fullUrl" -ForegroundColor White
Write-Host ""
Write-Host "GitHub Actions: https://github.com/$GitHubRepo/actions" -ForegroundColor Cyan
Write-Host "Azure Portal: https://portal.azure.com" -ForegroundColor Cyan
Write-Host ""

