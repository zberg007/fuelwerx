using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative.Zones;
using FuelWerx.Administrative.Zones.Dto;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Web.Models.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class MapController : FuelWerxControllerBase
	{
		private readonly IZoneAppService _zoneAppService;

		private readonly ITenantSettingsAppService _tenantsettingsAppService;

		public MapController(IZoneAppService zoneAppService, ITenantSettingsAppService tenantsettingsAppService)
		{
			this._zoneAppService = zoneAppService;
			this._tenantsettingsAppService = tenantsettingsAppService;
		}

		public ActionResult Index(double? longitude, double? latitude, string label)
		{
			return base.View(new MapView()
			{
				Longitude = longitude,
				Latitude = latitude,
				Label = label
			});
		}

		public async Task<ActionResult> Zones(string zoneIds)
		{
			int? impersonatorTenantId;
			object value;
			object obj;
			List<ZoneListDto> zoneListDtos = new List<ZoneListDto>();
			string[] strArrays = zoneIds.Split(new char[] { ',' });
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str = strArrays[i];
				IZoneAppService zoneAppService = this._zoneAppService;
				long num = long.Parse(str.ToString());
				if (this.AbpSession.ImpersonatorTenantId.HasValue)
				{
					impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
					value = impersonatorTenantId.Value;
				}
				else
				{
					impersonatorTenantId = this.AbpSession.TenantId;
					value = impersonatorTenantId.Value;
				}
				ZoneListDto zone = await zoneAppService.GetZone(num, (long)value);
				if (zone != null && zone.Id.ToString().Length > 0)
				{
					zoneListDtos.Add(zone);
				}
			}
			strArrays = null;
			ITenantSettingsAppService tenantSettingsAppService = this._tenantsettingsAppService;
			if (this.AbpSession.ImpersonatorTenantId.HasValue)
			{
				impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
				obj = impersonatorTenantId.Value;
			}
			else
			{
				impersonatorTenantId = this.AbpSession.TenantId;
				obj = impersonatorTenantId.Value;
			}
			string tenantCoordinates = await tenantSettingsAppService.GetTenantCoordinates((long)obj);
			ZonesMapView zonesMapView = new ZonesMapView()
			{
				Zones = zoneListDtos,
				TenantCoordinates = tenantCoordinates
			};
			return this.View(zonesMapView);
		}
	}
}