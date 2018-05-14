using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFees.Dto
{
	public class GetEmergencyDeliveryFeeForEditOutput : IOutputDto, IDto
	{
		public EmergencyDeliveryFeeEditDto EmergencyDeliveryFee
		{
			get;
			set;
		}

		public GetEmergencyDeliveryFeeForEditOutput()
		{
		}
	}
}