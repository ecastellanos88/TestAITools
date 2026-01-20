# Azure Deployment Checklist

## 📋 Pre-Deployment Checklist

Use this checklist to ensure you're ready to deploy to Azure.

---

## ✅ Prerequisites

### Azure Account
- [ ] Created Azure free account at https://azure.microsoft.com/free
- [ ] Verified email address
- [ ] Logged into Azure Portal (https://portal.azure.com)
- [ ] Confirmed free tier services are available in your region

### GitHub Account (for CI/CD)
- [ ] Created GitHub account
- [ ] Code pushed to GitHub repository
- [ ] Repository is accessible (public or private with permissions)

### Local Development Environment
- [ ] .NET 9 SDK installed
- [ ] Node.js 20.x installed
- [ ] Azure CLI installed (optional but recommended)
- [ ] Git installed
- [ ] PowerShell available (Windows)

### Application Status
- [ ] Backend builds successfully (`dotnet build`)
- [ ] Backend tests pass (`dotnet test`) - 13 tests
- [ ] Frontend builds successfully (`npm run build`)
- [ ] Frontend tests pass (`npm test`) - 8 tests
- [ ] Application runs locally without errors

---

## 🚀 Deployment Checklist

### Option 1: Manual Deployment via Azure Portal

#### Backend Deployment
- [ ] Created Resource Group in Azure Portal
- [ ] Created App Service Plan (Free F1)
- [ ] Created App Service (Web App)
- [ ] Configured runtime stack (.NET 9)
- [ ] Deployed code (GitHub, ZIP, or FTP)
- [ ] Verified backend URL works: `https://YOUR_APP.azurewebsites.net/api/patients`
- [ ] Noted backend URL for frontend configuration

#### Frontend Deployment
- [ ] Updated `environment.ts` with backend URL
- [ ] Created Static Web App in Azure Portal
- [ ] Connected to GitHub repository
- [ ] Configured build settings:
  - App location: `/patient-portal`
  - Output location: `dist/patient-portal/browser`
- [ ] Waited for GitHub Actions to complete deployment
- [ ] Verified frontend URL works: `https://YOUR_APP.azurestaticapps.net`

#### CORS Configuration
- [ ] Opened App Service (backend) in Azure Portal
- [ ] Navigated to CORS settings
- [ ] Added frontend URL to allowed origins
- [ ] Saved CORS configuration
- [ ] Tested API calls from frontend

#### Final Testing
- [ ] Opened frontend URL in browser
- [ ] Verified patient list loads
- [ ] Created a test patient
- [ ] Verified patient appears in list
- [ ] Checked browser console for errors
- [ ] Tested on mobile device (responsive design)

---

### Option 2: Automated Deployment via Scripts

#### Backend Script Deployment
- [ ] Installed Azure CLI
- [ ] Logged in: `az login`
- [ ] Reviewed script: `deploy-backend-azure.ps1`
- [ ] Ran script: `.\deploy-backend-azure.ps1`
- [ ] Noted backend URL from script output
- [ ] Verified deployment in Azure Portal

#### Frontend Script Deployment
- [ ] Reviewed script: `deploy-frontend-azure.ps1`
- [ ] Ran script with backend URL: `.\deploy-frontend-azure.ps1 -BackendUrl "https://YOUR_API.azurewebsites.net"`
- [ ] Authorized GitHub access when prompted
- [ ] Waited for deployment to complete
- [ ] Noted frontend URL from script output
- [ ] Verified deployment in Azure Portal

#### Post-Deployment
- [ ] Updated backend CORS with frontend URL
- [ ] Tested application end-to-end
- [ ] Verified all features work

---

### Option 3: CI/CD with GitHub Actions

#### GitHub Secrets Configuration
- [ ] Downloaded App Service publish profile
- [ ] Added `AZURE_WEBAPP_PUBLISH_PROFILE` secret to GitHub
- [ ] Copied Static Web Apps deployment token
- [ ] Added `AZURE_STATIC_WEB_APPS_API_TOKEN` secret to GitHub

#### Workflow Configuration
- [ ] Reviewed `.github/workflows/azure-deploy.yml`
- [ ] Updated `AZURE_WEBAPP_NAME` in workflow file
- [ ] Committed workflow file to repository
- [ ] Pushed to `main` branch

#### Deployment Monitoring
- [ ] Opened GitHub Actions tab
- [ ] Watched workflow execution
- [ ] Verified all jobs completed successfully
- [ ] Checked deployment logs for errors

#### Post-Deployment
- [ ] Verified backend health check passed
- [ ] Verified frontend deployed successfully
- [ ] Tested application in production

---

## 🔍 Post-Deployment Verification

### Backend Verification
- [ ] API responds to GET requests: `curl https://YOUR_API.azurewebsites.net/api/patients`
- [ ] API responds to POST requests (create patient)
- [ ] CORS headers present in responses
- [ ] No 500 errors in App Service logs
- [ ] SSL certificate valid (HTTPS works)

### Frontend Verification
- [ ] Page loads without errors
- [ ] Patient list displays correctly
- [ ] "Add New Patient" button works
- [ ] Form validation works
- [ ] Patient creation succeeds
- [ ] New patient appears in list
- [ ] Responsive design works on mobile
- [ ] No console errors in browser

### Performance Verification
- [ ] Initial page load < 3 seconds
- [ ] API response time < 1 second
- [ ] No cold start issues (or acceptable)
- [ ] Images and assets load correctly

---

## 📊 Monitoring Setup

### Azure Portal Monitoring
- [ ] Enabled Application Insights (optional, free tier available)
- [ ] Set up availability tests
- [ ] Configured alert rules for errors
- [ ] Set up budget alerts ($5 threshold)

### Usage Monitoring
- [ ] Checked App Service metrics (CPU time)
- [ ] Checked Static Web Apps metrics (bandwidth)
- [ ] Set up daily usage review

---

## 🔒 Security Checklist

### SSL/HTTPS
- [ ] Backend uses HTTPS
- [ ] Frontend uses HTTPS
- [ ] No mixed content warnings
- [ ] SSL certificates valid

### CORS
- [ ] CORS configured correctly
- [ ] Only frontend URL allowed
- [ ] No wildcard (*) in production

### Secrets Management
- [ ] No secrets in source code
- [ ] GitHub secrets configured
- [ ] Environment variables used for sensitive data

---

## 💰 Cost Management

### Free Tier Verification
- [ ] App Service on Free F1 tier
- [ ] Static Web Apps on Free tier
- [ ] No paid services enabled
- [ ] Budget alert configured

### Usage Monitoring
- [ ] CPU time < 60 minutes/day
- [ ] Bandwidth < 100 GB/month
- [ ] No unexpected charges

---

## 📚 Documentation

### Documentation Review
- [ ] Read `AZURE-DEPLOYMENT-GUIDE.md`
- [ ] Read `AZURE-DEPLOYMENT-SUMMARY.md`
- [ ] Read `AZURE-FREE-TIER-LIMITS.md`
- [ ] Bookmarked Azure Portal
- [ ] Saved deployment URLs

### Team Communication
- [ ] Shared deployment URLs with team
- [ ] Documented any issues encountered
- [ ] Updated project README with deployment info

---

## 🎯 Success Criteria

All of the following should be true:

- ✅ Backend deployed and accessible via HTTPS
- ✅ Frontend deployed and accessible via HTTPS
- ✅ CORS configured correctly
- ✅ Application works end-to-end
- ✅ No errors in logs
- ✅ Cost is $0/month (free tier)
- ✅ CI/CD pipeline working (if configured)
- ✅ Team can access and test application

---

## 🆘 Troubleshooting

If something doesn't work, check:

1. **Backend Issues**:
   - [ ] Check App Service logs in Azure Portal
   - [ ] Verify .NET 9 runtime configured
   - [ ] Check CORS settings
   - [ ] Verify deployment succeeded

2. **Frontend Issues**:
   - [ ] Check browser console for errors
   - [ ] Verify API URL in environment.ts
   - [ ] Check GitHub Actions logs
   - [ ] Verify build output location

3. **CORS Issues**:
   - [ ] Verify frontend URL in CORS settings
   - [ ] Check for typos in URLs
   - [ ] Ensure HTTPS (not HTTP)

4. **Cost Issues**:
   - [ ] Verify Free F1 tier selected
   - [ ] Check for additional services
   - [ ] Review Azure billing dashboard

---

## 📞 Support Resources

- 📖 **Documentation**: See `AZURE-DEPLOYMENT-GUIDE.md`
- 🌐 **Azure Portal**: https://portal.azure.com
- 📚 **Azure Docs**: https://docs.microsoft.com/azure
- 💬 **Azure Forums**: https://docs.microsoft.com/answers/products/azure

---

**Deployment Complete?** 🎉

If all checkboxes are checked, congratulations! Your application is live on Azure!

**Next Steps**:
- Share the URL with stakeholders
- Set up monitoring and alerts
- Plan for future enhancements
- Consider upgrading to paid tier if needed

