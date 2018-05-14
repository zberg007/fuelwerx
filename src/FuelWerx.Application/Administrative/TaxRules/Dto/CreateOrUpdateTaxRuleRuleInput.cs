using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	public class CreateOrUpdateTaxRuleRuleInput : IInputDto, IDto, IValidate
	{
		[Required]
		public TaxRuleRuleEditDto TaxRuleRule
		{
			get;
			set;
		}

		public CreateOrUpdateTaxRuleRuleInput()
		{
		}
	}
}