using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new System.Type[] { typeof(Phone) })]
	public class PhoneEditDto : IValidate, IPassivable
	{
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
		public virtual long OwnerId
		{
			get;
			set;
		}

		[Required]
		public virtual string OwnerType
		{
			get;
			set;
		}

		[RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage="Format must be valid phone number (555-555-5555).")]
		[StringLength(16)]
		public virtual string PhoneNumber
		{
			get;
			set;
		}

		[Required]
		[StringLength(50)]
		public virtual string Type
		{
			get;
			set;
		}

		public PhoneEditDto()
		{
		}
	}
}