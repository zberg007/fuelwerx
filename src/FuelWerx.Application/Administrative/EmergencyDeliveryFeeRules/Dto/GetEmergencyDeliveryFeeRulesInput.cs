using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto
{
	public class GetEmergencyDeliveryFeeRulesInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public GetEmergencyDeliveryFeeRulesInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "Name,Caption";
			}
		}
	}
}