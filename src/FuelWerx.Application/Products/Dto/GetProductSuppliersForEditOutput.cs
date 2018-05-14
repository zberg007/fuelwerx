using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	public class GetProductSuppliersForEditOutput : IOutputDto, IDto
	{
		public List<ProductSupplierEditDto> ProductSuppliers
		{
			get;
			set;
		}

		public GetProductSuppliersForEditOutput()
		{
		}
	}
}