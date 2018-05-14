using FuelWerx.Administrative.Zones.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Map
{
	public class ZonesMapView
	{
		private static Random rnd;

		public string GoogleMapsApiKey
		{
			get
			{
				return ConfigurationManager.AppSettings["Maps.Google.ApiKey"].ToString();
			}
		}

		private string[] GoogleMapsZoneColors
		{
			get
			{
				return ConfigurationManager.AppSettings["Maps.Google.ZoneColors"].ToString().Replace("'", "").Split(new char[] { ',' });
			}
		}

		public string TenantCoordinates
		{
			get;
			set;
		}

		public List<ZoneListDto> Zones
		{
			get;
			set;
		}

		static ZonesMapView()
		{
			ZonesMapView.rnd = new Random();
		}

		public ZonesMapView()
		{
		}

		public string RandomGoogleMapsZoneColors(List<string> exclusionList)
		{
			IEnumerable<string> strs = 
				from p in this.GoogleMapsZoneColors.AsEnumerable<string>()
				where !exclusionList.AsEnumerable<string>().Any<string>((string p2) => p2 == p)
				select p;
			if (strs.Count<string>() > 0)
			{
				int num = ZonesMapView.rnd.Next(strs.Count<string>());
				return strs.ToList<string>()[num];
			}
			strs = this.GoogleMapsZoneColors.AsEnumerable<string>();
			int num1 = ZonesMapView.rnd.Next(strs.Count<string>());
			return strs.ToList<string>()[num1];
		}
	}
}