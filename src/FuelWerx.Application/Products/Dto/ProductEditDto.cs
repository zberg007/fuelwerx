using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapTo(new Type[] { typeof(Product) })]
	public class ProductEditDto : IValidate, IPassivable
	{
		[Required]
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

		[Required]
		public virtual decimal FinalPrice
		{
			get;
			set;
		}

		public long? Id
		{
			get;
			set;
		}

		public Guid? ImageId
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

		[Required]
		[StringLength(255)]
		public virtual string Name
		{
			get;
			set;
		}

		[AutoMapTo(new Type[] { typeof(ProductOption) })]
		public virtual ICollection<ProductOption> ProductOptions
		{
			get;
			set;
		}

		[Required]
		public virtual int QuantityOnHand
		{
			get;
			set;
		}

		[Required]
		public virtual string QuantitySoldIn
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string Reference
		{
			get;
			set;
		}

		[MaxLength(99)]
		[Required]
		public virtual string Sku
		{
			get;
			set;
		}

		[Required]
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

		public ProductEditDto()
		{
		}
	}
}