using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.ServiceTanks
{
	[Table("FuelWerxServiceTanks")]
	public class ServiceTank : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxNumberLength = 16;

		public virtual decimal BaseTemperature
		{
			get;
			set;
		}

		[Required]
		public virtual decimal Capacity
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string Description
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
		public virtual bool IsOwned
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string LastInspectionComments
		{
			get;
			set;
		}

		public virtual DateTime? LastInspectionDate
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

		[MaxLength(16)]
		public virtual string Number
		{
			get;
			set;
		}

		[Required]
		public virtual decimal RemainingCapacity
		{
			get;
			set;
		}

		[Required]
		public virtual long ServiceId
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public ServiceTank()
		{
		}
	}
}