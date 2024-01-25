using FunctionWithTraceLog.Bl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionWithTraceLog
{
    public class FunctionApplication
    {
        private readonly ILogger _logger;
        private readonly IBusinessLogic _bl;
        private const string FunctionName = "FutureProspect";
        public FunctionApplication(IBusinessLogic bl, ILogger<FunctionApplication> logger)
        {
            _bl = bl;
            _logger = logger;
        }

        [Function(FunctionName)]
        public async Task<IActionResult> RunAsync(
             [HttpTrigger(AuthorizationLevel.Function, "POST", Route = null)] HttpRequest request)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // bl execution
            await _bl.StartEvent();

            return new AcceptedResult();
        }
    }
}
