using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Taxes.Dto
{
	public class CreateOrUpdateTaxInput : IInputDto, IDto, IValidate
	{
		[Required]
		public TaxEditDto Tax
		{
			get;
			set;
		}

		public CreateOrUpdateTaxInput()
		{
		}
	}
}