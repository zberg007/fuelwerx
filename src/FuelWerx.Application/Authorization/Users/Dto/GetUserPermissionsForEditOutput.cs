using Abp.Application.Services.Dto;
using FuelWerx.Authorization.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	public class GetUserPermissionsForEditOutput : IOutputDto, IDto
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

		public GetUserPermissionsForEditOutput()
		{
		}
	}
}