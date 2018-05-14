using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using FuelWerx.Authorization.Roles;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Roles.Dto
{
	[AutoMapFrom(new Type[] { typeof(Role) })]
	public class RoleListDto : EntityDto, IHasCreationTime
	{
		public DateTime CreationTime
		{
			get;
			set;
		}

		public string DisplayName
		{
			get;
			set;
		}

		public bool IsDefault
		{
			get;
			set;
		}

		public bool IsStatic
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public RoleListDto()
		{
		}
	}
}