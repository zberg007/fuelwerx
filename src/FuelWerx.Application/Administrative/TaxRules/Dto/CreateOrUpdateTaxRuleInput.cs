using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.TaxRules.Dto
{
	public class CreateOrUpdateTaxRuleInput : IInputDto, IDto, IValidate
	{
		[Required]
		public TaxRuleEditDto TaxRule
		{
			get;
			set;
		}

		public CreateOrUpdateTaxRuleInput()
		{
		}
	}
}