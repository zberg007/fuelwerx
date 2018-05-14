using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Estimates
{
	[Table("FuelWerxEstimateProductLineItems")]
	public class EstimateProductLineItem : FullAuditedEntity<long>
	{
		public virtual decimal Cost
		{
			get;
			set;
		}

		public virtual FuelWerx.Estimates.Estimate Estimate
		{
			get;
			set;
		}

		[ForeignKey("Estimate")]
		public virtual long EstimateId
		{
			get;
			set;
		}

		public virtual ICollection<EstimateProductLineItemOption> Options
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

		public virtual int Quantity
		{
			get;
			set;
		}

		public EstimateProductLineItem()
		{
		}
	}
}