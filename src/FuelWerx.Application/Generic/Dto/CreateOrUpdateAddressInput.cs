using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class CreateOrUpdateAddressInput : IInputDto, IDto, IValidate
	{
		[Required]
		public AddressEditDto Address
		{
			get;
			set;
		}

		public int CountryId
		{
			get;
			set;
		}

		public CreateOrUpdateAddressInput()
		{
		}
	}
}