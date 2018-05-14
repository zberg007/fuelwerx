using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Products;
using FuelWerx.Suppliers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products.Dto
{
	[AutoMapFrom(new Type[] { typeof(ProductSupplier) })]
	public class ProductSupplierListDto : FullAuditedEntityDto
	{
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

		public virtual long ProductId
		{
			get;
			set;
		}

		[ForeignKey("SupplierId")]
		public virtual FuelWerx.Suppliers.Supplier Supplier
		{
			get;
			set;
		}

		public virtual long SupplierId
		{
			get;
			set;
		}

		public ProductSupplierListDto()
		{
		}
	}
}