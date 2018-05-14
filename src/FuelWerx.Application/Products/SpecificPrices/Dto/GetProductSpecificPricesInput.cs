using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.SpecificPrices.Dto
{
	public class GetProductSpecificPricesInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public long ProductId
		{
			get;
			set;
		}

		public GetProductSpecificPricesInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "Cost";
			}
		}
	}
}