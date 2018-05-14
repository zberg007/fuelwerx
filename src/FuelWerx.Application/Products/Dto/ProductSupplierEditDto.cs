using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Products;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProductSupplier) })]
	public class ProductSupplierEditDto : IValidate, IPassivable
	{
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

		[Required]
		public virtual long ProductId
		{
			get;
			set;
		}

		[Required]
		public virtual long SupplierId
		{
			get;
			set;
		}

		public virtual int? TenantId
		{
			get;
			set;
		}

		public ProductSupplierEditDto()
		{
		}
	}
}