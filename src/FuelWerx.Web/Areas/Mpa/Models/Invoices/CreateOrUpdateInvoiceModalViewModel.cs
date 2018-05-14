using Abp.AutoMapper;
using FuelWerx.Invoices.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Invoices
{
	[AutoMapFrom(new Type[] { typeof(GetInvoiceForEditOutput) })]
	public class CreateOrUpdateInvoiceModalViewModel : GetInvoiceForEditOutput
	{
		public bool IsEditMode
		{
			get
			{
				return base.Invoice.Id.HasValue;
			}
		}

		public CreateOrUpdateInvoiceModalViewModel(GetInvoiceForEditOutput output)
		{
			output.MapTo<GetInvoiceForEditOutput, CreateOrUpdateInvoiceModalViewModel>(this);
		}
	}
}