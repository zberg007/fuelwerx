using System;
using System.Text.RegularExpressions;

namespace FuelWerx.Web.Views
{
	public static class UrlChecker
	{
		private readonly static Regex UrlWithProtocolRegex;

		static UrlChecker()
		{
			UrlChecker.UrlWithProtocolRegex = new Regex("^.{1,10}://.*$");
		}

		public static bool IsRooted(string url)
		{
			if (url.StartsWith("/"))
			{
				return true;
			}
			if (UrlChecker.UrlWithProtocolRegex.IsMatch(url))
			{
				return true;
			}
			return false;
		}
	}
}