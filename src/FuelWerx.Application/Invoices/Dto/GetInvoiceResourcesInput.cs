using Abp.Application.Services.Dto;
using FuelWerx.Invoices;
using System;

namespace FuelWerx.Invoices.Dto
{
	public class GetInvoiceResourcesInput : ListResultDto<InvoiceResource>
	{
		public GetInvoiceResourcesInput()
		{
		}
	}
}