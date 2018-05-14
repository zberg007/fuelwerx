using Abp.AutoMapper;
using FuelWerx.Invoices.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Invoices
{
	[AutoMapFrom(new Type[] { typeof(GetInvoicePaymentForAddOutput) })]
	public class CreateOrViewPaymentModalViewModel : GetInvoicePaymentForAddOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.InvoicePayment.Id.HasValue;
			}
		}

		public CreateOrViewPaymentModalViewModel(GetInvoicePaymentForAddOutput output)
		{
			output.MapTo<GetInvoicePaymentForAddOutput, CreateOrViewPaymentModalViewModel>(this);
		}
	}
}