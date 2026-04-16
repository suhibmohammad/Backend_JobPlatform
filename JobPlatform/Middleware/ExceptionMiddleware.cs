// File: API/Middleware/ExceptionMiddleware.cs
using JobPlatformBackend.Domain.src.Exceptions;
using System.Net;

namespace JobPlatformBackend.API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
			catch (Exception ex)
			{
				_logger.LogError(ex, "حدث خطأ: {Message}", ex.Message);
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";

			// القيم الافتراضية
			var statusCode = (int)HttpStatusCode.InternalServerError;
			var message = "Internal Server Error من السيرفر، رح نصلحه حالاً!";
			object? additionalData = null;

			// التحقق إذا كان الاستثناء من نوعنا المخصص
			if (exception is AppException appEx)
			{
				statusCode = appEx.StatusCode;
				message = appEx.Message;
				additionalData = appEx.AdditionalData; // هنا الربط الصحيح
			}

			context.Response.StatusCode = statusCode;

			var response = new
			{
				StatusCode = statusCode,
				Message = message,
				Data = additionalData, // سيحتوي على { "requiresVerification": true }
				Detail = exception.Message // يظهر في البيئة التطويرية فقط
			};

			return context.Response.WriteAsJsonAsync(response);
		}
	}
}