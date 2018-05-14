using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Assets.FillLots.Dto;
using FuelWerx.Dto;
using FuelWerx.FillLots;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Assets.FillLots
{
	public interface IFillLotAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateFillLot(CreateOrUpdateFillLotInput input);

		Task DeleteFillLot(IdInput<long> input);

		Task<FillLot> GetFillLot(long fillLotId);

		Task<GetFillLotForEditOutput> GetFillLotForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<FillLotListDto>> GetFillLots(GetFillLotsInput input);

		Task<FileDto> GetFillLotsToExcel();
	}
}