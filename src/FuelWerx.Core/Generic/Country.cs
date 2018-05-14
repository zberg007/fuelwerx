using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxCountries")]
	public class Country : FullAuditedEntity
	{
		public string Code
		{
			get;
			set;
		}

		public virtual IList<CountryRegion> CountryRegions
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public Country()
		{
		}
	}
}