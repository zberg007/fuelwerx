using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class CreateOrUpdatePhoneInput : IInputDto, IDto, IValidate
	{
		[Required]
		public PhoneEditDto Phone
		{
			get;
			set;
		}

		public CreateOrUpdatePhoneInput()
		{
		}
	}
}