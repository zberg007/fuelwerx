using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative
{
	[Table("FuelWerxEmergencyDeliveryFees")]
	public class EmergencyDeliveryFee : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxCaptionLength = 600;

		[MaxLength(600)]
		public virtual string Caption
		{
			get;
			set;
		}

		[Required]
		public virtual decimal Fee
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

		public virtual int TenantId
		{
			get;
			set;
		}

		[ForeignKey("ZoneId")]
		public virtual FuelWerx.Administrative.Zone Zone
		{
			get;
			set;
		}

		public virtual long? ZoneId
		{
			get;
			set;
		}

		public EmergencyDeliveryFee()
		{
		}
	}
}