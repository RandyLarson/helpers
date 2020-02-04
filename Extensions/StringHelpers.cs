using System;

namespace Helpers.Extensions
{
	public static class StringHelpers
	{
		public static string LogValue(this string toLog)
		{
			return toLog ?? "(null)";
		}

		public static bool EqualsIgnoreCase(this string lhs, string rhs)
		{
			return string.Equals(lhs, rhs, StringComparison.OrdinalIgnoreCase);
		}

		public static bool StartsWithIgnoreCase(this string lhs, string rhs)
		{
			if (null == lhs)
				return (rhs == null);
			return lhs.StartsWith(rhs, StringComparison.OrdinalIgnoreCase);
		}

		public static string OrEmpty(this string src)
		{
			return src ?? string.Empty;
		}		
	}
}
