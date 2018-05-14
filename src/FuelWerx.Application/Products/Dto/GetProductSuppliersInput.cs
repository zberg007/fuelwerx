using Abp.Application.Services.Dto;
using FuelWerx.Products;
using System;

namespace FuelWerx.Products.Dto
{
	public class GetProductSuppliersInput : ListResultDto<ProductSupplier>
	{
		public GetProductSuppliersInput()
		{
		}
	}
}