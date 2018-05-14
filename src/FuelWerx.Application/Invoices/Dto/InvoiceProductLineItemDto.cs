using Abp.AutoMapper;
using FuelWerx.Invoices;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapTo(new Type[] { typeof(InvoiceProductLineItem) })]
	public class InvoiceProductLineItemDto
	{
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual ICollection<InvoiceProductLineItemOptionDto> Options
		{
			get;
			set;
		}

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

		public InvoiceProductLineItemDto()
		{
		}
	}
}