using Abp.AutoMapper;
using FuelWerx.Invoices;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapTo(new Type[] { typeof(InvoiceProductLineItemOption) })]
	public class InvoiceProductLineItemOptionDto
	{
		public virtual long Id
		{
			get;
			set;
		}

		public virtual long InvoiceId
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public virtual long ProductOptionId
		{
			get;
			set;
		}

		public InvoiceProductLineItemOptionDto()
		{
		}
	}
}