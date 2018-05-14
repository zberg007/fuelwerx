using System;
using System.Globalization;
using System.Threading;

namespace FuelWerx.Localization
{
	public static class CultureHelper
	{
		public static bool IsRtl
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			}
		}
	}
}