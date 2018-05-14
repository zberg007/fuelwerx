using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Zones.Dto;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFees.Dto
{
	[AutoMapFrom(new Type[] { typeof(EmergencyDeliveryFee) })]
	public class EmergencyDeliveryFeeListDto : FullAuditedEntityDto
	{
		public virtual string Caption
		{
			get;
			set;
		}

		public virtual decimal Fee
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

		[ForeignKey("ZoneId")]
		public virtual ZoneDto Zone
		{
			get;
			set;
		}

		public virtual long? ZoneId
		{
			get;
			set;
		}

		public EmergencyDeliveryFeeListDto()
		{
		}
	}
}