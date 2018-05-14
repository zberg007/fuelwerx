using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Editions;
using System;

namespace FuelWerx.MultiTenancy
{
	public class TenantManager : AbpTenantManager<Tenant, Role, User>
	{
		public TenantManager(IRepository<Tenant> tenantRepository, IRepository<TenantFeatureSetting, long> tenantFeatureRepository, FuelWerx.Editions.EditionManager editionManager) : base(tenantRepository, tenantFeatureRepository, editionManager)
		{
		}
	}
}