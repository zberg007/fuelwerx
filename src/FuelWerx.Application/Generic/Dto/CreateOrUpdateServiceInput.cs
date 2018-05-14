using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class CreateOrUpdateServiceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ServiceEditDto Service
		{
			get;
			set;
		}

		public CreateOrUpdateServiceInput()
		{
		}
	}
}