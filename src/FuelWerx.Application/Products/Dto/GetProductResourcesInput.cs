using Abp.Application.Services.Dto;
using FuelWerx.Products;
using System;

namespace FuelWerx.Products.Dto
{
	public class GetProductResourcesInput : ListResultDto<ProductResource>
	{
		public GetProductResourcesInput()
		{
		}
	}
}