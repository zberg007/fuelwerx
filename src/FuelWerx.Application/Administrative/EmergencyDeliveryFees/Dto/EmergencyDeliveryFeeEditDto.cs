using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Administrative;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelWerx.Administrative.EmergencyDeliveryFees.Dto
{
	[AutoMapTo(new Type[] { typeof(EmergencyDeliveryFee) })]
	public class EmergencyDeliveryFeeEditDto : IValidate, IPassivable
	{
		[MaxLength(600)]
		public virtual string Caption
		{
			get;
			set;
		}

		[Required]
		public virtual decimal Fee
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

		[ForeignKey("ZoneId")]
		public virtual FuelWerx.Administrative.Zone Zone
		{
			get;
			set;
		}

		public virtual long? ZoneId
		{
			get;
			set;
		}

		public EmergencyDeliveryFeeEditDto()
		{
		}
	}
}