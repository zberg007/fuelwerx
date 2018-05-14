using Abp.AutoMapper;
using FuelWerx.Invoices;
using FuelWerx.Products.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapTo(new Type[] { typeof(InvoiceProduct) })]
	public class InvoiceProductDto
	{
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

		public virtual InvoiceProductLineItemDto LineItem
		{
			get;
			set;
		}

		public virtual ProductDto Product
		{
			get;
			set;
		}

		public virtual long ProductId
		{
			get;
			set;
		}

		public InvoiceProductDto()
		{
		}
	}
}