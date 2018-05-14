using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Dto;
using FuelWerx.Estimates.Dto;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Estimates
{
	public interface IEstimateAppService : IApplicationService, ITransientDependency
	{
		Task AcceptAsProject(IdInput<long> input);

		Task CopyEstimate(CopyEstimateInput input);

		Task<long> CreateOrUpdateEstimate(CreateOrUpdateEstimateInput input);

		Task DeleteEstimate(IdInput<long> input);

		Task DeleteEstimateResource(IdInput<long> input);

		Task<Estimate> GetEstimate(long estimateId);

		Task<GetEstimateForCopyOutput> GetEstimateForCopy(IdInput<long> input);

		Task<GetEstimateForEditOutput> GetEstimateForEdit(NullableIdInput<long> input);

		Task<EstimateResourceEditDto> GetEstimateResourceDetailsByBinaryObjectId(Guid resourceId);

		Task<GetEstimateResourceForEditOutput> GetEstimateResourcesForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<EstimateListDto>> GetEstimates(GetEstimatesInput input);

		Task<FileDto> GetEstimatesToExcel();

		Task SaveEstimateResourceAsync(UpdateEstimateResourceInput updateEstimateResourceInput);

		Task SaveEstimateResourceDetails(long estimateResourceId, string name, string description, bool isActive);
	}
}