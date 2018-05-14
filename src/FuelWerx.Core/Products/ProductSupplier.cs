using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Suppliers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Products
{
	[Table("FuelWerxProductSuppliers")]
	public class ProductSupplier : FullAuditedEntity<long>, IMustHaveTenant
	{
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

		[ForeignKey("SupplierId")]
		public virtual FuelWerx.Suppliers.Supplier Supplier
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

		public virtual int TenantId
		{
			get;
			set;
		}

		public ProductSupplier()
		{
		}
	}
}