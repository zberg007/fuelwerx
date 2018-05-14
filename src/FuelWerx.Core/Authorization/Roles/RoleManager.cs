using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using System;

namespace FuelWerx.Authorization.Roles
{
	public class RoleManager : AbpRoleManager<Tenant, Role, User>
	{
		public RoleManager(RoleStore store, IPermissionManager permissionManager, IRoleManagementConfig roleManagementConfig, ICacheManager cacheManager) : base(store, permissionManager, roleManagementConfig, cacheManager)
		{
		}
	}
}