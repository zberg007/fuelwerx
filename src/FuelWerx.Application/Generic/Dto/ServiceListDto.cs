using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	[AutoMapFrom(new System.Type[] { typeof(Service) })]
	public class ServiceListDto : FullAuditedEntityDto
	{
		public FuelWerx.Generic.Address Address
		{
			get;
			set;
		}

		public virtual long AddressId
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

		public virtual string RequestedServices
		{
			get;
			set;
		}

		public virtual string Type
		{
			get;
			set;
		}

		public ServiceListDto()
		{
		}
	}
}