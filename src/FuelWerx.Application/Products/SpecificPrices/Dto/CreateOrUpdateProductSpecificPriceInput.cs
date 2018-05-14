using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.SpecificPrices.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateProductSpecificPriceInput) })]
	public class CreateOrUpdateProductSpecificPriceInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ProductSpecificPriceEditDto SpecificPrice
		{
			get;
			set;
		}

		public CreateOrUpdateProductSpecificPriceInput()
		{
		}
	}
}