using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientIp.Filters
{
    public class ClientIdCheckFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public ClientIdCheckFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ClassConsoleLogActionOneFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Remote IpAddress: {context.HttpContext.Connection.RemoteIpAddress}");

            //foreach (var item in context.HttpContext.Request.Headers.Keys)
            //{
            //    _logger.LogInformation($"Header key: {item}");
            //}

            //foreach (var item in context.HttpContext.Request.Headers.Values)
            //{
            //    _logger.LogInformation($"Header value: {item}");
            //}

            base.OnActionExecuting(context);
        }
    }
}
