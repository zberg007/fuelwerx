using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Zones.Dto
{
	[AutoMapFrom(new Type[] { typeof(Zone) })]
	public class ZoneListDto : FullAuditedEntityDto
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

		public virtual string ZoneTaxesAsDelimitedString
		{
			get;
			set;
		}

		public ZoneListDto()
		{
		}
	}
}