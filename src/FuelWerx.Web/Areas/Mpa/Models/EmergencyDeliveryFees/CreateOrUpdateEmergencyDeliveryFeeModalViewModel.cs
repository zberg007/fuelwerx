using Abp.AutoMapper;
using FuelWerx.Administrative.EmergencyDeliveryFees.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.EmergencyDeliveryFees
{
	[AutoMapFrom(new Type[] { typeof(GetEmergencyDeliveryFeeForEditOutput) })]
	public class CreateOrUpdateEmergencyDeliveryFeeModalViewModel : GetEmergencyDeliveryFeeForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.EmergencyDeliveryFee.Id.HasValue;
			}
		}

		public CreateOrUpdateEmergencyDeliveryFeeModalViewModel(GetEmergencyDeliveryFeeForEditOutput output)
		{
			output.MapTo<GetEmergencyDeliveryFeeForEditOutput, CreateOrUpdateEmergencyDeliveryFeeModalViewModel>(this);
		}
	}
}