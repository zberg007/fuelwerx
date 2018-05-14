using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	public class UserRoleDto : IDto
	{
		public bool IsAssigned
		{
			get;
			set;
		}

		public string RoleDisplayName
		{
			get;
			set;
		}

		public int RoleId
		{
			get;
			set;
		}

		public string RoleName
		{
			get;
			set;
		}

		public UserRoleDto()
		{
		}
	}
}