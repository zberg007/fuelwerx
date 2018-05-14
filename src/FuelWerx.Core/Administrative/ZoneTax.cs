using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative
{
	[Table("FuelWerxZoneTaxes")]
	public class ZoneTax : FullAuditedEntity<long>
	{
		[Required]
		public virtual bool IsActive
		{
			get;
			set;
		}

		[ForeignKey("TaxId")]
		public virtual FuelWerx.Administrative.Tax Tax
		{
			get;
			set;
		}

		[Required]
		public virtual long TaxId
		{
			get;
			set;
		}

		[ForeignKey("ZoneId")]
		public virtual FuelWerx.Administrative.Zone Zone
		{
			get;
			set;
		}

		[Required]
		public virtual long ZoneId
		{
			get;
			set;
		}

		public ZoneTax()
		{
		}
	}
}