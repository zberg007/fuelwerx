using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class GetInvoicePaymentForAddOutput : IOutputDto, IDto
	{
		public InvoicePaymentAddDto InvoicePayment
		{
			get;
			set;
		}

		public GetInvoicePaymentForAddOutput()
		{
		}
	}
}