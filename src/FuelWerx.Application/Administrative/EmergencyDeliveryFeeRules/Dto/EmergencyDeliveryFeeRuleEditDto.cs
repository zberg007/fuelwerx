using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Administrative;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto
{
	[AutoMapTo(new Type[] { typeof(EmergencyDeliveryFeeRule) })]
	public class EmergencyDeliveryFeeRuleEditDto : IValidate, IPassivable
	{
		[MaxLength(600)]
		public virtual string Caption
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

		public virtual int? TenantId
		{
			get;
			set;
		}

		public EmergencyDeliveryFeeRuleEditDto()
		{
		}
	}
}