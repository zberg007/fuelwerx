using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Products;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Prices.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProductPrice) })]
	public class ProductPriceListDto : FullAuditedEntityDto
	{
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual bool Historical
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual DateTime? MarkedHistoricalAt
		{
			get;
			set;
		}

		public virtual FuelWerx.Products.Product Product
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual string ProductPriceTaxRulesAsDelimitedString
		{
			get;
			set;
		}

		public virtual decimal? SpecialDeliveryFee
		{
			get;
			set;
		}

		public virtual decimal UnitPrice
		{
			get;
			set;
		}

		public ProductPriceListDto()
		{
		}
	}
}