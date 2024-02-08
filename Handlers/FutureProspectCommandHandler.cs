using FunctionWithTraceLog.Bl;
using FunctionWithTraceLog.Commands;
using MediatR;

using Microsoft.Extensions.Logging;

namespace FunctionWithTraceLog.Handlers
{
    internal class FutureProspectCommandHandler : IRequestHandler<FutureProspectCommand>
    {
        private readonly IBusinessLogic _bl;
        private readonly ILogger _logger;

        public FutureProspectCommandHandler(
            IBusinessLogic bl,
            ILogger<FutureProspectCommandHandler> logger)
        {
            _bl = bl;
            _logger = logger;
        }


        public async Task Handle(FutureProspectCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Command handler executing at: {DateTime.Now}");

            _ = _bl.StartEvent();
            await Task.CompletedTask;

            _logger.LogInformation($"Command handler completed at: {DateTime.Now}");
        }
    }
}