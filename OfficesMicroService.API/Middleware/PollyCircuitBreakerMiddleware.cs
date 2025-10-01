using Polly.CircuitBreaker;

using System.Net;

namespace OfficesMicroService.API.Middleware;

public class PollyCircuitBreakerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

    public PollyCircuitBreakerMiddleware(RequestDelegate next, AsyncCircuitBreakerPolicy circuitBreakerPolicy)
    {
        _next = next;
        _circuitBreakerPolicy = circuitBreakerPolicy;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _circuitBreakerPolicy.ExecuteAsync(() => _next(context));
        }
        catch (BrokenCircuitException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            await context.Response.WriteAsync("Service is currently unavailable. Please try again later.");
        }
    }
}
