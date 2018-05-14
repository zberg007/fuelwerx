using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxCountryRegions")]
	public class CountryRegion : FullAuditedEntity
	{
		public string Code
		{
			get;
			set;
		}

		[ForeignKey("CountryId")]
		public virtual FuelWerx.Generic.Country Country
		{
			get;
			set;
		}

		public int CountryId
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public CountryRegion()
		{
		}
	}
}