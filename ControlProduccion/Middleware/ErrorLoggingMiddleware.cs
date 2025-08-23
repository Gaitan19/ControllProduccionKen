using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Application.DTOs;
using Application.Interfaces;

public sealed class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await LogErrorAsync(context, ex);
            throw;
        }
    }

    private static async Task LogErrorAsync(HttpContext context, Exception ex)
    {
        var errorLogService = context.RequestServices.GetRequiredService<IErrorLogService>();

        context.Request.EnableBuffering();
        string body = string.Empty;

        if (context.Request.ContentLength > 0)
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        var correlationId = context.Request.Headers.TryGetValue("X-Correlation-ID", out var cid)
            ? cid.ToString()
            : context.TraceIdentifier;

        var ip = context.Request.Headers.TryGetValue("X-Forwarded-For", out var xff)
            ? xff.ToString().Split(',')[0].Trim()
            : context.Connection.RemoteIpAddress?.ToString();

        var dto = new ErrorLogDTO
        {
            Level = "Error",
            Message = ex.Message,
            Exception = ex.ToString(),
            StackTrace = ex.StackTrace,
            RequestPath = context.Request.Path,
            RequestMethod = context.Request.Method,
            QueryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null,
            RequestBody = body,
            UserId = context.User?.Identity?.Name,
            IPAddress = ip,
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            CorrelationId = correlationId
        };

        await errorLogService.LogAsync(dto);
    }
}
