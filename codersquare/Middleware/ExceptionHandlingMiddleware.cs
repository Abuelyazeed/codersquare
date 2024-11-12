using System.Net;
using Newtonsoft.Json;

namespace codersquare.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            //Proceed with next middleware
            await _next(context);
        }
        catch (Exception ex)
        {
            //Log the exception
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        // Set the response status code and content type
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        
        // Create a structured error response
        var response = new
        {
            status = "error",
            message = "Oops an unexpected error occurred. Please try again later."
        };

        // Serialize the response to JSON
        var responseJson = JsonConvert.SerializeObject(response);

        // Write the response to the HTTP context
        return context.Response.WriteAsync(responseJson);
        
    }
}