using Microsoft.Extensions.Logging;

namespace FunctionWithTraceLog.Bl
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger _logger;
        public BusinessLogic(ILogger<BusinessLogic> logger)
        {
            _logger = logger;
        }

        public async Task<bool> StartEvent()
        {
            var x = _logger.IsEnabled(LogLevel.Trace);
            _logger.LogInformation($"trace is enabled :{x}");
            _logger.LogTrace($"logging trace in bl");
            _logger.LogDebug($"logging debug in bl");
            _logger.LogInformation($"logging info in bl");
            _logger.LogWarning($"logging warning in bl");
            _logger.LogError($"logging error in bl");
            _logger.LogCritical($"logging critical in bl");
            return true;
        }
    }
}
