using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wise.IsolatedApp.Domain;

var builder = FunctionsApplication.CreateBuilder(args);

// Enable full web pipeline (for HttpTrigger)
builder.ConfigureFunctionsWebApplication();

// Load config from Azure Key Vault
var keyVaultUrl = builder.Configuration["KeyVaultUrl"];
if (!string.IsNullOrEmpty(keyVaultUrl))
{
    var credential = new DefaultAzureCredential();
    var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);
    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
}

// DI Services
builder.Services.AddSingleton<ICustomRepository, CustomRepository>();

// Application Insights (uyumlu paketle!)
builder.Services.AddSingleton<ITelemetryInitializer, OperationCorrelationTelemetryInitializer>();
builder.Services.AddSingleton<TelemetryConfiguration>(provider =>
{
    var config = TelemetryConfiguration.CreateDefault();
    config.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
    return config;
});
builder.Services.AddApplicationInsightsTelemetryWorkerService();

builder.Build().Run();
