using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.SpecificPrices.Dto
{
	public class GetProductSpecificPriceForEditOutput : IOutputDto, IDto
	{
		public ProductSpecificPriceEditDto SpecificPrice
		{
			get;
			set;
		}

		public GetProductSpecificPriceForEditOutput()
		{
		}
	}
}