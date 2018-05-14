using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	public class GetEstimatesInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public GetEstimatesInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "Number,Label";
			}
		}
	}
}