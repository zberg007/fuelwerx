using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new Type[] { typeof(DriversData) })]
	public class DriversDataEditDto : IValidate, IPassivable
	{
		[Required]
		public virtual DateTime CDLExpiration
		{
			get;
			set;
		}

		[MaxLength(50)]
		[Required]
		public virtual string CDLNumber
		{
			get;
			set;
		}

		public virtual bool? HasHazmat
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
		public virtual long OwnerId
		{
			get;
			set;
		}

		public virtual int TenantId
		{
			get;
			set;
		}

		public DriversDataEditDto()
		{
		}
	}
}