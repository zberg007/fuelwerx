using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers.Dto
{
	public class GetSuppliersInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public GetSuppliersInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "Name,CountryId,CountryRegionId,City";
			}
		}
	}
}