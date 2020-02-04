using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Extensions
{
    public static class ConversionExtensions
    {
		public static int AsIntOr(this string src, int valIfNull)
		{
			return src.AsInt() ?? valIfNull;
		}

		public static int? AsInt(this string src)
		{

			if (src != null && int.TryParse(src, out int result))
				return result;
			return null;
		}
	}
}
