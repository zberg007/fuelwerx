using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Prices.Dto
{
	public class GetProductPriceForEditOutput : IOutputDto, IDto
	{
		public ProductPriceEditDto Price
		{
			get;
			set;
		}

		public GetProductPriceForEditOutput()
		{
		}
	}
}