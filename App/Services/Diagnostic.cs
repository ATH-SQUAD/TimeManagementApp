using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Services
{
    public class Diagnostic
    {
        public class StatusCodePagesMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly StatusCodePagesOptions _options;

            public StatusCodePagesMiddleware(RequestDelegate next, IOptions<StatusCodePagesOptions> options)
            {
                _next = next;
                _options = options.Value;
                if (_options.HandleAsync == null)
                {
                    throw new ArgumentException("Missing options.HandleAsync implementation.");
                }
            }

            public async Task Invoke(HttpContext context)
            {
                StatusCodePagesFeature statusCodeFeature = new StatusCodePagesFeature();
                context.Features.Set<IStatusCodePagesFeature>(statusCodeFeature);

                await _next(context);

                if (!statusCodeFeature.Enabled)
                {
                    // Check if the feature is still available because other middleware (such as a web API written in MVC) could
                    // have disabled the feature to prevent HTML status code responses from showing up to an API client.
                    return;
                }

                // Do nothing if a response body has already been provided.
                if (context.Response.HasStarted
                    || context.Response.StatusCode < 400
                    || context.Response.StatusCode >= 600
                    || context.Response.ContentLength.HasValue
                    || !string.IsNullOrEmpty(context.Response.ContentType))
                {
                    return;
                }

                StatusCodeContext statusCodeContext = new StatusCodeContext(context, _options, _next);
                await _options.HandleAsync(statusCodeContext);
            }
        }
    }
}
