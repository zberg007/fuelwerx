using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices
{
	[Table("FuelWerxInvoiceProductLineItemOptions")]
	public class InvoiceProductLineItemOption : FullAuditedEntity<long>
	{
		public virtual InvoiceProductLineItem ProductLineItem
		{
			get;
			set;
		}

		[ForeignKey("ProductLineItem")]
		public virtual long ProductLineItemId
		{
			get;
			set;
		}

		[Required]
		public virtual long ProductOptionId
		{
			get;
			set;
		}

		public InvoiceProductLineItemOption()
		{
		}
	}
}