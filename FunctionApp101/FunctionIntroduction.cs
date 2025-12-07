using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp101
{
    public class FunctionIntroduction
    {
        private readonly ILogger _logger;

        public FunctionIntroduction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FunctionIntroduction>();
        }

        [Function("FunctionIntroduction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
