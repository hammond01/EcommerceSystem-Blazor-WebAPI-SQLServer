using Microsoft.AspNetCore.Mvc.Filters;

namespace Server.Helper.LoggersConfig
{
    public class LoggingActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"Executing action: {actionName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"Executed action: {actionName}");
        }
    }
}
