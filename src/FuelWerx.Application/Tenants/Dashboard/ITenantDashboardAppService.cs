using Abp.Application.Services;
using Abp.Dependency;
using FuelWerx.Tenants.Dashboard.Dto;

namespace FuelWerx.Tenants.Dashboard
{
	public interface ITenantDashboardAppService : IApplicationService, ITransientDependency
	{
		GetMemberActivityOutput GetMemberActivity();
	}
}