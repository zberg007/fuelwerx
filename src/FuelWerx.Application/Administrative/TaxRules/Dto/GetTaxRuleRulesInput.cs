using Abp.Runtime.Validation;
using FuelWerx.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	public class GetTaxRuleRulesInput : PagedAndSortedInputDto, IShouldNormalize
	{
		public string Filter
		{
			get;
			set;
		}

		public long? TaxRuleId
		{
			get;
			set;
		}

		public GetTaxRuleRulesInput()
		{
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(base.Sorting))
			{
				base.Sorting = "CountryId,CountryRegionId,Behavior,Caption";
			}
		}
	}
}