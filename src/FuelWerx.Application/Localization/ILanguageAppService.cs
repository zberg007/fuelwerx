using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Localization.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Localization
{
	public interface ILanguageAppService : IApplicationService, ITransientDependency
	{
		Task CreateOrUpdateLanguage(CreateOrUpdateLanguageInput input);

		Task DeleteLanguage(IdInput input);

		Task<GetLanguageForEditOutput> GetLanguageForEdit(NullableIdInput input);

		Task<GetLanguagesOutput> GetLanguages();

		Task<PagedResultOutput<LanguageTextListDto>> GetLanguageTexts(GetLanguageTextsInput input);

		Task SetDefaultLanguage(SetDefaultLanguageInput input);

		Task UpdateLanguageText(UpdateLanguageTextInput input);
	}
}