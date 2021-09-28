using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Shared.Exceptions
{
    public class MiddelwareHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddelwareHandler> _logger;

        public MiddelwareHandler(RequestDelegate next, ILogger<MiddelwareHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await ExceptionHandlerAsync(context, e, _logger);
            }
        }

        private async Task ExceptionHandlerAsync(HttpContext context, Exception e, ILogger<MiddelwareHandler> logger)
        {
            object error = null;
            switch (e)
            {
                case ExceptionHandler eh:
                    logger.LogError(e, "Error en el manejador");
                    error = eh.Error;
                    context.Response.StatusCode = (int)eh.Code;
                    break;
                case Exception ex:
                    logger.LogError(ex, "Otro tipo de error");
                    error = string.IsNullOrWhiteSpace(ex.Message) ? "Errores" : ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (error != null)
            {
                string result = JsonConvert.SerializeObject(new { error });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
