using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using FuelWerx.MultiTenancy;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.MultiTenancy.Dto
{
	[AutoMapFrom(new Type[] { typeof(Tenant) })]
	public class TenantListDto : EntityDto, IPassivable, IHasCreationTime
	{
		public DateTime CreationTime
		{
			get;
			set;
		}

		public string EditionDisplayName
		{
			get;
			set;
		}

		public bool IsActive
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string TenancyName
		{
			get;
			set;
		}

		public TenantListDto()
		{
		}
	}
}