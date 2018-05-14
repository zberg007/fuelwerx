using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using FuelWerx.Customers;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products
{
	[Table("FuelWerxProductSpecificPrices")]
	public class ProductSpecificPrice : FullAuditedEntity<long>, IMustHaveTenant
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

		[ForeignKey("ForCountryId")]
		public virtual Country ForCountry
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

		[ForeignKey("ForCustomerId")]
		public virtual Customer ForCustomer
		{
			get;
			set;
		}

		public virtual long? ForCustomerId
		{
			get;
			set;
		}

		[ForeignKey("ForOrganizationalUnitId")]
		public virtual Abp.Organizations.OrganizationUnit ForOrganizationalUnit
		{
			get;
			set;
		}

		public virtual long? ForOrganizationalUnitId
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

		public virtual int TenantId
		{
			get;
			set;
		}

		public ProductSpecificPrice()
		{
		}
	}
}