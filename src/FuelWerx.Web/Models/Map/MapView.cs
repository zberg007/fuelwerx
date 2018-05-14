using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Map
{
	public class MapView
	{
		public string GoogleMapsApiKey
		{
			get
			{
				return ConfigurationManager.AppSettings["Maps.Google.ApiKey"].ToString();
			}
		}

		public string Label
		{
			get;
			set;
		}

		public double? Latitude
		{
			get;
			set;
		}

		public double? Longitude
		{
			get;
			set;
		}

		public MapView()
		{
		}
	}
}