using Abp.Authorization;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using System;

namespace FuelWerx.Authorization
{
	public class PermissionChecker : PermissionChecker<Tenant, Role, User>
	{
		public PermissionChecker(UserManager userManager) : base(userManager)
		{
		}
	}
}