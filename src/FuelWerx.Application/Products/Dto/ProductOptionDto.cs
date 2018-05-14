using Abp.AutoMapper;
using FuelWerx.Products;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapTo(new Type[] { typeof(ProductOption) })]
	public class ProductOptionDto
	{
		public virtual string Comment
		{
			get;
			set;
		}

		public virtual long Id
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

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public virtual string Value
		{
			get;
			set;
		}

		public ProductOptionDto()
		{
		}
	}
}