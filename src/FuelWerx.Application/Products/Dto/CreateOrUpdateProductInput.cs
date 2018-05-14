using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	public class CreateOrUpdateProductInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ProductEditDto Product
		{
			get;
			set;
		}

		public byte?[] ProductImage
		{
			get;
			set;
		}

		public CreateOrUpdateProductInput()
		{
		}
	}
}