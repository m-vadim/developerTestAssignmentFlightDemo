using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

internal sealed class RequestLoggingFilter : IEndpointFilter {
	private readonly ILogger<RequestLoggingFilter> _logger;

	public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger) {
		_logger = logger;
	}

	public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
		var httpContext = context.HttpContext;

		// Log request details
		_logger.LogInformation(
			"Request: {Method} {Path}{QueryString} - Client IP: {IPAddress}",
			httpContext.Request.Method,
			httpContext.Request.Path,
			httpContext.Request.QueryString,
			httpContext.Connection.RemoteIpAddress);

		try {
			// Continue with the request pipeline
			var result = await next(context);

			// Log response status code after the request is processed
			_logger.LogInformation(
				"Response: {StatusCode} for {Method} {Path}",
				httpContext.Response.StatusCode,
				httpContext.Request.Method,
				httpContext.Request.Path);

			return result;
		} catch (Exception ex) {
			// Log any exceptions
			_logger.LogError(
				ex,
				"Exception occurred processing {Method} {Path}",
				httpContext.Request.Method,
				httpContext.Request.Path);
			throw;
		}
	}
}
