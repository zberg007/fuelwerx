using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxPhones")]
	public class Phone : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxTypeLength = 50;

		public const int MaxNumberLength = 16;

		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[Required]
		public virtual long OwnerId
		{
			get;
			set;
		}

		[Required]
		public virtual string OwnerType
		{
			get;
			set;
		}

		[MaxLength(16)]
		[Phone]
		[Required]
		public virtual string PhoneNumber
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		[MaxLength(50)]
		[Required]
		public virtual string Type
		{
			get;
			set;
		}

		public Phone()
		{
		}
	}
}