using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Helpers.Helpers;
using Newtonsoft.Json;
using System.Reflection;
using Validation;

namespace Helpers.Helpers
{
	public static class ConfigurationExtensions
    {
		/// <summary>
		/// DI of POCO config objects without having to spread IOptions<> around.
		/// Thank you, https://www.strathweb.com/2016/09/strongly-typed-configuration-in-asp-net-core-without-ioptionst/
		/// </summary>
		/// <typeparam name="TConfig"></typeparam>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns>The configured poco</returns>
		public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
		{
			Conditions.Begin()
				.NonNull(services, $"{nameof(services)} cannot be null")
				.ThrowIfAny();

			var config = configuration.BindToPOCO<TConfig>();
			services.AddSingleton(config);
			return config;
		}

		public static TConfig ConfigurePOCO<TConfig, TFace>(this IServiceCollection services, IConfiguration configuration) 
			where TConfig : class, TFace, new()
		{
			Conditions.Begin()
				.NonNull(services, $"{nameof(services)} cannot be null")
				.ThrowIfAny();

			var config = configuration.BindToPOCO<TConfig>();
			services.AddSingleton(typeof(TFace), config);
			return config;
		}

		public static TConfig BindPOCOViaAttributes<TConfig>(this IServiceCollection services,  IConfiguration configuration) where TConfig : class, new()
		{
			Conditions.Begin()
				.NonNull(configuration, $"{nameof(configuration)} cannot be null")
				.ThrowIfAny();

			var config = new TConfig();
			foreach (var propInfo in config.GetType().GetTypeInfo().GetProperties())
			{
				JsonPropertyAttribute serializationInfo = propInfo.GetCustomAttribute<JsonPropertyAttribute>();
				if ( serializationInfo != null )
				{
					var itsValue = configuration[serializationInfo.PropertyName];
					propInfo.SetValue(config, itsValue);
				}
			}

			services.AddSingleton(config);
			return config;
		}

		public static TConfig BindToPOCO<TConfig>(this IConfiguration configuration) where TConfig : class, new()
		{
			Conditions.Begin()
				.NonNull(configuration, $"{nameof(configuration)} cannot be null")
				.ThrowIfAny();

			var config = new TConfig();
			configuration.Bind(config);
			return config;
		}
	}
}
