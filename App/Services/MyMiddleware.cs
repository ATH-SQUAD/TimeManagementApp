using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace TimeManagementApp.App.Services
{
    public class ElapsedTimeMiddleware
    {
        private readonly ILogger _logger;
        private RequestDelegate _next;

        public ElapsedTimeMiddleware(RequestDelegate next, ILogger<ElapsedTimeMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next(context);
            bool? isHtml = context.Response.ContentType?.ToLower().Contains("text/html");
            if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault())
            {
                _logger.LogInformation($"{context.Request.Path} executed in {stopWatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
