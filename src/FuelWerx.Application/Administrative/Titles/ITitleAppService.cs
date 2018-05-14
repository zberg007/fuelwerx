using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelWerx.Administrative.Titles
{
	public interface ITitleAppService : IApplicationService, ITransientDependency
	{
		Task<long> CreateOrUpdateTitle(CreateOrUpdateTitleInput input);

		Task DeleteTitle(IdInput<long> input);

		Task<GetTitleForEditOutput> GetTitleForEdit(NullableIdInput<long> input);

		Task<PagedResultOutput<TitleListDto>> GetTitles(GetTitlesInput input);

		Task<List<TitleListDto>> GetTitlesForTenant(long tenantId);

		Task<FileDto> GetTitlesToExcel();

		Task SaveTitleImageAsync(UpdateTitleImageInput updateTitleImageInput);
	}
}