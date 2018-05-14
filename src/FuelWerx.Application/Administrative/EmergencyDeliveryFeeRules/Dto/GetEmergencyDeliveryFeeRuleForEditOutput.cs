using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto
{
	public class GetEmergencyDeliveryFeeRuleForEditOutput : IOutputDto, IDto
	{
		public EmergencyDeliveryFeeRuleEditDto EmergencyDeliveryFeeRule
		{
			get;
			set;
		}

		public GetEmergencyDeliveryFeeRuleForEditOutput()
		{
		}
	}
}