using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxDriversData")]
	public class DriversData : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxCDLNumberLength = 50;

		[Required]
		public virtual DateTime CDLExpiration
		{
			get;
			set;
		}

		[MaxLength(50)]
		[Required]
		public virtual string CDLNumber
		{
			get;
			set;
		}

		public virtual bool? HasHazmat
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
		public virtual long OwnerId
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public DriversData()
		{
		}
	}
}