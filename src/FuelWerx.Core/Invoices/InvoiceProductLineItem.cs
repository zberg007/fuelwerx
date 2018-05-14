using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices
{
	[Table("FuelWerxInvoiceProductLineItems")]
	public class InvoiceProductLineItem : FullAuditedEntity<long>
	{
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual FuelWerx.Invoices.Invoice Invoice
		{
			get;
			set;
		}

		[ForeignKey("Invoice")]
		public virtual long InvoiceId
		{
			get;
			set;
		}

		public virtual ICollection<InvoiceProductLineItemOption> Options
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

		public virtual int Quantity
		{
			get;
			set;
		}

		public InvoiceProductLineItem()
		{
		}
	}
}