using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic
{
	[Table("FuelWerxFIPs")]
	public class FIP : FullAuditedEntity
	{
		[MaxLength(110)]
		public string CountyName
		{
			get;
			set;
		}

		[MaxLength(6)]
		public string FIPsCounty
		{
			get;
			set;
		}

		[MaxLength(4)]
		public string FIPsState
		{
			get;
			set;
		}

		[MaxLength(12)]
		public string FIPsStateCounty
		{
			get;
			set;
		}

		[MaxLength(110)]
		public string StateName
		{
			get;
			set;
		}

		public FIP()
		{
		}
	}
}