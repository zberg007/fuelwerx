using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateProductSupplierInput) })]
	public class CreateOrUpdateProductSupplierInput : IInputDto, IDto, IValidate
	{
		[Required]
		public virtual long? ProductId
		{
			get;
			set;
		}

		public List<ProductSupplierEditDto> ProductSuppliers
		{
			get;
			set;
		}

		public CreateOrUpdateProductSupplierInput()
		{
		}
	}
}