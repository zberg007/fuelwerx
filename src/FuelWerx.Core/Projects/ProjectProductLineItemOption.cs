using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Projects
{
	[Table("FuelWerxProjectProductLineItemOptions")]
	public class ProjectProductLineItemOption : FullAuditedEntity<long>
	{
		public virtual ProjectProductLineItem ProductLineItem
		{
			get;
			set;
		}

		[ForeignKey("ProductLineItem")]
		public virtual long ProductLineItemId
		{
			get;
			set;
		}

		[Required]
		public virtual long ProductOptionId
		{
			get;
			set;
		}

		public ProjectProductLineItemOption()
		{
		}
	}
}