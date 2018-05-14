using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Invoices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices.Dto
{
	[AutoMapFrom(new Type[] { typeof(InvoiceTeamMember) })]
	public class InvoiceTeamMemberEditDto : IValidate, IPassivable
	{
		public long? Id
		{
			get;
			set;
		}

		[Required]
		public virtual long InvoiceId
		{
			get;
			set;
		}

		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[Required]
		public virtual long TeamMemberId
		{
			get;
			set;
		}

		public InvoiceTeamMemberEditDto()
		{
		}
	}
}