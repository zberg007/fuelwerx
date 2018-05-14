using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Editions.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Editions
{
	public interface IEditionAppService : IApplicationService, ITransientDependency
	{
		Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input);

		Task DeleteEdition(EntityRequestInput input);

		Task<GetEditionForEditOutput> GetEditionForEdit(NullableIdInput input);

		Task<ListResultOutput<EditionListDto>> GetEditions();
	}
}