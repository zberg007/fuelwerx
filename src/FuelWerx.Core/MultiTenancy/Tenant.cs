using Abp.MultiTenancy;
using FuelWerx.Authorization.Users;
using System;

namespace FuelWerx.MultiTenancy
{
	public class Tenant : AbpTenant<Tenant, User>
	{
		protected Tenant()
		{
		}

		public Tenant(string tenancyName, string name) : base(tenancyName, name)
		{
		}
	}
}