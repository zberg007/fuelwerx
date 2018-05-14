using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class GetInvoiceForCopyOutput : IOutputDto, IDto
	{
		public InvoiceCopyDto Invoice
		{
			get;
			set;
		}

		public GetInvoiceForCopyOutput()
		{
		}
	}
}