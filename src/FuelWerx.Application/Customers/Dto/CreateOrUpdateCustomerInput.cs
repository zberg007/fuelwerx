using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Customers.Dto
{
	public class CreateOrUpdateCustomerInput : IInputDto, IDto, IValidate
	{
		[Required]
		public CustomerEditDto Customer
		{
			get;
			set;
		}

		public CreateOrUpdateCustomerInput()
		{
		}
	}
}