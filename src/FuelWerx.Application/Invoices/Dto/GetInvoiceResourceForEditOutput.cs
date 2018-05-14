using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class GetInvoiceResourceForEditOutput : IOutputDto, IDto
	{
		public List<InvoiceResourceEditDto> InvoiceResources
		{
			get;
			set;
		}

		public GetInvoiceResourceForEditOutput()
		{
		}
	}
}