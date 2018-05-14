using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using FuelWerx.Administrative.EmergencyDeliveryFeeRules;
using FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto;
using FuelWerx.Web.Areas.Mpa.Models.EmergencyDeliveryFeeRules;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.EmergencyDeliveryFeeRules" })]
	public class EmergencyDeliveryFeeRulesController : FuelWerxControllerBase
	{
		private readonly IEmergencyDeliveryFeeRuleAppService _emergencyDeliveryFeeRuleAppService;

		public EmergencyDeliveryFeeRulesController(IEmergencyDeliveryFeeRuleAppService emergencyDeliveryFeeRuleAppService)
		{
			this._emergencyDeliveryFeeRuleAppService = emergencyDeliveryFeeRuleAppService;
		}

		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			IEmergencyDeliveryFeeRuleAppService emergencyDeliveryFeeRuleAppService = this._emergencyDeliveryFeeRuleAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetEmergencyDeliveryFeeRuleForEditOutput emergencyDeliveryFeeRuleForEdit = await emergencyDeliveryFeeRuleAppService.GetEmergencyDeliveryFeeRuleForEdit(nullableIdInput);
			return this.PartialView("_CreateOrUpdateModal", new CreateOrUpdateEmergencyDeliveryFeeRuleModalViewModel(emergencyDeliveryFeeRuleForEdit));
		}

		public ActionResult Index(GetEmergencyDeliveryFeeRulesInput input)
		{
			return base.View();
		}
	}
}