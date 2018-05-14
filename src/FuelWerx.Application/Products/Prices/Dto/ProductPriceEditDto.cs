using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Prices.Dto
{
	[AutoMapTo(new Type[] { typeof(ProductPrice) })]
	public class ProductPriceEditDto : IValidate, IPassivable
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

		public long? Id
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

		public virtual decimal? SpecialDeliveryFee
		{
			get;
			set;
		}

		public virtual int? TenantId
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

		public ProductPriceEditDto()
		{
		}
	}
}