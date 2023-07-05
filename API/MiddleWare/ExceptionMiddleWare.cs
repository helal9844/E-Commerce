using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleWare(IHostEnvironment environment, ILogger<ExceptionMiddleWare> logger, RequestDelegate requestDelegate)
        {
            _environment = environment;
            _logger = logger;
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context); 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _environment
                    .IsDevelopment() ? new APIException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new APIException((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
