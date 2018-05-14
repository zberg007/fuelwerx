using Abp.Authorization;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization
{
	public static class PermissionManagerExtensions
	{
		public static IEnumerable<Permission> GetPermissionsFromNamesByValidating(this IPermissionManager permissionManager, IEnumerable<string> permissionNames)
		{
			List<Permission> permissions = new List<Permission>();
			List<string> strs = new List<string>();
			foreach (string str in permissionNames)
			{
				Permission permissionOrNull = permissionManager.GetPermissionOrNull(str);
				if (permissionOrNull == null)
				{
					strs.Add(str);
				}
				permissions.Add(permissionOrNull);
			}
			if (strs.Count > 0)
			{
				throw new AbpValidationException(string.Format("There are {0} undefined permission names.", strs.Count))
				{
					ValidationErrors = strs.ConvertAll<ValidationResult>((string permissionName) => new ValidationResult(string.Concat("Undefined permission: ", permissionName)))
				};
			}
			return permissions;
		}
	}
}