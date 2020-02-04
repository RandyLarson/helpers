using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers.Billing.Helpers
{
    public static class LogExtensions
    {
		public static IFlurlRequest LogCall(this IFlurlRequest iRequest, ILogger withLogger)
		{
			if ( withLogger != null )
			{
				withLogger.LogInformation("Invoking URL: {0}", iRequest.Url);
			}
			return iRequest;
		}
		public static Url LogCall(this Url uri, ILogger withLogger)
		{
			if (withLogger != null)
			{
				withLogger.LogInformation("Invoking URL: {0}", uri);
			}
			return uri;
		}
	}
}
