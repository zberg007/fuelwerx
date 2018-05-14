using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFees.Dto
{
	public class CreateOrUpdateEmergencyDeliveryFeeInput : IInputDto, IDto, IValidate
	{
		[Required]
		public EmergencyDeliveryFeeEditDto EmergencyDeliveryFee
		{
			get;
			set;
		}

		public CreateOrUpdateEmergencyDeliveryFeeInput()
		{
		}
	}
}