# Azure CI/CD Setup Guide

## 📋 Overview

This guide explains how to set up **Continuous Integration and Continuous Deployment (CI/CD)** for the Patient Management Portal using **GitHub Actions** and **Azure**.

**Date**: 2026-01-20  
**Status**: ✅ Ready to Configure  
**Automation**: GitHub Actions (Free for public repos)

---

## 🎯 What Gets Automated

### Automatic Deployment Triggers

✅ **Push to `main` branch** → Automatic deployment  
✅ **Pull Request** → Build and test (no deployment)  
✅ **Manual trigger** → Deploy on demand

### CI/CD Pipeline Steps

1. **Backend (.NET 9 API)**:
   - ✅ Restore NuGet packages
   - ✅ Build project
   - ✅ Run unit tests (13 tests)
   - ✅ Publish to Azure App Service
   - ✅ Health check

2. **Frontend (Angular 18)**:
   - ✅ Install npm dependencies
   - ✅ Build production bundle
   - ✅ Deploy to Azure Static Web Apps
   - ✅ Health check

---

## 🔧 Prerequisites

Before setting up CI/CD, you need:

1. ✅ **GitHub Repository** with your code
2. ✅ **Azure App Service** (backend) - created
3. ✅ **Azure Static Web App** (frontend) - created
4. ✅ **GitHub Secrets** configured (see below)

---

## 🔐 Step 1: Configure GitHub Secrets

GitHub Secrets store sensitive information like Azure credentials.

### 1.1 Get Azure App Service Publish Profile

**For Backend Deployment**:

1. Go to **Azure Portal**: https://portal.azure.com
2. Navigate to your **App Service** (backend)
3. Click **"Get publish profile"** (top menu)
4. Download the `.PublishSettings` file
5. Open file in text editor and **copy all content**

### 1.2 Get Azure Static Web Apps API Token

**For Frontend Deployment**:

1. Go to **Azure Portal**: https://portal.azure.com
2. Navigate to your **Static Web App** (frontend)
3. Click **"Manage deployment token"**
4. **Copy the token** (long string)

### 1.3 Add Secrets to GitHub

1. Go to your **GitHub repository**
2. Click **Settings** → **Secrets and variables** → **Actions**
3. Click **"New repository secret"**
4. Add these secrets:

| Secret Name | Value | Description |
|-------------|-------|-------------|
| `AZURE_WEBAPP_PUBLISH_PROFILE` | (Paste publish profile XML) | Backend deployment credentials |
| `AZURE_STATIC_WEB_APPS_API_TOKEN` | (Paste token) | Frontend deployment token |

---

## 📝 Step 2: Update Workflow Configuration

### 2.1 Edit `.github/workflows/azure-deploy.yml`

Update these values in the workflow file:

```yaml
env:
  AZURE_WEBAPP_NAME: 'YOUR_BACKEND_APP_NAME'  # Change this!
```

**Example**:
```yaml
env:
  AZURE_WEBAPP_NAME: 'patient-api-demo'
```

### 2.2 Commit and Push

```bash
git add .github/workflows/azure-deploy.yml
git commit -m "Configure Azure CI/CD"
git push origin main
```

---

## 🚀 Step 3: Trigger First Deployment

### Option A: Automatic (Push to main)

```bash
# Make any change
echo "# CI/CD Configured" >> README.md
git add README.md
git commit -m "Trigger CI/CD"
git push origin main
```

### Option B: Manual Trigger

1. Go to **GitHub** → **Actions** tab
2. Click **"Deploy to Azure"** workflow
3. Click **"Run workflow"** button
4. Select branch: `main`
5. Click **"Run workflow"**

---

## 📊 Step 4: Monitor Deployment

### 4.1 Watch GitHub Actions

1. Go to **GitHub** → **Actions** tab
2. Click on the running workflow
3. Watch real-time logs:
   - ✅ Backend build and test
   - ✅ Backend deployment
   - ✅ Frontend build
   - ✅ Frontend deployment
   - ✅ Health checks

### 4.2 Check Deployment Status

**Expected Timeline**:
- Backend deployment: ~3-5 minutes
- Frontend deployment: ~2-3 minutes
- Total: ~5-8 minutes

**Success Indicators**:
- ✅ All jobs show green checkmarks
- ✅ Backend health check passes
- ✅ Frontend deployed successfully

---

## 🧪 Step 5: Verify Deployment

### 5.1 Test Backend

```bash
# Test API endpoint
curl https://YOUR_BACKEND_APP.azurewebsites.net/api/patients

# Expected: [] (empty array)
```

### 5.2 Test Frontend

1. Open: `https://YOUR_FRONTEND_APP.azurestaticapps.net`
2. Should see Patient Management Portal
3. Try creating a patient
4. Verify it appears in the list

---

## 🔄 How CI/CD Works

### Workflow Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                    Developer Workflow                        │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
                    ┌──────────────────┐
                    │  git push main   │
                    └──────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                    GitHub Actions Trigger                    │
└─────────────────────────────────────────────────────────────┘
                              │
                ┌─────────────┴─────────────┐
                ▼                           ▼
    ┌───────────────────┐       ┌───────────────────┐
    │  Backend Job      │       │  Frontend Job     │
    │  (deploy-backend) │       │  (deploy-frontend)│
    └───────────────────┘       └───────────────────┘
                │                           │
                ▼                           ▼
    ┌───────────────────┐       ┌───────────────────┐
    │ 1. Restore        │       │ 1. Install npm    │
    │ 2. Build          │       │ 2. Build Angular  │
    │ 3. Test (13)      │       │ 3. Deploy to SWA  │
    │ 4. Publish        │       └───────────────────┘
    │ 5. Deploy to AS   │
    └───────────────────┘
                │
                └─────────────┬─────────────┘
                              ▼
                    ┌──────────────────┐
                    │  Health Check    │
                    │  - Backend API   │
                    │  - Frontend URL  │
                    └──────────────────┘
                              │
                              ▼
                    ┌──────────────────┐
                    │  ✅ Deployed!    │
                    └──────────────────┘
```

---

## 🛠️ Customization Options

### Add Environment-Specific Deployments

Create separate workflows for dev/staging/prod:

```yaml
# .github/workflows/deploy-dev.yml
on:
  push:
    branches:
      - develop

# .github/workflows/deploy-prod.yml
on:
  push:
    branches:
      - main
```

### Add Slack Notifications

```yaml
- name: Notify Slack
  if: always()
  uses: 8398a7/action-slack@v3
  with:
    status: ${{ job.status }}
    webhook_url: ${{ secrets.SLACK_WEBHOOK }}
```

### Add Code Quality Checks

```yaml
- name: Run ESLint
  run: npm run lint
  working-directory: ./patient-portal

- name: Run Code Coverage
  run: dotnet test --collect:"XPlat Code Coverage"
  working-directory: ./tests
```

---

## 🆘 Troubleshooting

### Deployment Fails: "Publish profile not found"

**Solution**:
- Re-download publish profile from Azure Portal
- Update `AZURE_WEBAPP_PUBLISH_PROFILE` secret in GitHub
- Make sure there are no extra spaces or line breaks

### Deployment Fails: "Static Web Apps token invalid"

**Solution**:
- Get new deployment token from Azure Portal
- Update `AZURE_STATIC_WEB_APPS_API_TOKEN` secret
- Token expires after 90 days - regenerate if needed

### Tests Fail in CI but Pass Locally

**Solution**:
- Check test output in GitHub Actions logs
- Ensure all test dependencies are in `package.json` / `.csproj`
- Check for environment-specific issues (paths, timezones, etc.)

### Build Succeeds but App Doesn't Work

**Solution**:
- Check Azure App Service logs
- Verify environment variables are set
- Check CORS configuration
- Verify API URL in frontend environment.ts

---

## 📚 Additional Resources

- [GitHub Actions Documentation](https://docs.github.com/actions)
- [Azure App Service Deploy Action](https://github.com/Azure/webapps-deploy)
- [Azure Static Web Apps Deploy Action](https://github.com/Azure/static-web-apps-deploy)
- [GitHub Secrets Documentation](https://docs.github.com/actions/security-guides/encrypted-secrets)

---

**Ready to automate?** Follow the steps above and every push to `main` will automatically deploy to Azure! 🚀

