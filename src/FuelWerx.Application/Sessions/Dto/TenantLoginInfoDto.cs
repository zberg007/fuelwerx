using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FuelWerx.MultiTenancy;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Sessions.Dto
{
	[AutoMapFrom(new Type[] { typeof(Tenant) })]
	public class TenantLoginInfoDto : EntityDto
	{
		public string EditionDisplayName
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

		public TenantLoginInfoDto()
		{
		}
	}
}