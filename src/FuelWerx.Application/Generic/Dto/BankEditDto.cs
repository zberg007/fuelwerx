using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new Type[] { typeof(Bank) })]
	public class BankEditDto : IValidate, IPassivable
	{
		[MaxLength(255)]
		[Required]
		public virtual string AccountNumber
		{
			get;
			set;
		}

		[MaxLength(50)]
		[Required]
		public virtual string AccountType
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

		[StringLength(255)]
		public virtual string Name
		{
			get;
			set;
		}

		[MaxLength(600)]
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

		[StringLength(255)]
		public virtual string RoutingNumber
		{
			get;
			set;
		}

		public BankEditDto()
		{
		}
	}
}