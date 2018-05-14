using System;
using System.IO;
using System.Reflection;

namespace FuelWerx
{
	public class AppVersionHelper
	{
		public const string Version = "1.7.1.1";

		public static DateTime ReleaseDate
		{
			get
			{
				return (new FileInfo(typeof(AppVersionHelper).Assembly.Location)).LastWriteTime;
			}
		}

		public AppVersionHelper()
		{
		}
	}
}