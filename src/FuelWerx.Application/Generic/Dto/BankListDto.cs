using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new Type[] { typeof(Bank) })]
	public class BankListDto : FullAuditedEntityDto
	{
		public virtual string AccountNumber
		{
			get;
			set;
		}

		public virtual string AccountType
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

		public virtual string Note
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

		public virtual string RoutingNumber
		{
			get;
			set;
		}

		public BankListDto()
		{
		}
	}
}