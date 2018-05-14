using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	public class UpdateUserPermissionsInput : IInputDto, IDto, IValidate
	{
		[Required]
		public List<string> GrantedPermissionNames
		{
			get;
			set;
		}

		[Range(1, 2147483647)]
		public long Id
		{
			get;
			set;
		}

		public UpdateUserPermissionsInput()
		{
		}
	}
}