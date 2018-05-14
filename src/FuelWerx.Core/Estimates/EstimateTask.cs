using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates
{
	[Table("FuelWerxEstimateTasks")]
	public class EstimateTask : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxCommentLength = 1200;

		[MaxLength(1200)]
		public virtual string Comment
		{
			get;
			set;
		}

		public virtual decimal? Cost
		{
			get;
			set;
		}

		public virtual decimal? Discount
		{
			get;
			set;
		}

		[ForeignKey("EstimateId")]
		public virtual FuelWerx.Estimates.Estimate Estimate
		{
			get;
			set;
		}

		[Required]
		public virtual long EstimateId
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
		public virtual bool IsComplete
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

		public virtual decimal? Retail
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public EstimateTask()
		{
		}
	}
}