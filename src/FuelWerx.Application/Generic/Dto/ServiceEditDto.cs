using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using FuelWerx.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapTo(new System.Type[] { typeof(Service) })]
	public class ServiceEditDto : IValidate, IPassivable
	{
		[Required]
		public virtual long AddressId
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

		[MaxLength(1200)]
		[Required]
		public virtual string RequestedServices
		{
			get;
			set;
		}

		public ICollection<ServiceTankDto> Tanks
		{
			get;
			set;
		}

		[MaxLength(255)]
		[Required]
		public virtual string Type
		{
			get;
			set;
		}

		public ServiceEditDto()
		{
		}
	}
}