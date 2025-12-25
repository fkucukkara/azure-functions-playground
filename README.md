# Azure Functions 101 Playground 🚀

A simple, educational Azure Functions playground project for learning and experimenting with Azure Functions using the .NET isolated worker model.

## 📋 Overview

This project demonstrates the basics of Azure Functions v4 with the .NET isolated worker model. It's designed as a learning resource for developers getting started with serverless computing on Azure.

### Features

- ✅ HTTP-triggered Azure Function
- ✅ Blob-triggered Function with output binding (copy blob between containers)
- ✅ .NET Framework 4.8 with isolated worker model
- ✅ Application Insights integration
- ✅ Ready for local development and Azure deployment

## 🛠️ Prerequisites

Before you begin, ensure you have the following installed:

- [.NET Framework 4.8 SDK](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [Azure Functions Core Tools v4](https://docs.microsoft.com/azure/azure-functions/functions-run-local)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with C# extension
- [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli) (for deployment)
- [Azurite](https://docs.microsoft.com/azure/storage/common/storage-use-azurite) or Azure Storage Emulator (for local development)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/FunctionApp101.git
cd FunctionApp101
```

### 2. Configure Local Settings

Create a `local.settings.json` file in the `FunctionApp101` folder:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "APPLICATIONINSIGHTS_CONNECTION_STRING": "<your-connection-string>"
  }
}
```

> **Note:** For local development, `UseDevelopmentStorage=true` requires [Azurite](https://docs.microsoft.com/azure/storage/common/storage-use-azurite) to be running.

### 3. Run Locally

Using Azure Functions Core Tools:

```bash
cd FunctionApp101
func start
```

Or using Visual Studio:
- Open `FunctionApp101.slnx`
- Press `F5` to start debugging

### 4. Test the Function

Once running, test the HTTP endpoint:

```bash
curl http://localhost:7071/api/Function1
```

Or open in your browser: `http://localhost:7071/api/Function1`

Expected response:
```
Welcome to Azure Functions!
```

### Test the Blob Copy Function

1. Ensure Azurite is running for local storage emulation
2. Create a container named `source-container` in your storage
3. Upload a blob to `source-container`
4. The function will automatically copy it to `destination-container`

You can use Azure Storage Explorer or Azure CLI:
```bash
# Create containers (if using Azure Storage)
az storage container create --name source-container --connection-string "<your-connection-string>"
az storage container create --name destination-container --connection-string "<your-connection-string>"

# Upload a test file
az storage blob upload --container-name source-container --file test.txt --name test.txt --connection-string "<your-connection-string>"
```

## 📁 Project Structure

```
FunctionApp101/
├── FunctionApp101.slnx          # Solution file
├── .gitignore                   # Git ignore rules
├── README.md                    # This file
├── LICENSE                      # MIT License
└── FunctionApp101/              # Function App project
    ├── Function1.cs             # HTTP trigger function
    ├── BlobCopyFunction.cs      # Blob trigger with output binding
    ├── Program.cs               # Application entry point
    ├── FunctionApp101.csproj    # Project file
    ├── host.json                # Host configuration
    └── local.settings.json      # Local settings (not in git)
```

## 📚 Learning Resources

### About Azure Functions

Azure Functions is a serverless compute service that enables you to run event-triggered code without having to explicitly provision or manage infrastructure.

### Key Concepts Demonstrated

1. **HTTP Triggers**: Functions that respond to HTTP requests
2. **Blob Triggers**: Functions that respond to blob storage events
3. **Output Bindings**: Declarative way to write data to external services
4. **Isolated Worker Model**: Out-of-process execution model for better flexibility
5. **Dependency Injection**: Using `ILoggerFactory` for logging
6. **Application Insights**: Telemetry and monitoring integration

### Useful Links

- [Azure Functions Documentation](https://docs.microsoft.com/azure/azure-functions/)
- [.NET Isolated Worker Guide](https://docs.microsoft.com/azure/azure-functions/dotnet-isolated-process-guide)
- [Azure Functions Core Tools](https://docs.microsoft.com/azure/azure-functions/functions-run-local)
- [Application Insights for Azure Functions](https://docs.microsoft.com/azure/azure-functions/functions-monitoring)

## 🔧 Configuration

### host.json

The `host.json` file contains global configuration options:

```json
{
  "version": "2.0",
  "logging": {
    "applicationInsights": {
      "samplingSettings": {
        "isEnabled": true,
        "excludedTypes": "Request"
      },
      "enableLiveMetricsFilters": true
    }
  }
}
```

### Environment Variables

| Variable | Description |
|----------|-------------|
| `AzureWebJobsStorage` | Connection string for Azure Storage |
| `FUNCTIONS_WORKER_RUNTIME` | Set to `dotnet-isolated` |
| `APPLICATIONINSIGHTS_CONNECTION_STRING` | Application Insights connection string |

## 🚢 Deployment

### Deploy to Azure using Azure CLI

1. Create a resource group:
```bash
az group create --name rg-functionapp101 --location eastus
```

2. Create a storage account:
```bash
az storage account create --name stfunctionapp101 --location eastus --resource-group rg-functionapp101 --sku Standard_LRS
```

3. Create a function app:
```bash
az functionapp create --resource-group rg-functionapp101 --consumption-plan-location eastus --runtime dotnet-isolated --functions-version 4 --name functionapp101 --storage-account stfunctionapp101
```

4. Deploy the function:
```bash
func azure functionapp publish functionapp101
```

### Deploy using Visual Studio

> **💡 Tip:** For a seamless deployment experience, create your Azure Function App in the Azure Portal first (or using Azure CLI), then use Visual Studio's publish feature to deploy to the existing Function App. This ensures all Azure resources are properly configured before deployment.

1. Right-click on the project in Solution Explorer
2. Select **Publish**
3. Choose **Azure** → **Azure Function App (Windows)**
4. Select **Select Existing** to choose your pre-created Function App
5. Follow the wizard to complete the deployment

## 🧪 Extending the Project

### Adding a New Function

1. Create a new `.cs` file in the project
2. Add the function class with `[Function]` attribute:

```csharp
public class MyNewFunction
{
    private readonly ILogger _logger;

    public MyNewFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<MyNewFunction>();
    }

    [Function("MyNewFunction")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        _logger.LogInformation("Processing request...");
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString("Hello from MyNewFunction!");
        return response;
    }
}
```

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Azure Functions Team](https://github.com/Azure/azure-functions-host)
- [Microsoft Learn](https://learn.microsoft.com/azure/azure-functions/)

---

**Happy Coding!** 🎉

If you find this project helpful, please give it a ⭐ on GitHub!
