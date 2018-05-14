using Abp.AutoMapper;
using FuelWerx.Products;
using FuelWerx.Products.Prices.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapTo(new Type[] { typeof(Product) })]
	public class ProductDto
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

		public long Id
		{
			get;
			set;
		}

		public Guid? ImageId
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

		public virtual ICollection<ProductOptionDto> Options
		{
			get;
			set;
		}

		public virtual ICollection<PriceDto> Prices
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

		public virtual int? TenantId
		{
			get;
			set;
		}

		public ProductDto()
		{
		}
	}
}