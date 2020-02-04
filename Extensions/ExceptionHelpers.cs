using System;

namespace Helpers.Helpers
{
	public static class ExceptionHelpers
	{

		public static string Messages(this Exception ex)
		{
			if (null == ex)
				return string.Empty;

			string innerMessages = ex.InnerException.Messages();
			return ex.Message + (string.IsNullOrWhiteSpace(innerMessages) ? string.Empty : " // " + innerMessages);
		}
	}
}
