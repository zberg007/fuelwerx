using Abp.AutoMapper;
using FuelWerx.Invoices.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Invoices
{
	[AutoMapFrom(new Type[] { typeof(GetInvoiceTeamMembersForEditOutput) })]
	public class CreateOrUpdateInvoiceTeamMembersModalViewModel : GetInvoiceTeamMembersForEditOutput
	{
		public long InvoiceId
		{
			get;
			set;
		}

		public CreateOrUpdateInvoiceTeamMembersModalViewModel(GetInvoiceTeamMembersForEditOutput output)
		{
			output.MapTo<GetInvoiceTeamMembersForEditOutput, CreateOrUpdateInvoiceTeamMembersModalViewModel>(this);
		}
	}
}