using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	public class GetInvoiceTeamMembersForEditOutput : IOutputDto, IDto
	{
		public List<InvoiceTeamMemberEditDto> InvoiceTeamMembers
		{
			get;
			set;
		}

		public GetInvoiceTeamMembersForEditOutput()
		{
		}
	}
}