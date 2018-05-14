using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative
{
	[Table("FuelWerxContacts")]
	public class Contact : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxTitleLength = 255;

		public const int MaxEmailLength = 600;

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string Description
		{
			get;
			set;
		}

		[EmailAddress]
		[MaxLength(600)]
		[Required]
		public virtual string Email
		{
			get;
			set;
		}

		public virtual Guid? ImageId
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

		public virtual int TenantId
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string Title
		{
			get;
			set;
		}

		public Contact()
		{
		}
	}
}