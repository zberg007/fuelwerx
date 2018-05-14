using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Prices.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateProductPriceInput) })]
	public class CreateOrUpdateProductPriceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ProductPriceEditDto Price
		{
			get;
			set;
		}

		public CreateOrUpdateProductPriceInput()
		{
		}
	}
}