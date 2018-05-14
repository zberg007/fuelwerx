using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Suppliers.Dto
{
	public class CreateOrUpdateSupplierInput : IInputDto, IDto, IValidate
	{
		public int CountryId
		{
			get;
			set;
		}

		[Required]
		public SupplierEditDto Supplier
		{
			get;
			set;
		}

		public CreateOrUpdateSupplierInput()
		{
		}
	}
}