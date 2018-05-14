using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Trucks
{
	[Table("FuelWerxTrucks")]
	public class Truck : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxNumberLength = 16;

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

		public virtual int TenantId
		{
			get;
			set;
		}

		public Truck()
		{
		}
	}
}