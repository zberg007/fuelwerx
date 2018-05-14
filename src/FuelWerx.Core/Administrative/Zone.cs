using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative
{
	[Table("FuelWerxZones")]
	public class Zone : FullAuditedEntity<long>, IMustHaveTenant
	{
		public const int MaxNameLength = 255;

		public const int MaxCaptionLength = 600;

		public const int MaxZoneHexColorLength = 12;

		[MaxLength(600)]
		public virtual string Caption
		{
			get;
			set;
		}

		[MaxLength(12)]
		public virtual string HexColor
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

		[MaxLength(255)]
		[Required]
		public virtual string Name
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string PolygonCoordinates
		{
			get;
			set;
		}

		[Column(TypeName="nvarchar(MAX)")]
		public virtual string PolygonCoordinatesReversed
		{
			get;
			set;
		}

		public virtual ICollection<ZoneTax> Taxes
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		[NotMapped]
		public virtual string ZoneTaxesAsDelimitedString
		{
			get
			{
				if (this.Taxes.Count == 0)
				{
					return string.Empty;
				}
				return string.Join(", ", 
					from i in this.Taxes
					select i.Tax.Name);
			}
		}

		public Zone()
		{
		}
	}
}