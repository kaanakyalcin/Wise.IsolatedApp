
# ⚡ Wise.Isolated (.NET 8 Azure Function App - Isolated Mode)

This project is a .NET 8 **Isolated Azure Function App** designed with best practices, including:

- ✅ Dependency Injection (`ICustomRepository`)
- 🔐 Azure Key Vault integration
- 📈 Application Insights telemetry (manual setup)
- 🌐 HTTP Trigger sample using `ILogger`, `IConfiguration`

---

## 🚀 Project Structure

```
Wise.Isolated/
│
├── Program.cs                  # Function App startup (CreateBuilder pattern)
├── Function1.cs                # Sample HTTP-triggered function
├── appsettings.json            # Config values (like KeyVaultUrl, AI connection string)
│
└── Wise.Isolated.Domain/
    ├── ICustomRepository.cs   # Interface for DI
    └── CustomRepository.cs    # Implementation of the repository
```

---

## 🛠 Technologies Used

| Tech                        | Description                           |
|-----------------------------|---------------------------------------|
| .NET 8                      | Latest LTS SDK                        |
| Azure Functions v4 (Isolated)| Functions runtime & hosting model   |
| Azure Key Vault             | Secure secret access via managed identity |
| Application Insights        | Manual telemetry setup using `ILogger` |
| Dependency Injection        | Built-in service registration         |

---

## ⚙️ Configuration (`appsettings.json`)

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "KeyVaultUrl": "https://your-keyvault.vault.azure.net/",
    "APPLICATIONINSIGHTS_CONNECTION_STRING": "<your-connection-string>"
  }
}
```

---

## 🔐 Azure Key Vault Access

Make sure your Function App has **Managed Identity** enabled and that it has **Secret Reader** permissions in your Key Vault (RBAC or Access Policies).

---

## 🧪 Local Testing

1. Install [Azure Functions Core Tools v4](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local):
   ```
   npm install -g azure-functions-core-tools@4 --unsafe-perm true
   ```

2. Run the function app:
   ```
   func start
   ```

3. Call the HTTP endpoint:
   ```
   http://localhost:7071/api/HttpTrigger
   ```

Expected response:

```
Telemetry is configured. Repo value: Value from CustomRepository
```

---

## 🧼 Logging & Application Insights

`ILogger` usage is automatically tracked by Application Insights. Logs will appear in the **Logs (Analytics)** section of your Application Insights resource.

---
