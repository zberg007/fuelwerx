using FuelWerx.Customers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class InvoiceCopyDto
	{
		[ForeignKey("CustomerId")]
		public virtual FuelWerx.Customers.Customer Customer
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerEmail
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerFirstName
		{
			get;
			set;
		}

		[Required]
		public virtual long CustomerId
		{
			get;
			set;
		}

		[MaxLength(255)]
		public virtual string CustomerLastName
		{
			get;
			set;
		}

		[Required]
		public long InvoiceId
		{
			get;
			set;
		}

		[MaxLength(38)]
		[Required]
		public virtual string Number
		{
			get;
			set;
		}

		public InvoiceCopyDto()
		{
		}
	}
}