using System.Text.Json;

namespace DigitalStore.WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> Log;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> log)
        {
            this.next = next;
            Log = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // before controller invoke
                await next.Invoke(context);
                // after controller invoke
            }
            catch (Exception ex)
            {
                // log
                Log.LogWarning(
                    $"Path={context.Request.Path} || " +
                    $"Method={context.Request.Method} || " +
                    $"Exception={ex.Message}"
                );

                context.Response.StatusCode = 500;
                context.Request.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize("Internal Server Error"));
            }

        }

    }
}
