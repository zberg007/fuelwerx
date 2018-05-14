using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.MultiTenancy.Dto;
using System.Threading.Tasks;

namespace FuelWerx.MultiTenancy
{
	public interface ITenantAppService : IApplicationService, ITransientDependency
	{
		Task CreateTenant(CreateTenantInput input);

		Task DeleteTenant(EntityRequestInput input);

		Task<GetTenantFeaturesForEditOutput> GetTenantFeaturesForEdit(EntityRequestInput input);

		Task<TenantEditDto> GetTenantForEdit(EntityRequestInput input);

		Task<PagedResultOutput<TenantListDto>> GetTenants(GetTenantsInput input);

		Task ResetTenantSpecificFeatures(EntityRequestInput input);

		Task UpdateTenant(TenantEditDto input);

		Task UpdateTenantFeatures(UpdateTenantFeaturesInput input);
	}
}