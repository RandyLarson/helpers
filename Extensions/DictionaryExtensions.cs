using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Helpers.Extensions
{
	public static class DictionaryExtensions
	{
		public static void AddRange<Tk, Tv>(this IDictionary<Tk, Tv> dst, IEnumerable<KeyValuePair<Tk, Tv>> src)
		{
			if (null == dst && null == src)
				return;

			if (null == dst)
				dst = new Dictionary<Tk, Tv>();

			foreach (var kvp in src)
				dst[kvp.Key] = kvp.Value;
		}

		public static void AddRange<Tk, Tv>(this IDictionary<Tk, Tv> dst, params (Tk, Tv)[] src)
		{
			if (null == dst && null == src)
				return;

			if (null == dst)
				dst = new Dictionary<Tk, Tv>();

			foreach (var tup in src)
				dst[tup.Item1] = tup.Item2;
		}

		public static T GetAs<Tk, Tv, T>(this IDictionary<Tk, Tv> src, Tk propertyName)
		{
			if (propertyName == null)
				throw new ArgumentNullException("propertyName");

			if (src == null || !src.ContainsKey(propertyName))
			{
				throw new KeyNotFoundException($"Property not found: {propertyName}");
			}

			// This will either be a simple property, or a JSON object. 
			Tv propertyPayload = src[propertyName];
			bool isString = propertyPayload is string;
			try
			{
				if ( isString )
				{
					T val = JsonConvert.DeserializeObject<T>(propertyPayload as string);
					return val;
				}
			}
			catch (Exception)
			{
				// Convert to Enum
				if (isString && typeof(T).GetTypeInfo().IsEnum)
					return (T)Enum.Parse(typeof(T), propertyPayload as string, true);
			}
			return (T)Convert.ChangeType(propertyPayload, typeof(T));
		}

		public static T GetAsOr<Tk, Tv, T>(this IDictionary<Tk, Tv> src, Tk propertyName, T valueIfNotPresent)
		{
			try
			{
				return src.GetAs<Tk, Tv, T>(propertyName);
			}
			catch (Exception)
			{
				return valueIfNotPresent;
			}
		}

		public static T GetAsOr<Tv, Tn, T>(this IDictionary<string, Tv> src, Tn propertyName, T valueIfNotPresent)
		{
			try
			{
				return src.GetAs<string, Tv, T>(propertyName.ToString());
			}
			catch (Exception)
			{
				return valueIfNotPresent;
			}
		}
	}
}
