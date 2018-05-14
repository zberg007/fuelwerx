using Abp.AutoMapper;
using FuelWerx.Invoices;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapTo(new Type[] { typeof(InvoiceTask) })]
	public class InvoiceTaskDto
	{
		public virtual string Comment
		{
			get;
			set;
		}

		public virtual decimal? Cost
		{
			get;
			set;
		}

		public virtual decimal? Discount
		{
			get;
			set;
		}

		public virtual long Id
		{
			get;
			set;
		}

		public virtual FuelWerx.Invoices.Invoice Invoice
		{
			get;
			set;
		}

		public virtual long InvoiceId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual bool IsComplete
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual decimal? Retail
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public InvoiceTaskDto()
		{
		}
	}
}