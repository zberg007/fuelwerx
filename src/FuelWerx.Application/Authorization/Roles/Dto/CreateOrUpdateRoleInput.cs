using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Roles.Dto
{
	public class CreateOrUpdateRoleInput : IInputDto, IDto, IValidate
	{
		[Required]
		public List<string> GrantedPermissionNames
		{
			get;
			set;
		}

		[Required]
		public RoleEditDto Role
		{
			get;
			set;
		}

		public CreateOrUpdateRoleInput()
		{
		}
	}
}