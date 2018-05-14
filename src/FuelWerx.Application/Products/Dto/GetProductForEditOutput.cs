using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	public class GetProductForEditOutput : IOutputDto, IDto
	{
		public byte[] Image
		{
			get;
			set;
		}

		public ProductEditDto Product
		{
			get;
			set;
		}

		public GetProductForEditOutput()
		{
		}
	}
}