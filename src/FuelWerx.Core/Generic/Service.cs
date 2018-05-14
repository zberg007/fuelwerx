using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.ServiceTanks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxServices")]
	public class Service : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxTypeLength = 255;

		public const int MaxRequestedServicesLength = 1200;

		[ForeignKey("AddressId")]
		public FuelWerx.Generic.Address Address
		{
			get;
			set;
		}

		[Required]
		public virtual long AddressId
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

		[Column(TypeName="nvarchar(MAX)")]
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

		[MaxLength(1200)]
		[Required]
		public virtual string RequestedServices
		{
			get;
			set;
		}

		public ICollection<ServiceTank> Tanks
		{
			get;
			set;
		}

		[NotMapped]
		public virtual int TankTotal
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
		public virtual string Type
		{
			get;
			set;
		}

		public Service()
		{
		}
	}
}