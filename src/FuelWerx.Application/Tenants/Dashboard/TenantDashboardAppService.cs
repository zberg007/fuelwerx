using Abp;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Dependency;
using FuelWerx;
using FuelWerx.Tenants.Dashboard.Dto;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FuelWerx.Tenants.Dashboard
{
	[AbpAuthorize(new string[] { "Pages.Tenant.Dashboard" })]
	public class TenantDashboardAppService : FuelWerxAppServiceBase, ITenantDashboardAppService, IApplicationService, ITransientDependency
	{
		public TenantDashboardAppService()
		{
		}

		public GetMemberActivityOutput GetMemberActivity()
		{
			return new GetMemberActivityOutput()
			{
				TotalMembers = (
					from i in Enumerable.Range(0, 13)
					select RandomHelper.GetRandom(15, 40)).ToList<int>(),
				NewMembers = (
					from i in Enumerable.Range(0, 13)
					select RandomHelper.GetRandom(3, 15)).ToList<int>()
			};
		}
	}
}