using Abp.AutoMapper;
using FuelWerx.Products;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Prices.Dto
{
	[AutoMapTo(new Type[] { typeof(ProductPrice) })]
	public class PriceDto
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

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual ICollection<PriceTaxRuleDto> ProductPriceTaxRules
		{
			get;
			set;
		}

		public virtual decimal? SpecialDeliveryFee
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public virtual decimal UnitPrice
		{
			get;
			set;
		}

		public PriceDto()
		{
		}
	}
}