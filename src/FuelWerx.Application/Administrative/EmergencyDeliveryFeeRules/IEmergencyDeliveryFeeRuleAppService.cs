using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Administrative.EmergencyDeliveryFeeRules.Dto;
using FuelWerx.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.EmergencyDeliveryFeeRules
{
	public interface IEmergencyDeliveryFeeRuleAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateEmergencyDeliveryFeeRule(CreateOrUpdateEmergencyDeliveryFeeRuleInput input);

		Task DeleteEmergencyDeliveryFeeRule(IdInput<long> input);

		Task<GetEmergencyDeliveryFeeRuleForEditOutput> GetEmergencyDeliveryFeeRuleForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<EmergencyDeliveryFeeRuleListDto>> GetEmergencyDeliveryFeeRules(GetEmergencyDeliveryFeeRulesInput input);

		Task<FileDto> GetEmergencyDeliveryFeeRulesToExcel();
	}
}