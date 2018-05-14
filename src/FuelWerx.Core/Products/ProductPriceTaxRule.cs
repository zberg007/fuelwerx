using Abp.Domain.Entities.Auditing;
using FuelWerx.Administrative;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products
{
	[Table("FuelWerxProductPriceTaxRules")]
	public class ProductPriceTaxRule : FullAuditedEntity<long>
	{
		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[ForeignKey("ProductPriceId")]
		public virtual FuelWerx.Products.ProductPrice ProductPrice
		{
			get;
			set;
		}

		[Required]
		public virtual long ProductPriceId
		{
			get;
			set;
		}

		[ForeignKey("TaxRuleId")]
		public virtual FuelWerx.Administrative.TaxRule TaxRule
		{
			get;
			set;
		}

		[Required]
		public virtual long TaxRuleId
		{
			get;
			set;
		}

		public ProductPriceTaxRule()
		{
		}
	}
}