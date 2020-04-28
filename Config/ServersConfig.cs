using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validation;

namespace Helpers.Config
{
	public interface IAvailableServers
	{
		string[] AvailableServers { get; }
		string NextServer();
		ServersConfig Config { get; }
	}

	public class ServersConfig : IAvailableServers
	{
		private int _iServer = 0;

		/// <summary>
		/// A collection of servers to be used for price calculation.
		/// </summary>
		public string[] AvailableServers { get; }
		public string OriginalServerValue { get; }

		private string QualifyServer(string inServer)
		{
			if (!inServer.StartsWith("http"))
				return "http://" + inServer;
			return inServer;
		}
		/// <summary>
		/// Serves out the next server from the held list in order to facilitate some sense of load balancing.
		/// </summary>
		/// <returns>The next server in the sequence.</returns>
		public string NextServer() => AvailableServers[_iServer++ % AvailableServers.Length];
		public ServersConfig Config => this;

		public ServersConfig(string niceName, string servers)
		{
			OriginalServerValue = servers;
			AvailableServers = servers?
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(inServer => QualifyServer(inServer))
				.ToArray() ?? null;

			Conditions.Begin()
				.HasContent(AvailableServers, $"{niceName} // No servers defined. Must be at least 1 in a csv string.")
				.ThrowIfAny();
		}


	}
}
