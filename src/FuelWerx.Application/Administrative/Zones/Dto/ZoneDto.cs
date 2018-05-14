using Abp.AutoMapper;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Taxes.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Zones.Dto
{
	[AutoMap(new Type[] { typeof(Zone) })]
	public class ZoneDto
	{
		public virtual string Caption
		{
			get;
			set;
		}

		public virtual string HexColor
		{
			get;
			set;
		}

		public virtual long Id
		{
			get;
			set;
		}

		public virtual bool IsActive
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual string PolygonCoordinates
		{
			get;
			set;
		}

		public virtual string PolygonCoordinatesReversed
		{
			get;
			set;
		}

		public virtual ICollection<ZoneTaxDto> Taxes
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

		public ZoneDto()
		{
		}
	}
}