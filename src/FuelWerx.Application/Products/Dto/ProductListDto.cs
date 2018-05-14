using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Products;
using FuelWerx.Products.Prices.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapFrom(new Type[] { typeof(Product) })]
	public class ProductListDto : FullAuditedEntityDto
	{
		public virtual decimal BasePrice
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual decimal FinalPrice
		{
			get;
			set;
		}

		public virtual Guid? ImageId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual ICollection<ProductOptionDto> ProductOptions
		{
			get;
			set;
		}

		public virtual ICollection<PriceDto> ProductPrices
		{
			get;
			set;
		}

		public virtual ICollection<ProductResource> ProductResources
		{
			get;
			set;
		}

		public virtual ICollection<ProductSupplier> ProductSuppliers
		{
			get;
			set;
		}

		public virtual int QuantityOnHand
		{
			get;
			set;
		}

		public virtual string QuantitySoldIn
		{
			get;
			set;
		}

		public virtual string Reference
		{
			get;
			set;
		}

		public virtual string Sku
		{
			get;
			set;
		}

		public virtual decimal Surcharge
		{
			get;
			set;
		}

		public ProductListDto()
		{
		}
	}
}