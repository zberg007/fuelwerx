using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class GetInvoiceForEditOutput : IOutputDto, IDto
	{
		public InvoiceEditDto Invoice
		{
			get;
			set;
		}

		public GetInvoiceForEditOutput()
		{
		}
	}
}