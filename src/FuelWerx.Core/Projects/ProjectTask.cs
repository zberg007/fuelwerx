using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects
{
	[Table("FuelWerxProjectTasks")]
	public class ProjectTask : FullAuditedEntity<long>, IMustHaveTenant
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

		[ForeignKey("ProjectId")]
		public virtual FuelWerx.Projects.Project Project
		{
			get;
			set;
		}

		[Required]
		public virtual long ProjectId
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

		public ProjectTask()
		{
		}
	}
}