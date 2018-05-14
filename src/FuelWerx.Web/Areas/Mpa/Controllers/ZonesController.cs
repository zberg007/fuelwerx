using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Taxes;
using FuelWerx.Administrative.Zones;
using FuelWerx.Administrative.Zones.Dto;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Generic;
using FuelWerx.Web.Areas.Mpa.Models.Zones;
using FuelWerx.Web.Controllers;
using FuelWerx.Web.Models.Map;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Zones" })]
	public class ZonesController : FuelWerxControllerBase
	{
		private readonly IZoneAppService _zoneAppService;

		private readonly IGenericAppService _genericAppService;

		private readonly ITenantSettingsAppService _tenantsettingsAppService;

		private readonly ITaxAppService _taxAppService;

		public ZonesController(IZoneAppService zoneAppService, IGenericAppService genericAppService, ITenantSettingsAppService tenantsettingsAppService, ITaxAppService taxAppService)
		{
			this._zoneAppService = zoneAppService;
			this._genericAppService = genericAppService;
			this._tenantsettingsAppService = tenantsettingsAppService;
			this._taxAppService = taxAppService;
		}

		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			int? impersonatorTenantId;
			object value;
			IZoneAppService zoneAppService = this._zoneAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateZoneModalViewModel createOrUpdateZoneModalViewModel = new CreateOrUpdateZoneModalViewModel(await zoneAppService.GetZoneForEdit(nullableIdInput));
			CreateOrUpdateZoneModalViewModel tenantCoordinates = createOrUpdateZoneModalViewModel;
			ITenantSettingsAppService tenantSettingsAppService = this._tenantsettingsAppService;
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
			tenantCoordinates.TenantCoordinates = await tenantSettingsAppService.GetTenantCoordinates((long)value);
			tenantCoordinates = null;
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (Tax taxesForTaxRule in await this._taxAppService.GetTaxesForTaxRules())
			{
				List<SelectListItem> selectListItems1 = selectListItems;
				SelectListItem selectListItem = new SelectListItem()
				{
					Text = string.Format("{0} - {1}%", taxesForTaxRule.Name, taxesForTaxRule.Rate),
					Value = taxesForTaxRule.Id.ToString(),
					Disabled = false,
					Selected = false
				};
				selectListItems1.Add(selectListItem);
			}
			this.ViewData["Taxes"] = selectListItems.AsEnumerable<SelectListItem>();
			return this.PartialView("_CreateOrUpdateModal", createOrUpdateZoneModalViewModel);
		}

		public ActionResult Index(GetZonesInput input)
		{
			((dynamic)base.ViewBag).GoogleMapsApiKey = ConfigurationManager.AppSettings["Maps.Google.ApiKey"].ToString();
			return base.View();
		}

		public async Task<PartialViewResult> MapViewModal()
		{
			int? impersonatorTenantId;
			int value;
			object obj;
			IZoneAppService zoneAppService = this._zoneAppService;
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
			List<ZoneListDto> zonesByTenantId = await zoneAppService.GetZonesByTenantId(value, true);
			if (zonesByTenantId == null || zonesByTenantId != null && zonesByTenantId.Count == 0)
			{
				zonesByTenantId = new List<ZoneListDto>();
			}
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
				Zones = zonesByTenantId,
				TenantCoordinates = tenantCoordinates
			};
			return this.PartialView("_MapViewModal", zonesMapView);
		}
	}
}