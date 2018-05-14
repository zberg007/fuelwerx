using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Customers.Dto;
using FuelWerx.Generic.Dto;
using FuelWerx.Organizations.Dto;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.SpecificPrices.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProductSpecificPrice) })]
	public class ProductSpecificPriceListDto : FullAuditedEntityDto
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

		public virtual decimal BaseCost
		{
			get;
			set;
		}

		public virtual bool BaseCostOverride
		{
			get;
			set;
		}

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

		public virtual string DiscountType
		{
			get;
			set;
		}

		[ForeignKey("ForCountryId")]
		public virtual CountryDto ForCountry
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
		public virtual CustomerDto ForCustomer
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
		public virtual OrganizationUnitDto ForOrganizationalUnit
		{
			get;
			set;
		}

		public virtual long? ForOrganizationalUnitId
		{
			get;
			set;
		}

		public virtual bool IsActive
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

		[ForeignKey("ProductOptionId")]
		public virtual ProductOptionDto ProductOption
		{
			get;
			set;
		}

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

		public ProductSpecificPriceListDto()
		{
		}
	}
}