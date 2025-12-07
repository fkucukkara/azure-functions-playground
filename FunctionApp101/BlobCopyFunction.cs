using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp101
{
    public class BlobCopyFunction
    {
        private readonly ILogger _logger;

        public BlobCopyFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlobCopyFunction>();
        }

        [Function("BlobCopyFunction")]
        [BlobOutput("destination-container/{name}", Connection = "AzureWebJobsStorage")]
        public byte[] Run(
            [BlobTrigger("source-container/{name}", Connection = "AzureWebJobsStorage")] byte[] myBlob,
            string name)
        {
            _logger.LogInformation("Copying blob: {Name}, Size: {Size} Bytes", name, myBlob.Length);
            return myBlob;
        }
    }
}
