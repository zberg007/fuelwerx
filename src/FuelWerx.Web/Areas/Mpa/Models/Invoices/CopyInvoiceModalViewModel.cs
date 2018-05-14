using Abp.AutoMapper;
using FuelWerx.Invoices.Dto;
using System;

namespace FuelWerx.Web.Areas.Mpa.Models.Invoices
{
	[AutoMapFrom(new Type[] { typeof(GetInvoiceForCopyOutput) })]
	public class CopyInvoiceModalViewModel : GetInvoiceForCopyOutput
	{
		public CopyInvoiceModalViewModel(GetInvoiceForCopyOutput output)
		{
			output.MapTo<GetInvoiceForCopyOutput, CopyInvoiceModalViewModel>(this);
		}
	}
}