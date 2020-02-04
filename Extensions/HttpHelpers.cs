using Flurl.Http;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Helpers.Helpers
{
	static class RetryHelper
	{
		private static readonly IEnumerable<HttpStatusCode> DEFAULT_SKIP_CODES = new[] { HttpStatusCode.BadRequest, HttpStatusCode.Forbidden, HttpStatusCode.Unauthorized, HttpStatusCode.Conflict, HttpStatusCode.NotFound };

		public static async Task<T> RetryHttpAsync<T>(Func<Task<T>> funcToRetry, int retryCount = 10, IEnumerable<HttpStatusCode> skipRetryOnCodes = null)
		{
			if (skipRetryOnCodes == null)
				skipRetryOnCodes = DEFAULT_SKIP_CODES;

			return await Policy
				.Handle<FlurlHttpTimeoutException>()
				.Or<FlurlHttpException>(ex => ex.Call.HttpStatus.HasValue && !skipRetryOnCodes.Contains(ex.Call.HttpStatus.Value))
				.WaitAndRetryAsync(retryCount, iter => TimeSpan.FromSeconds(1))
				.ExecuteAsync(funcToRetry);
		}

		public static async Task<T> RetryOnException<T>(Func<Task<T>> funcToRetry, int retryCount = 10, int retryWaitMs=1000)
		{
			return await Policy
				.Handle<Exception>()
				.WaitAndRetryAsync(retryCount, iter => TimeSpan.FromMilliseconds(retryWaitMs))
				.ExecuteAsync(funcToRetry);
		}

	}
}
