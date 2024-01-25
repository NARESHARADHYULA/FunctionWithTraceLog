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
            _logger.LogWarning($"logging warning in bl");
            _logger.LogTrace($"logging trace in bl");
            return true;
        }
    }
}
