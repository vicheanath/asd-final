using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Satellite.Astronaut.Tracking.Exceptions;

namespace Satellite.Astronaut.Tracking.Filters;

public class ApiExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var error = new ApiError
        {
            Timestamp = DateTime.UtcNow,
            Status = StatusCodes.Status500InternalServerError,
            Error = "Internal Server Error",
            Message = context.Exception.Message,
            Path = context.HttpContext.Request.Path
        };

        switch (context.Exception)
        {
            case AstronautNotFoundException ex:
                error.Status = StatusCodes.Status404NotFound;
                error.Error = "Not Found";
                break;
            case SatelliteNotFoundException ex:
                error.Status = StatusCodes.Status404NotFound;
                error.Error = "Not Found";
                break;
            case SatelliteDecommissionedException ex:
                error.Status = StatusCodes.Status400BadRequest;
                error.Error = "Bad Request";
                break;
        }

        context.Result = new JsonResult(error) { StatusCode = error.Status };
        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}

public class ApiError
{
    public DateTime Timestamp { get; set; }
    public int Status { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }
}