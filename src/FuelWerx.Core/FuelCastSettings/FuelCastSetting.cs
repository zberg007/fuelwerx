using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.FuelCastSettings
{
	[Table("FuelWerxTenantFuelCastSettings")]
	public class FuelCastSetting : FullAuditedEntity, IMustHaveTenant
	{
		[Required]
		public virtual bool AllowAnomolyModification
		{
			get;
			set;
		}

		public virtual DateTime? AllowAnomolyModificationDateTime
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
		public virtual int KFactorAnomoly_LastN
		{
			get;
			set;
		}

		[Required]
		public virtual decimal KFactorAnomoly_Range
		{
			get;
			set;
		}

		[Required]
		public virtual int KFactorAnomoly_TrendingDown
		{
			get;
			set;
		}

		[Required]
		public virtual int KFactorComparison_LastN
		{
			get;
			set;
		}

		[Required]
		public virtual decimal KFactorComparison_Range
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string Notes
		{
			get;
			set;
		}

		[Required]
		public virtual decimal OptimalDeliveryRange
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public FuelCastSetting()
		{
		}
	}
}