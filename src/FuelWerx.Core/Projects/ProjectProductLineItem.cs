using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects
{
	[Table("FuelWerxProjectProductLineItems")]
	public class ProjectProductLineItem : FullAuditedEntity<long>
	{
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual ICollection<ProjectProductLineItemOption> Options
		{
			get;
			set;
		}

		[Required]
		public virtual long ProductId
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

		public virtual int Quantity
		{
			get;
			set;
		}

		public ProjectProductLineItem()
		{
		}
	}
}