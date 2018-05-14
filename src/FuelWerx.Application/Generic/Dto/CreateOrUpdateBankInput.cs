using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class CreateOrUpdateBankInput : IInputDto, IDto, IValidate
	{
		[Required]
		public BankEditDto Bank
		{
			get;
			set;
		}

		public CreateOrUpdateBankInput()
		{
		}
	}
}