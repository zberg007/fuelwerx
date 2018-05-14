using Abp.AutoMapper;
using FuelWerx.Administrative.Zones.Dto;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Zones
{
	[AutoMapFrom(new Type[] { typeof(GetZoneForEditOutput) })]
	public class CreateOrUpdateZoneModalViewModel : GetZoneForEditOutput
	{
		public string GoogleMapsApiKey
		{
			get
			{
				return ConfigurationManager.AppSettings["Maps.Google.ApiKey"].ToString();
			}
		}

		public bool IsEditMode
		{
			get
			{
				return base.Zone.Id.HasValue;
			}
		}

		public virtual string TenantCoordinates
		{
			get;
			set;
		}

		public CreateOrUpdateZoneModalViewModel(GetZoneForEditOutput output)
		{
			output.MapTo<GetZoneForEditOutput, CreateOrUpdateZoneModalViewModel>(this);
		}
	}
}