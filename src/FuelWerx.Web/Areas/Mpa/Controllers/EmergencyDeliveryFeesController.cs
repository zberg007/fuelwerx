using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative.EmergencyDeliveryFees;
using FuelWerx.Administrative.EmergencyDeliveryFees.Dto;
using FuelWerx.Administrative.Zones;
using FuelWerx.Administrative.Zones.Dto;
using FuelWerx.Web.Areas.Mpa.Models.EmergencyDeliveryFees;
using FuelWerx.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFees" })]
	public class EmergencyDeliveryFeesController : FuelWerxControllerBase
	{
		private readonly IEmergencyDeliveryFeeAppService _emergencyDeliveryFeeAppService;

		private readonly IZoneAppService _zoneAppService;

		public EmergencyDeliveryFeesController(IEmergencyDeliveryFeeAppService emergencyDeliveryFeeAppService, IZoneAppService zoneAppService)
		{
			this._emergencyDeliveryFeeAppService = emergencyDeliveryFeeAppService;
			this._zoneAppService = zoneAppService;
		}

		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			IEmergencyDeliveryFeeAppService emergencyDeliveryFeeAppService = this._emergencyDeliveryFeeAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateEmergencyDeliveryFeeModalViewModel createOrUpdateEmergencyDeliveryFeeModalViewModel = new CreateOrUpdateEmergencyDeliveryFeeModalViewModel(await emergencyDeliveryFeeAppService.GetEmergencyDeliveryFeeForEdit(nullableIdInput));
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			IZoneAppService zoneAppService = this._zoneAppService;
			int? tenantId = this.AbpSession.TenantId;
			List<ZoneListDto> zonesByTenantId = await zoneAppService.GetZonesByTenantId(tenantId.Value, true);
			if (zonesByTenantId.Count <= 0)
			{
				this.ViewData["Zones"] = null;
			}
			else
			{
				foreach (ZoneListDto zoneListDto in zonesByTenantId)
				{
					List<SelectListItem> selectListItems1 = selectListItems;
					SelectListItem selectListItem = new SelectListItem()
					{
						Text = zoneListDto.Name,
						Value = zoneListDto.Id.ToString(),
						Selected = false
					};
					selectListItems1.Add(selectListItem);
				}
				List<SelectListItem> selectListItems2 = selectListItems;
				SelectListItem selectListItem1 = new SelectListItem()
				{
					Text = "",
					Value = "",
					Disabled = false
				};
				selectListItems2.Insert(0, selectListItem1);
				this.ViewData["Zones"] = selectListItems.AsEnumerable<SelectListItem>();
			}
			return this.PartialView("_CreateOrUpdateModal", createOrUpdateEmergencyDeliveryFeeModalViewModel);
		}

		public ActionResult Index(GetEmergencyDeliveryFeesInput input)
		{
			return base.View();
		}
	}
}