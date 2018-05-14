using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Assets.Trucks.Dto;
using FuelWerx.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Assets.Trucks
{
	public interface ITruckAppService : IApplicationService, ITransientDependency
	{
		Task CreateOrUpdateTruck(CreateOrUpdateTruckInput input);

		Task DeleteTruck(IdInput<long> input);

		Task<bool> DeleteTruckImage(IdInput<long> input);

		Task<GetTruckForEditOutput> GetTruckForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<TruckListDto>> GetTrucks(GetTrucksInput input);

		Task<ListResultDto<TruckListDto>> GetTrucksByTenantId(int tenantId, bool active);

		Task<FileDto> GetTrucksToExcel();

		Task SaveImageAsync(UpdateTruckImageInput input);
	}
}