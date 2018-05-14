using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Authorization.Users;
using FuelWerx.Invoices;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapFrom(new Type[] { typeof(InvoiceTeamMember) })]
	public class InvoiceTeamMemberListDto : FullAuditedEntityDto
	{
		[ForeignKey("InvoiceId")]
		public virtual FuelWerx.Invoices.Invoice Invoice
		{
			get;
			set;
		}

		public virtual long InvoiceId
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual long TeamMemberId
		{
			get;
			set;
		}

		[ForeignKey("TeamMemberId")]
		public virtual FuelWerx.Authorization.Users.User User
		{
			get;
			set;
		}

		public InvoiceTeamMemberListDto()
		{
		}
	}
}