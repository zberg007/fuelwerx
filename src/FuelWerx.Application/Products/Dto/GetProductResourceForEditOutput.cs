using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	public class GetProductResourceForEditOutput : IOutputDto, IDto
	{
		public List<ProductResourceEditDto> ProductResources
		{
			get;
			set;
		}

		public GetProductResourceForEditOutput()
		{
		}
	}
}