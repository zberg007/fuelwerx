using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	public class GetTaxRuleForEditOutput : IOutputDto, IDto
	{
		public TaxRuleEditDto TaxRule
		{
			get;
			set;
		}

		public GetTaxRuleForEditOutput()
		{
		}
	}
}