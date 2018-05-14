using Abp.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using System;

namespace FuelWerx.Authorization.Roles
{
	public class Role : AbpRole<FuelWerx.MultiTenancy.Tenant, User>
	{
		public Role()
		{
		}

		public Role(int? tenantId, string displayName) : base(tenantId, displayName)
		{
		}

		public Role(int? tenantId, string name, string displayName) : base(tenantId, name, displayName)
		{
		}
	}
}