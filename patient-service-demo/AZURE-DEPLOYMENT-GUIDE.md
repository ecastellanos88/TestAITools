# Azure Deployment Guide - Free Tier

## 📋 Overview

This guide explains how to deploy the Patient Management Portal to Azure using **FREE tier services**. Perfect for development, testing, and learning purposes.

**Date**: 2026-01-20  
**Status**: ✅ Ready for Deployment  
**Cost**: $0/month (within free tier limits)

---

## 💰 Azure Free Tier Options

### Option 1: Azure Static Web Apps + Azure App Service (RECOMMENDED) ✅

**Best for**: Full-stack applications with separate frontend/backend

| Component | Service | Tier | Cost | Limits |
|-----------|---------|------|------|--------|
| **Frontend** | Azure Static Web Apps | Free | $0 | 100 GB bandwidth/month, Custom domains |
| **Backend** | Azure App Service | Free (F1) | $0 | 60 CPU minutes/day, 1 GB RAM, 1 GB storage |

**Pros**:
- ✅ Completely free
- ✅ Easy deployment from GitHub
- ✅ Automatic CI/CD
- ✅ Custom domains supported
- ✅ SSL certificates included

**Cons**:
- ⚠️ Backend limited to 60 CPU minutes/day
- ⚠️ App sleeps after 20 minutes of inactivity
- ⚠️ No custom domain for backend (uses .azurewebsites.net)

---

### Option 2: Azure Container Apps (FREE TIER) ✅

**Best for**: Containerized applications, microservices

| Component | Service | Tier | Cost | Limits |
|-----------|---------|------|------|--------|
| **Full Stack** | Azure Container Apps | Consumption | $0* | 180,000 vCPU-seconds, 360,000 GiB-seconds/month |

**Pros**:
- ✅ Free tier includes generous limits
- ✅ Auto-scaling (including to zero)
- ✅ Modern container-based deployment
- ✅ Supports both frontend and backend

**Cons**:
- ⚠️ Requires Docker knowledge
- ⚠️ More complex setup
- ⚠️ *Free tier has monthly limits (but very generous)

---

### Option 3: Azure App Service (Basic Tier) - PAID

**Best for**: Production applications with consistent traffic

| Component | Service | Tier | Cost | Limits |
|-----------|---------|------|------|--------|
| **Backend** | Azure App Service | Basic (B1) | ~$13/month | 1.75 GB RAM, 10 GB storage, Always on |
| **Frontend** | Azure Static Web Apps | Free | $0 | 100 GB bandwidth/month |

**Total Cost**: ~$13/month

---

## 🎯 Recommended Approach: Static Web Apps + App Service Free

This is the **best option for your free subscription**:

1. **Frontend (Angular)** → Azure Static Web Apps (Free)
2. **Backend (.NET 9 API)** → Azure App Service Free (F1)

### Why This Approach?

- ✅ **100% Free** (no credit card charges)
- ✅ **Easy to deploy** (GitHub integration)
- ✅ **Automatic CI/CD** (push to deploy)
- ✅ **SSL included** (HTTPS by default)
- ✅ **Good for demos** and learning

### Limitations to Know

1. **Backend sleeps after 20 minutes** of inactivity
   - First request after sleep takes ~10-30 seconds
   - Solution: Use a "keep-alive" ping service (also free)

2. **60 CPU minutes/day limit**
   - Enough for ~100-200 requests/day
   - Resets daily at midnight UTC

3. **No custom domain for backend**
   - Frontend: `your-app.azurestaticapps.net`
   - Backend: `your-api.azurewebsites.net`

---

## 📦 What You'll Need

### Prerequisites

1. **Azure Account** (Free tier)
   - Sign up at: https://azure.microsoft.com/free
   - $200 credit for 30 days (optional, not needed for free tier)
   - 12 months of free services

2. **GitHub Account** (Free)
   - For source code hosting
   - For automatic deployments

3. **Azure CLI** (Optional but recommended)
   - Install: https://docs.microsoft.com/cli/azure/install-azure-cli
   - Or use Azure Cloud Shell (browser-based)

4. **Git** (For version control)
   - Install: https://git-scm.com/downloads

---

## 🚀 Deployment Steps

### Step 1: Prepare Your Code

1. **Push code to GitHub**:
   ```bash
   cd patient-service-demo
   git init
   git add .
   git commit -m "Initial commit"
   git remote add origin https://github.com/YOUR_USERNAME/patient-service-demo.git
   git push -u origin main
   ```

2. **Update CORS settings** (backend):
   - Will be updated after frontend deployment
   - Frontend URL will be: `https://YOUR_APP_NAME.azurestaticapps.net`

---

### Step 2: Deploy Backend (.NET API)

#### Option A: Azure Portal (Easiest)

1. **Go to Azure Portal**: https://portal.azure.com
2. **Create App Service**:
   - Click "Create a resource"
   - Search "Web App"
   - Click "Create"

3. **Configure**:
   - **Subscription**: Your free subscription
   - **Resource Group**: Create new "patient-portal-rg"
   - **Name**: `patient-api-[yourname]` (must be globally unique)
   - **Publish**: Code
   - **Runtime stack**: .NET 9 (STS)
   - **Operating System**: Windows
   - **Region**: Choose closest to you
   - **Pricing Plan**: Free F1 (100% free)

4. **Review + Create** → **Create**

5. **Deploy Code**:
   - Go to your App Service
   - Click "Deployment Center"
   - Source: GitHub
   - Authorize GitHub
   - Select repository: `patient-service-demo`
   - Branch: `main`
   - Build provider: GitHub Actions
   - Click "Save"

6. **Configure Build**:
   - Azure will create a GitHub Actions workflow
   - Edit `.github/workflows/main_patient-api-yourname.yml`
   - Update path to API project

---

### Step 3: Deploy Frontend (Angular)

#### Option A: Azure Portal (Easiest)

1. **Go to Azure Portal**: https://portal.azure.com
2. **Create Static Web App**:
   - Click "Create a resource"
   - Search "Static Web App"
   - Click "Create"

3. **Configure**:
   - **Subscription**: Your free subscription
   - **Resource Group**: Use existing "patient-portal-rg"
   - **Name**: `patient-portal-[yourname]`
   - **Plan type**: Free
   - **Region**: Choose closest to you
   - **Source**: GitHub
   - **Authorize GitHub**
   - **Repository**: `patient-service-demo`
   - **Branch**: `main`
   - **Build Presets**: Angular
   - **App location**: `/patient-portal`
   - **Output location**: `dist/patient-portal/browser`

4. **Review + Create** → **Create**

5. **Wait for deployment** (~5 minutes)
   - Azure creates GitHub Actions workflow
   - Automatic build and deploy

---

### Step 4: Configure CORS

1. **Get Frontend URL**:
   - Go to Static Web App in Azure Portal
   - Copy URL: `https://YOUR_APP_NAME.azurestaticapps.net`

2. **Update Backend CORS**:
   - Go to App Service (backend)
   - Click "CORS"
   - Add allowed origin: `https://YOUR_APP_NAME.azurestaticapps.net`
   - Click "Save"

3. **Update Frontend API URL**:
   - Edit `patient-portal/src/environments/environment.ts`
   - Change `apiUrl` to: `https://YOUR_API_NAME.azurewebsites.net/api`
   - Commit and push (triggers auto-deploy)

---

## 🧪 Testing Deployment

### 1. Test Backend

```bash
# Test health endpoint
curl https://YOUR_API_NAME.azurewebsites.net/api/patients

# Should return empty array: []
```

### 2. Test Frontend

1. Open: `https://YOUR_APP_NAME.azurestaticapps.net`
2. Should see Patient Management Portal
3. Try creating a patient
4. Check if it appears in the list

---

## 📊 Monitoring Free Tier Usage

### Check App Service Usage

1. Go to App Service in Azure Portal
2. Click "Metrics"
3. Monitor:
   - CPU Time (60 minutes/day limit)
   - Memory usage
   - HTTP requests

### Check Static Web App Usage

1. Go to Static Web App in Azure Portal
2. Click "Metrics"
3. Monitor:
   - Bandwidth (100 GB/month limit)
   - Requests

---

## 💡 Tips for Staying Within Free Tier

### 1. Prevent Backend Sleep (Optional)

Use a free ping service to keep backend awake:

**Option A: Azure Logic Apps (Free tier)**
- Create Logic App with HTTP trigger
- Schedule: Every 15 minutes
- Action: HTTP GET to your API

**Option B: UptimeRobot (External, Free)**
- Sign up: https://uptimerobot.com
- Add monitor for your API URL
- Check interval: 5 minutes

### 2. Optimize for CPU Minutes

- Use in-memory repository (already implemented)
- Minimize complex operations
- Cache responses when possible

### 3. Monitor Usage

Set up alerts:
- Azure Portal → Monitor → Alerts
- Alert when CPU time > 50 minutes/day
- Email notification

---

## 🔒 Security Considerations

### Free Tier Security

1. **HTTPS**: ✅ Included by default
2. **Authentication**: ⚠️ Not implemented (add Azure AD B2C if needed)
3. **API Keys**: ⚠️ Not implemented (add if needed)
4. **Rate Limiting**: ⚠️ Not included in free tier

### Recommendations

- Don't store sensitive data (use free tier for demos only)
- Add authentication before production use
- Use environment variables for secrets

---

## 📝 Next Steps

After successful deployment:

1. ✅ **Add Custom Domain** (Optional, requires domain purchase)
2. ✅ **Set up Azure AD B2C** (Free tier available for authentication)
3. ✅ **Add Application Insights** (Free tier for monitoring)
4. ✅ **Configure CI/CD** (Already done with GitHub Actions)
5. ✅ **Add Database** (Azure SQL Free tier or Cosmos DB Free tier)

---

## 🆘 Troubleshooting

### Backend Returns 500 Error

**Solution**:
- Check App Service logs
- Go to App Service → Log stream
- Look for errors

### Frontend Can't Connect to Backend

**Solution**:
- Check CORS settings
- Verify API URL in environment.ts
- Check browser console for errors

### Deployment Failed

**Solution**:
- Check GitHub Actions logs
- Go to GitHub → Actions tab
- Click on failed workflow
- Review error messages

---

## 📚 Additional Resources

- [Azure Free Account](https://azure.microsoft.com/free)
- [Static Web Apps Documentation](https://docs.microsoft.com/azure/static-web-apps/)
- [App Service Documentation](https://docs.microsoft.com/azure/app-service/)
- [Azure CLI Documentation](https://docs.microsoft.com/cli/azure/)

---

**Ready to deploy?** Follow the steps above and your app will be live in ~15 minutes! 🚀

