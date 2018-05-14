using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class CreateOrUpdateDriversDataInput : IInputDto, IDto, IValidate
	{
		[Required]
		public DriversDataEditDto DriversData
		{
			get;
			set;
		}

		public CreateOrUpdateDriversDataInput()
		{
		}
	}
}