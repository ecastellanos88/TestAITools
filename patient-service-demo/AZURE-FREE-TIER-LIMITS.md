# Azure Free Tier - Costs and Limitations

## 📋 Overview

This document details the **costs, limitations, and best practices** for running the Patient Management Portal on Azure's free tier.

**Date**: 2026-01-20  
**Monthly Cost**: $0 (within free tier limits)  
**Recommended For**: Development, testing, demos, learning

---

## 💰 Cost Breakdown

### Free Tier Services (12 Months)

| Service | Tier | Monthly Cost | Free Allowance |
|---------|------|--------------|----------------|
| **Azure App Service** | Free F1 | $0 | 10 web apps, 1 GB storage |
| **Azure Static Web Apps** | Free | $0 | 100 GB bandwidth, 2 apps |
| **Azure SQL Database** | Free | $0 | 1 database, 32 MB storage (optional) |
| **Application Insights** | Free | $0 | 5 GB data/month (optional) |
| **Azure Cosmos DB** | Free | $0 | 1000 RU/s, 25 GB storage (optional) |

**Total Monthly Cost**: **$0** ✅

---

## ⚠️ Free Tier Limitations

### Azure App Service (Free F1) - Backend

| Limitation | Value | Impact |
|------------|-------|--------|
| **CPU Time** | 60 minutes/day | App stops after limit reached |
| **Memory** | 1 GB RAM | Sufficient for .NET 9 API |
| **Storage** | 1 GB | Enough for application files |
| **Instances** | 1 shared instance | No scaling, shared with other apps |
| **Always On** | ❌ Not available | App sleeps after 20 min inactivity |
| **Custom Domain** | ❌ Not available | Uses .azurewebsites.net |
| **SSL** | ✅ Included | HTTPS works on .azurewebsites.net |
| **Deployment Slots** | ❌ Not available | No staging environment |
| **Auto Scale** | ❌ Not available | Fixed 1 instance |

**Key Impacts**:
- ⚠️ **Cold Start**: First request after sleep takes 10-30 seconds
- ⚠️ **Daily Limit**: App stops if CPU time exceeds 60 minutes/day
- ⚠️ **Shared Resources**: Performance may vary

---

### Azure Static Web Apps (Free) - Frontend

| Limitation | Value | Impact |
|------------|-------|--------|
| **Bandwidth** | 100 GB/month | ~3.3 GB/day |
| **Storage** | 0.5 GB | Enough for Angular build |
| **Custom Domains** | ✅ 2 domains | Can use your own domain |
| **SSL** | ✅ Included | HTTPS automatic |
| **API Functions** | ❌ Not available | Use separate backend |
| **Authentication** | ✅ Included | Azure AD, GitHub, etc. |
| **Staging Environments** | ✅ 3 environments | PR previews included |

**Key Impacts**:
- ✅ **No Cold Start**: Frontend always available
- ✅ **Global CDN**: Fast worldwide
- ⚠️ **Bandwidth Limit**: Monitor if high traffic

---

## 📊 Usage Monitoring

### How to Check Your Usage

#### 1. Azure App Service (Backend)

**Via Azure Portal**:
1. Go to your App Service
2. Click **"Metrics"**
3. Select metrics:
   - **CPU Time**: Shows daily usage (max 60 min)
   - **Memory Working Set**: RAM usage
   - **Http Server Errors**: Error rate

**Via Azure CLI**:
```bash
# Get CPU time usage
az monitor metrics list \
  --resource /subscriptions/{subscription-id}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{app-name} \
  --metric "CpuTime" \
  --start-time 2026-01-20T00:00:00Z \
  --end-time 2026-01-20T23:59:59Z
```

#### 2. Azure Static Web Apps (Frontend)

**Via Azure Portal**:
1. Go to your Static Web App
2. Click **"Metrics"**
3. Select metrics:
   - **Data Out**: Bandwidth usage
   - **Requests**: Number of requests

---

## 🚨 What Happens When You Hit Limits?

### Scenario 1: CPU Time Limit Exceeded (60 min/day)

**What Happens**:
- ❌ App stops responding
- ❌ Returns HTTP 403 error
- ✅ Resets at midnight UTC

**Solutions**:
1. **Optimize code** to reduce CPU usage
2. **Upgrade to Basic tier** ($13/month for unlimited CPU)
3. **Use caching** to reduce processing

**Example Error**:
```
HTTP 403 - Forbidden
You do not have permission to view this directory or page.
Reason: CPU quota exceeded for the day.
```

---

### Scenario 2: Bandwidth Limit Exceeded (100 GB/month)

**What Happens**:
- ⚠️ Additional bandwidth charged at $0.15/GB
- ⚠️ You'll receive email notification

**Solutions**:
1. **Optimize images** and assets
2. **Enable compression** (gzip)
3. **Use CDN caching** (already included)
4. **Monitor usage** regularly

**Cost Example**:
- 100 GB free
- 150 GB used = 50 GB overage
- Cost: 50 GB × $0.15 = **$7.50**

---

### Scenario 3: App Sleep (20 Minutes Inactivity)

**What Happens**:
- ✅ App goes to sleep (saves resources)
- ⏱️ First request takes 10-30 seconds (cold start)
- ✅ Subsequent requests are fast

**Solutions**:
1. **Accept cold starts** (free tier limitation)
2. **Use keep-alive service** (ping every 15 min)
3. **Upgrade to Basic tier** ($13/month for "Always On")

---

## 💡 Optimization Tips

### 1. Reduce CPU Usage

**Backend (.NET)**:
```csharp
// ✅ Use in-memory caching
services.AddMemoryCache();

// ✅ Use async/await properly
public async Task<IEnumerable<Patient>> GetAllAsync()
{
    return await _repository.GetAllAsync();
}

// ❌ Avoid synchronous blocking
// var result = _repository.GetAllAsync().Result; // DON'T DO THIS
```

**Frontend (Angular)**:
```typescript
// ✅ Use OnPush change detection
@Component({
  changeDetection: ChangeDetectionStrategy.OnPush
})

// ✅ Lazy load modules
const routes: Routes = [
  { path: 'patients', loadChildren: () => import('./patients/patients.module') }
];
```

---

### 2. Reduce Bandwidth Usage

**Frontend Optimization**:
```bash
# Enable production build optimizations
ng build --configuration production

# Results in:
# - Minification
# - Tree shaking
# - Compression
# - Bundle optimization
```

**Backend Optimization**:
```csharp
// Enable response compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
```

---

### 3. Keep Backend Awake (Optional)

**Option A: Azure Logic Apps (Free)**

1. Create Logic App (Consumption plan - free)
2. Add HTTP trigger
3. Schedule: Every 15 minutes
4. Action: HTTP GET to your API

**Option B: External Service (UptimeRobot)**

1. Sign up: https://uptimerobot.com (free)
2. Add monitor for your API URL
3. Check interval: 5 minutes

**Cost**: $0 (both options are free)

---

## 📈 When to Upgrade?

### Upgrade Triggers

Consider upgrading when:

1. **CPU Limit Hit Regularly**
   - Upgrade to: **Basic B1** ($13/month)
   - Benefits: Unlimited CPU, Always On, 1.75 GB RAM

2. **Need Custom Domain for Backend**
   - Upgrade to: **Basic B1** ($13/month)
   - Benefits: Custom domain, SSL certificate support

3. **Need Staging Environment**
   - Upgrade to: **Standard S1** ($70/month)
   - Benefits: 5 deployment slots, auto-scale

4. **Production Traffic**
   - Upgrade to: **Premium P1V2** ($146/month)
   - Benefits: Better performance, SLA, auto-scale

---

## 🎯 Cost Comparison

| Tier | Monthly Cost | CPU | RAM | Always On | Custom Domain | Best For |
|------|--------------|-----|-----|-----------|---------------|----------|
| **Free F1** | $0 | 60 min/day | 1 GB | ❌ | ❌ | Development, demos |
| **Basic B1** | $13 | Unlimited | 1.75 GB | ✅ | ✅ | Small apps, testing |
| **Standard S1** | $70 | Unlimited | 1.75 GB | ✅ | ✅ | Production, staging |
| **Premium P1V2** | $146 | Unlimited | 3.5 GB | ✅ | ✅ | High traffic, SLA |

---

## 🔔 Set Up Cost Alerts

### Create Budget Alert

1. Go to **Azure Portal** → **Cost Management + Billing**
2. Click **"Budgets"**
3. Click **"Add"**
4. Configure:
   - **Name**: "Free Tier Alert"
   - **Amount**: $5
   - **Alert at**: 80% ($4)
   - **Email**: your-email@example.com
5. Click **"Create"**

**You'll receive email when costs exceed $4**

---

## 📚 Additional Resources

- [Azure Free Account FAQ](https://azure.microsoft.com/free/free-account-faq/)
- [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/)
- [Azure Cost Management](https://azure.microsoft.com/services/cost-management/)
- [App Service Pricing](https://azure.microsoft.com/pricing/details/app-service/)
- [Static Web Apps Pricing](https://azure.microsoft.com/pricing/details/app-service/static/)

---

## ✅ Summary

**Free Tier is Perfect For**:
- ✅ Learning Azure
- ✅ Development and testing
- ✅ Demos and prototypes
- ✅ Low-traffic applications
- ✅ Personal projects

**Upgrade When**:
- ⚠️ Hitting CPU limits regularly
- ⚠️ Need custom domain for backend
- ⚠️ Need "Always On" (no cold starts)
- ⚠️ Production traffic
- ⚠️ Need SLA guarantees

**Your Current Setup**: **$0/month** 🎉

