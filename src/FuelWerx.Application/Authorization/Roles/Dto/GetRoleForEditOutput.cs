using Abp.Application.Services.Dto;
using FuelWerx.Authorization.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Roles.Dto
{
	public class GetRoleForEditOutput : IOutputDto, IDto
	{
		public List<string> GrantedPermissionNames
		{
			get;
			set;
		}

		public List<FlatPermissionDto> Permissions
		{
			get;
			set;
		}

		public RoleEditDto Role
		{
			get;
			set;
		}

		public GetRoleForEditOutput()
		{
		}
	}
}