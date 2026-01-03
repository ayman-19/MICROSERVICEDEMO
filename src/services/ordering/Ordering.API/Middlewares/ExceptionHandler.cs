namespace Ordering.API.Middlewares;

public sealed class ExceptionHandler() : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var (statusCode, message) = MapExceptionToResponse(ex);

            var response = new Response
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(
                response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            );

            await context.Response.WriteAsync(json);
        }
    }

    private static (int statusCode, string message) MapExceptionToResponse(Exception ex) =>
        ex switch
        {
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, ex.Message),
            ServiceException => (
                (int)HttpStatusCode.InternalServerError,
                "Oops!! something went wrong whatever happened it was probably our fault. please try again"
            ),
            NotSupportedException => ((int)HttpStatusCode.NotImplemented, ex.Message),
            ValidatorException => ((int)HttpStatusCode.BadRequest, ex.Message),
            _ => (
                (int)HttpStatusCode.InternalServerError,
                "Oops!! something went wrong whatever happened it was probably our fault. please try again"
            ),
        };

    private async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
        try
        {
            if (context.Request.ContentLength == null || context.Request.ContentLength == 0)
                return "EMPTY BODY";

            context.Request.Body.Position = 0;

            using StreamReader reader = new(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true
            );

            var bodyAsString = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            return System.Text.Json.JsonSerializer.Serialize(
                JsonDocument.Parse(bodyAsString),
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "UNABLE TO READ BODY";
        }
    }
}
