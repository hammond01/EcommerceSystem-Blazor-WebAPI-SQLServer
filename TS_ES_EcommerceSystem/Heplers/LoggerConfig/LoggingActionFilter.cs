using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Heplers.LoggersConfig
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
            var controller = context.Controller.GetType().Name;
            var action = context.ActionDescriptor.RouteValues["action"];
            var parameters = context.ActionArguments;

            _logger.LogInformation($"Executing {controller} => {action} with parameters: {Newtonsoft.Json.JsonConvert.SerializeObject(parameters)}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller.GetType().Name;
            var action = context.ActionDescriptor.RouteValues["action"];

            _logger.LogInformation($"Executed {controller} => {action}");
        }
    }

}
