using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using Wise.IsolatedApp.Domain;

namespace Wise.IsolatedApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly ICustomRepository _repository;
        private readonly IConfiguration _configuration;

        public Function1(ICustomRepository repository, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<Function1>();
            _configuration = configuration;
        }

        [Function("Function1")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string secretValue = _configuration["MySecret"] ?? ""; // Value From KeyVault

            var data = _repository.GetData(secretValue);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Data from repository: {data}");

            return response;
        }
    }
}
