using Abp.AutoMapper;
using FuelWerx.Estimates;
using FuelWerx.Products.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates.Dto
{
	[AutoMapTo(new Type[] { typeof(EstimateProduct) })]
	public class EstimateProductDto
	{
		public virtual long EstimateId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual EstimateProductLineItemDto LineItem
		{
			get;
			set;
		}

		public virtual ProductDto Product
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public EstimateProductDto()
		{
		}
	}
}