using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Administrative.EmergencyDeliveryFees.Dto;
using FuelWerx.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.EmergencyDeliveryFees
{
	public interface IEmergencyDeliveryFeeAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateEmergencyDeliveryFee(CreateOrUpdateEmergencyDeliveryFeeInput input);

		Task DeleteEmergencyDeliveryFee(IdInput<long> input);

		Task<GetEmergencyDeliveryFeeForEditOutput> GetEmergencyDeliveryFeeForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<EmergencyDeliveryFeeListDto>> GetEmergencyDeliveryFees(GetEmergencyDeliveryFeesInput input);

		Task<FileDto> GetEmergencyDeliveryFeesToExcel();
	}
}