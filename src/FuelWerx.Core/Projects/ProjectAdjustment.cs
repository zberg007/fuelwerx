using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects
{
	[Table("FuelWerxProjectAdjustments")]
	public class ProjectAdjustment : FullAuditedEntity<long>
	{
		public const int MaxNameLength = 255;

		public const int MaxDescriptionLength = 1200;

		public virtual decimal? Cost
		{
			get;
			set;
		}

		[MaxLength(1200)]
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
		public virtual bool IsTaxable
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

		public virtual FuelWerx.Projects.Project Project
		{
			get;
			set;
		}

		[ForeignKey("Project")]
		public virtual long ProjectId
		{
			get;
			set;
		}

		public virtual decimal? RetailCost
		{
			get;
			set;
		}

		public ProjectAdjustment()
		{
		}
	}
}