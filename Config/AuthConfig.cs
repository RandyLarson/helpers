using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validation;

namespace Helpers.Config
{
	public class AuthConfig : IAvailableServers
	{
		ServersConfig _innerServerConfig = null;
		public string AUTH_SERVERS { get => _innerServerConfig?.OriginalServerValue ?? string.Empty; set => _innerServerConfig = new ServersConfig("AUTH_SERVERS", value); }
		public string AUTH_API_KEY { get; set; }
		public string AUTH_API_SECRET { get; set; }
		public string AUTH_USERNAME { get; set; }
		public string AUTH_PASSWORD { get; set; }

		public string[] AvailableServers => _innerServerConfig.AvailableServers;

		public ServersConfig Config => _innerServerConfig;

		public string NextServer() => _innerServerConfig.NextServer();

		public object GetAuthObject()
		{
			if (AUTH_API_KEY != null)
			{
				return new
				{
					grant_type = "client_credentials",
					client_id = $"{AUTH_API_KEY}",
					client_secret = $"{ AUTH_API_SECRET}"
				};
			}

			return new
			{
				grant_type = "password",
				username = $"{AUTH_USERNAME}",
				password = $"{AUTH_PASSWORD}"
			};
		}
	}
}
