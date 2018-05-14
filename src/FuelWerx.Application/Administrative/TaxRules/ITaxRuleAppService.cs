using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Administrative.TaxRules.Dto;
using FuelWerx.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.TaxRules
{
	public interface ITaxRuleAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateTaxRule(CreateOrUpdateTaxRuleInput input);

		Task<long> CreateOrUpdateTaxRuleRule(CreateOrUpdateTaxRuleRuleInput input);

		Task<bool> DeleteTaxRule(IdInput<long> input);

		Task DeleteTaxRuleRule(IdInput<long> input);

		Task<GetTaxRuleForEditOutput> GetTaxRuleForEdit(NullableIdInput<long> input);

		Task<GetTaxRuleRuleForEditOutput> GetTaxRuleRuleForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<TaxRuleRuleListDto>> GetTaxRuleRules(GetTaxRuleRulesInput input);

		Task<PagedResultOutput<TaxRuleListDto>> GetTaxRules(GetTaxRulesInput input);

		Task<ListResultDto<TaxRuleListDto>> GetTaxRulesByTenantId(int tenantId, bool active);

		Task<FileDto> GetTaxRulesToExcel();
	}
}