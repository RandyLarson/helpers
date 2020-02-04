using System.Collections.Generic;
using System.Linq;

namespace Helpers.Extensions
{
	public static class CollectionExtensions
	{
		public static string AsJoinedString<T>(this IEnumerable<T> src, string joiner = ",") => string.Join(joiner, src.AsStrings());
		public static IEnumerable<string> AsStrings<T>(this IEnumerable<T> src) => src?.Select(item => item.ToString()) ?? new string[0];
		public static IEnumerable<T> AsEnumerable<T>(this T item) => item == null ? new T[0] : new T[] { item };
		
		public static IEnumerable<KeyValuePair<Tk, Tv>> Concat<Tk, Tv>(this IEnumerable<KeyValuePair<Tk, Tv>> lhs, params (Tk, Tv)[] rhs)
		{
			if (null == lhs && null == rhs)
				return null;

			if (lhs == null)
				lhs = new List<KeyValuePair<Tk, Tv>>();

			return lhs.Concat(rhs.Select(tup => new KeyValuePair<Tk, Tv>(tup.Item1, tup.Item2)));
		}
	}
}
