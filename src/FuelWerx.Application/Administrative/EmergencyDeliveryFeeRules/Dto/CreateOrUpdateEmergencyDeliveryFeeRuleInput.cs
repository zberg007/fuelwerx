using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto
{
	public class CreateOrUpdateEmergencyDeliveryFeeRuleInput : IInputDto, IDto, IValidate
	{
		[Required]
		public EmergencyDeliveryFeeRuleEditDto EmergencyDeliveryFeeRule
		{
			get;
			set;
		}

		public CreateOrUpdateEmergencyDeliveryFeeRuleInput()
		{
		}
	}
}