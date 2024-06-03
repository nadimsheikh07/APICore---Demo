using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using CoreApi.LoggerService;
using CoreApi.Common.Models;

namespace CoreApi.CustomExceptionMiddleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILoggerManager _logger;

		public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (ApiHttpException apiHttpEx)
			{
				_logger.LogError($"A new violation exception has been thrown: {apiHttpEx}");
				await HandleExceptionAsync(httpContext, apiHttpEx);
			}
			catch (AccessViolationException avEx)
			{
				_logger.LogError($"A new violation exception has been thrown: {avEx}");
				await HandleExceptionAsync(httpContext, avEx);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)

		{

			_logger.LogDebug("Starting of HandleExceptionAsync method");

			var errorResponse = new ErrorLogModel();



			if (exception is ApiHttpException httpException)

			{

				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

				errorResponse.errorId = (int)httpException.errorId;

				errorResponse.errorDescription = httpException.message;

				_logger.LogError(httpException.message);

			}
			else
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;



				errorResponse.errorId = (int)ErrorDetails.UnknownExceptionError;



				errorResponse.errorDescription = exception.Message;
			}



			context.Response.ContentType = "application/json";



			_logger.LogError(exception.ToString());



			await context.Response.WriteAsync(errorResponse.ToString());

			_logger.LogDebug("Ending of HandleExceptionAsync method");



		}
	}
}
