using Abp.Domain.Entities.Auditing;
using FuelWerx.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Invoices
{
	[Table("FuelWerxInvoiceTeamMembers")]
	public class InvoiceTeamMember : FullAuditedEntity<long>
	{
		[ForeignKey("InvoiceId")]
		public virtual FuelWerx.Invoices.Invoice Invoice
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

		[ForeignKey("TeamMemberId")]
		public virtual User TeamMember
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

		public InvoiceTeamMember()
		{
		}
	}
}