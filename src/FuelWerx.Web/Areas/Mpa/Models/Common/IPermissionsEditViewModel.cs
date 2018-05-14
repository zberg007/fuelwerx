using FuelWerx.Authorization.Dto;
using System;
using System.Collections.Generic;

namespace FuelWerx.Web.Areas.Mpa.Models.Common
{
	public interface IPermissionsEditViewModel
	{
		List<string> GrantedPermissionNames
		{
			get;
			set;
		}

		List<FlatPermissionDto> Permissions
		{
			get;
			set;
		}
	}
}