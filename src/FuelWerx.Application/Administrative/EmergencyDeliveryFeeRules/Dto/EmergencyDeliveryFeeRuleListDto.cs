using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Administrative;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto
{
	[AutoMapFrom(new Type[] { typeof(EmergencyDeliveryFeeRule) })]
	public class EmergencyDeliveryFeeRuleListDto : FullAuditedEntityDto
	{
		public virtual string Caption
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

		public EmergencyDeliveryFeeRuleListDto()
		{
		}
	}
}