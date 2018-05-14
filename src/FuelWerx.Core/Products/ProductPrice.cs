using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Administrative;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products
{
	[Table("FuelWerxProductPrices")]
	public class ProductPrice : FullAuditedEntity<long>, IMustHaveTenant
	{
		[Required]
		public virtual decimal Cost
		{
			get;
			set;
		}

		[Required]
		public virtual bool Historical
		{
			get;
			set;
		}

		[Required]
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

		[ForeignKey("ProductId")]
		public virtual FuelWerx.Products.Product Product
		{
			get;
			set;
		}

		[Required]
		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual ICollection<ProductPriceTaxRule> ProductPriceTaxRules
		{
			get;
			set;
		}

		[NotMapped]
		public virtual string ProductPriceTaxRulesAsDelimitedString
		{
			get
			{
				if (this.ProductPriceTaxRules.Count == 0)
				{
					return string.Empty;
				}
				return string.Join(", ", 
					from i in this.ProductPriceTaxRules
					select i.TaxRule.Name);
			}
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

		[Required]
		public virtual decimal UnitPrice
		{
			get;
			set;
		}

		public ProductPrice()
		{
		}
	}
}