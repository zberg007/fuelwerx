using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	public class GetTaxRuleRuleForEditOutput : IOutputDto, IDto
	{
		public TaxRuleRuleEditDto TaxRuleRule
		{
			get;
			set;
		}

		public GetTaxRuleRuleForEditOutput()
		{
		}
	}
}