using FunctionWithTraceLog.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Drawing.Drawing2D;

namespace FunctionWithTraceLog
{
    public class FunctionApplication
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private const string FunctionName = "FutureProspect";
        public FunctionApplication(IMediator mediator, ILogger<FunctionApplication> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Function(FunctionName)]
        public async Task<IActionResult> RunAsync(
             [HttpTrigger(AuthorizationLevel.Function, "POST", Route = null)] HttpRequest request)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var x = _logger.IsEnabled(LogLevel.Trace);
            _logger.LogInformation($"trace is enabled in function :{x}");
            _logger.LogTrace($"logging trace in function");
            _logger.LogDebug($"logging debug in function");
            _logger.LogInformation($"logging info in function");
            _logger.LogWarning($"logging warning in function");
            _logger.LogError($"logging error in function");
            _logger.LogCritical($"logging critical in function");

            await Task.Run(async () => await ExecutePipelineAsync().ConfigureAwait(false)).ConfigureAwait(false);

            return new AcceptedResult();


        }
        public Task ExecutePipelineAsync()
        {
            try
            {
                FutureProspectCommand command = new()
                {
                    Data = new Data.BusinessPartnerFutureProspectData
                    {
                        Name = "prospect",
                    },
                };

                // now use MediatR and your handler to handle the message
                _ = _mediator.Send(command).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"pipeline failed with {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
