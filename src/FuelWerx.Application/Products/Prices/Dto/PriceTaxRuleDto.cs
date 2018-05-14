using Abp.AutoMapper;
using FuelWerx.Administrative;
using FuelWerx.Products;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Prices.Dto
{
	[AutoMapTo(new Type[] { typeof(ProductPriceTaxRule) })]
	public class PriceTaxRuleDto
	{
		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual long ProductPriceId
		{
			get;
			set;
		}

		public virtual FuelWerx.Administrative.TaxRule TaxRule
		{
			get;
			set;
		}

		public virtual long TaxRuleId
		{
			get;
			set;
		}

		public PriceTaxRuleDto()
		{
		}
	}
}