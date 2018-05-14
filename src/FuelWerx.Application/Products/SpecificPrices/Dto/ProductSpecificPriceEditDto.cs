using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Products;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.SpecificPrices.Dto
{
	[AutoMapTo(new Type[] { typeof(ProductSpecificPrice) })]
	public class ProductSpecificPriceEditDto : IValidate, IPassivable
	{
		public virtual DateTime? AvailableFrom
		{
			get;
			set;
		}

		public virtual DateTime? AvailableTo
		{
			get;
			set;
		}

		[Required]
		public virtual decimal BaseCost
		{
			get;
			set;
		}

		[Required]
		public virtual bool BaseCostOverride
		{
			get;
			set;
		}

		[Required]
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual decimal? Discount
		{
			get;
			set;
		}

		public virtual bool? DiscountIncludeTax
		{
			get;
			set;
		}

		public virtual int? ForCountryId
		{
			get;
			set;
		}

		public virtual string ForCurrency
		{
			get;
			set;
		}

		public virtual long? ForCustomerId
		{
			get;
			set;
		}

		public virtual long? ForOrganizationalUnitId
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

		public virtual FuelWerx.Products.ProductOption ProductOption
		{
			get;
			set;
		}

		[ForeignKey("ProductOption")]
		public virtual long? ProductOptionId
		{
			get;
			set;
		}

		public virtual int? StartingAtQuantity
		{
			get;
			set;
		}

		public ProductSpecificPriceEditDto()
		{
		}
	}
}