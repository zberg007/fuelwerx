using Abp.AutoMapper;
using FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.EmergencyDeliveryFeeRules
{
	[AutoMapFrom(new Type[] { typeof(GetEmergencyDeliveryFeeRuleForEditOutput) })]
	public class CreateOrUpdateEmergencyDeliveryFeeRuleModalViewModel : GetEmergencyDeliveryFeeRuleForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.EmergencyDeliveryFeeRule.Id.HasValue;
			}
		}

		public CreateOrUpdateEmergencyDeliveryFeeRuleModalViewModel(GetEmergencyDeliveryFeeRuleForEditOutput output)
		{
			output.MapTo<GetEmergencyDeliveryFeeRuleForEditOutput, CreateOrUpdateEmergencyDeliveryFeeRuleModalViewModel>(this);
		}
	}
}