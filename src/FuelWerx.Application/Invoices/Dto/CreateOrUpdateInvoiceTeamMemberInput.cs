using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapFrom(new Type[] { typeof(CreateOrUpdateInvoiceTeamMemberInput) })]
	public class CreateOrUpdateInvoiceTeamMemberInput : IInputDto, IDto, IValidate
	{
		[Required]
		public virtual long? InvoiceId
		{
			get;
			set;
		}

		public List<InvoiceTeamMemberEditDto> InvoiceTeamMembers
		{
			get;
			set;
		}

		public CreateOrUpdateInvoiceTeamMemberInput()
		{
		}
	}
}