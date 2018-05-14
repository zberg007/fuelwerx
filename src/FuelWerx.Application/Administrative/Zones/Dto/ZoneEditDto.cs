using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Administrative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.Zones.Dto
{
	[AutoMap(new Type[] { typeof(Zone) })]
	public class ZoneEditDto : IValidate, IPassivable
	{
		[MaxLength(600)]
		public virtual string Caption
		{
			get;
			set;
		}

		[Required]
		[StringLength(12)]
		public virtual string HexColor
		{
			get;
			set;
		}

		public long? Id
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

		[Required]
		[StringLength(255)]
		public virtual string Name
		{
			get;
			set;
		}

		[Required]
		public virtual string PolygonCoordinates
		{
			get;
			set;
		}

		[Required]
		public virtual string PolygonCoordinatesReversed
		{
			get;
			set;
		}

		[AutoMapTo(new Type[] { typeof(ZoneTax) })]
		public ICollection<ZoneTax> Taxes
		{
			get;
			set;
		}

		public virtual int? TenantId
		{
			get;
			set;
		}

		public ZoneEditDto()
		{
		}
	}
}