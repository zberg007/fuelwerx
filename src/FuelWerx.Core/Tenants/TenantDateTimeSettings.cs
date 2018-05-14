using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants
{
	[Table("FuelWerxTenantDateTimeSettings")]
	public class TenantDateTimeSettings : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxTimezoneIdLength = 70;

		public virtual int TenantId
		{
			get;
			set;
		}

		[MaxLength(70)]
		[Required]
		public virtual string TimezoneId
		{
			get;
			set;
		}

		public TenantDateTimeSettings()
		{
		}
	}
}