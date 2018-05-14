using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using System;

namespace FuelWerx.Authorization.Roles
{
	public class RoleStore : AbpRoleStore<Tenant, Role, User>
	{
		public RoleStore(IRepository<Role> roleRepository, IRepository<UserRole, long> userRoleRepository, IRepository<RolePermissionSetting, long> rolePermissionSettingRepository) : base(roleRepository, userRoleRepository, rolePermissionSettingRepository)
		{
		}
	}
}