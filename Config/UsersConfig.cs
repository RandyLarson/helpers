using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validation;

namespace Helpers.Config
{
	public class UsersConfig : IAvailableServers
	{
		ServersConfig _innerServerConfig = null;
		public string SERVERS { get => _innerServerConfig?.OriginalServerValue ?? string.Empty; set => _innerServerConfig = new ServersConfig("SERVERS", value); }

		public UsersConfig()
		{
		}

		public string[] AvailableServers => _innerServerConfig.AvailableServers;

		public ServersConfig Config => _innerServerConfig;

		public string NextServer() => _innerServerConfig.NextServer();
	}
}
