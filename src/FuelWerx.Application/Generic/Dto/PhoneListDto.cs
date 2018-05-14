using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new System.Type[] { typeof(Phone) })]
	public class PhoneListDto : FullAuditedEntityDto
	{
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

		[Phone]
		public virtual string PhoneNumber
		{
			get;
			set;
		}

		public virtual string Type
		{
			get;
			set;
		}

		public PhoneListDto()
		{
		}
	}
}