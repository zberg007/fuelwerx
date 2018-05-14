using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxBanks")]
	public class Bank : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxAccountTypeLength = 50;

		public const int MaxNameLength = 255;

		public const int MaxAccountNumberLength = 255;

		public const int MaxRoutingNumberLength = 255;

		public const int MaxNoteLength = 600;

		[MaxLength(255)]
		[Required]
		public virtual string AccountNumber
		{
			get;
			set;
		}

		[MaxLength(50)]
		[Required]
		public virtual string AccountType
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

		[MaxLength(255)]
		[Required]
		public virtual string Name
		{
			get;
			set;
		}

		[MaxLength(600)]
		public virtual string Note
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

		[MaxLength(255)]
		[Required]
		public virtual string RoutingNumber
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public Bank()
		{
		}
	}
}