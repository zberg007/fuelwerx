using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.MultiTenancy.Dto
{
	public class GetTenantsInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public GetTenantsInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "TenancyName";
			}
			base.Sorting = base.Sorting.Replace("editionDisplayName", "Edition.DisplayName");
		}
	}
}