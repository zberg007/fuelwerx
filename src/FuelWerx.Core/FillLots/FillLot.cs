using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.FillLotTanks;
using FuelWerx.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.FillLots
{
	[Table("FuelWerxFillLots")]
	public class FillLot : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxLabelLength = 255;

		public const int MaxShortLabelLength = 12;

		[ForeignKey("AddressId")]
		public virtual FuelWerx.Generic.Address Address
		{
			get;
			set;
		}

		public virtual long? AddressId
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

		[MaxLength(255)]
		[Required]
		public virtual string Label
		{
			get;
			set;
		}

		[MaxLength(12)]
		public virtual string ShortLabel
		{
			get;
			set;
		}

		public ICollection<FillLotTank> Tanks
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

		public FillLot()
		{
		}
	}
}