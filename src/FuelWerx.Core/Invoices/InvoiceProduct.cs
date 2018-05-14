using Abp.Domain.Entities.Auditing;
using FuelWerx.Products;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices
{
	[Table("FuelWerxInvoiceProducts")]
	public class InvoiceProduct : FullAuditedEntity<long>
	{
		[ForeignKey("InvoiceId")]
		public virtual FuelWerx.Invoices.Invoice Invoice
		{
			get;
			set;
		}

		[Required]
		public virtual long InvoiceId
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

		public virtual InvoiceProductLineItem LineItem
		{
			get;
			set;
		}

		[ForeignKey("LineItem")]
		public virtual long? LineItemId
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

		public InvoiceProduct()
		{
		}
	}
}