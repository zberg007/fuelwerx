using Abp.Application.Features;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using System;

namespace FuelWerx.Editions
{
	public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
	{
		public FeatureValueStore(TenantManager tenantManager) : base(tenantManager)
		{
		}
	}
}