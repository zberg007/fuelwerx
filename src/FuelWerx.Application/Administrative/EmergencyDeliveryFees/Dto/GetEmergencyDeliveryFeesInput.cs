using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFees.Dto
{
	public class GetEmergencyDeliveryFeesInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public GetEmergencyDeliveryFeesInput()
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