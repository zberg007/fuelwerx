using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Runtime.Session;
using Abp.UI;
using Castle.Core.Logging;
using FuelWerx;
using FuelWerx.Localization.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Localization
{
	[AbpAuthorize(new string[] { "Pages.Administration.Languages" })]
	public class LanguageAppService : FuelWerxAppServiceBase, ILanguageAppService, IApplicationService, ITransientDependency
	{
		private readonly IApplicationLanguageManager _applicationLanguageManager;

		private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;

		private readonly IRepository<ApplicationLanguage> _languageRepository;

		public LanguageAppService(IApplicationLanguageManager applicationLanguageManager, IApplicationLanguageTextManager applicationLanguageTextManager, IRepository<ApplicationLanguage> languageRepository)
		{
			this._applicationLanguageManager = applicationLanguageManager;
			this._languageRepository = languageRepository;
			this._applicationLanguageTextManager = applicationLanguageTextManager;
		}

		private async Task CheckLanguageIfAlreadyExists(string languageName, int? expectedId = null)
		{
			IReadOnlyList<ApplicationLanguage> languagesAsync = await this._applicationLanguageManager.GetLanguagesAsync(this.AbpSession.TenantId);
			ApplicationLanguage applicationLanguage = languagesAsync.FirstOrDefault<ApplicationLanguage>((ApplicationLanguage l) => l.Name == languageName);
			if (applicationLanguage != null)
			{
				if (!expectedId.HasValue || applicationLanguage.Id != expectedId.Value)
				{
					throw new UserFriendlyException(this.L("ThisLanguageAlreadyExists"));
				}
			}
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Languages.Create" })]
		protected virtual async Task CreateLanguageAsync(CreateOrUpdateLanguageInput input)
		{
			CultureInfo cultureInfoByChecking = this.GetCultureInfoByChecking(input.Language.Name);
			await this.CheckLanguageIfAlreadyExists(cultureInfoByChecking.Name, null);
			await this._applicationLanguageManager.AddAsync(new ApplicationLanguage(this.AbpSession.TenantId, cultureInfoByChecking.Name, cultureInfoByChecking.DisplayName, input.Language.Icon));
		}

		public async Task CreateOrUpdateLanguage(CreateOrUpdateLanguageInput input)
		{
			if (!input.Language.Id.HasValue)
			{
				await this.CreateLanguageAsync(input);
			}
			else
			{
				await this.UpdateLanguageAsync(input);
			}
		}

		public async Task DeleteLanguage(IdInput input)
		{
			ApplicationLanguage async = await this._languageRepository.GetAsync(input.Id);
			await this._applicationLanguageManager.RemoveAsync(this.AbpSession.TenantId, async.Name);
		}

		private CultureInfo GetCultureInfoByChecking(string name)
		{
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = CultureInfo.GetCultureInfo(name);
			}
			catch (CultureNotFoundException cultureNotFoundException1)
			{
				CultureNotFoundException cultureNotFoundException = cultureNotFoundException1;
				base.Logger.Warn(cultureNotFoundException.ToString(), cultureNotFoundException);
				throw new UserFriendlyException(this.L("InvlalidLanguageCode"));
			}
			return cultureInfo;
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Languages.Create", "Pages.Administration.Languages.Edit" })]
		public async Task<GetLanguageForEditOutput> GetLanguageForEdit(NullableIdInput input)
		{
			ApplicationLanguageEditDto applicationLanguageEditDto;
			ApplicationLanguage async = null;
			if (input.Id.HasValue)
			{
				IRepository<ApplicationLanguage> repository = this._languageRepository;
				async = await repository.GetAsync(input.Id.Value);
			}
			GetLanguageForEditOutput getLanguageForEditOutput = new GetLanguageForEditOutput();
			GetLanguageForEditOutput getLanguageForEditOutput1 = getLanguageForEditOutput;
			applicationLanguageEditDto = (async != null ? async.MapTo<ApplicationLanguageEditDto>() : new ApplicationLanguageEditDto());
			getLanguageForEditOutput1.Language = applicationLanguageEditDto;
			GetLanguageForEditOutput list = getLanguageForEditOutput;
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
			list.LanguageNames = (
				from c in (IEnumerable<CultureInfo>)cultures
				orderby c.DisplayName
				select new ComboboxItemDto(c.Name, string.Concat(c.DisplayName, " (", c.Name, ")"))
				{
					IsSelected = getLanguageForEditOutput.Language.Name == c.Name
				}).ToList<ComboboxItemDto>();
			GetLanguageForEditOutput list1 = getLanguageForEditOutput;
			List<string> flagClassNames = FamFamFamFlagsHelper.FlagClassNames;
			list1.Flags = (
				from f in flagClassNames
				orderby f
				select new ComboboxItemDto(f, FamFamFamFlagsHelper.GetCountryCode(f))
				{
					IsSelected = getLanguageForEditOutput.Language.Icon == f
				}).ToList<ComboboxItemDto>();
			return getLanguageForEditOutput;
		}

		public async Task<GetLanguagesOutput> GetLanguages()
		{
			string name;
			IReadOnlyList<ApplicationLanguage> languagesAsync = await this._applicationLanguageManager.GetLanguagesAsync(this.AbpSession.TenantId);
			IEnumerable<ApplicationLanguage> applicationLanguages = languagesAsync;
			IOrderedEnumerable<ApplicationLanguage> displayName = 
				from l in applicationLanguages
				orderby l.DisplayName
				select l;
			ApplicationLanguage defaultLanguageOrNullAsync = await this._applicationLanguageManager.GetDefaultLanguageOrNullAsync(this.AbpSession.TenantId);
			ApplicationLanguage applicationLanguage = defaultLanguageOrNullAsync;
			List<ApplicationLanguageListDto> applicationLanguageListDtos = displayName.MapTo<List<ApplicationLanguageListDto>>();
			if (applicationLanguage == null)
			{
				name = null;
			}
			else
			{
				name = applicationLanguage.Name;
			}
			return new GetLanguagesOutput(applicationLanguageListDtos, name);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Languages.ChangeTexts" })]
		public async Task<PagedResultOutput<LanguageTextListDto>> GetLanguageTexts(GetLanguageTextsInput input)
		{
			if (input.BaseLanguageName.IsNullOrEmpty())
			{
				ApplicationLanguage defaultLanguageOrNullAsync = await this._applicationLanguageManager.GetDefaultLanguageOrNullAsync(this.AbpSession.TenantId);
				ApplicationLanguage applicationLanguage = defaultLanguageOrNullAsync;
				if (applicationLanguage == null)
				{
					IReadOnlyList<ApplicationLanguage> languagesAsync = await this._applicationLanguageManager.GetLanguagesAsync(this.AbpSession.TenantId);
					applicationLanguage = languagesAsync.FirstOrDefault<ApplicationLanguage>();
					if (applicationLanguage == null)
					{
						throw new ApplicationException("No language found in the application!");
					}
				}
				input.BaseLanguageName = applicationLanguage.Name;
			}
			ILocalizationSource source = this.LocalizationManager.GetSource(input.SourceName);
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(input.BaseLanguageName);
			CultureInfo cultureInfo1 = CultureInfo.GetCultureInfo(input.TargetLanguageName);
			IQueryable<LanguageTextListDto> key = (
				from localizedString in source.GetAllStrings(true)
				select new LanguageTextListDto()
				{
					Key = localizedString.Name,
					BaseValue = this._applicationLanguageTextManager.GetStringOrNull(this.AbpSession.TenantId, source.Name, cultureInfo, localizedString.Name, true),
					TargetValue = this._applicationLanguageTextManager.GetStringOrNull(this.AbpSession.TenantId, source.Name, cultureInfo1, localizedString.Name, false)
				}).AsQueryable<LanguageTextListDto>();
			if (input.TargetValueFilter == "EMPTY")
			{
				key = 
					from s in key
					where s.TargetValue.IsNullOrEmpty()
					select s;
			}
			if (!input.FilterText.IsNullOrEmpty())
			{
				key = 
					from l in key
					where l.Key != null && l.Key.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0 || l.BaseValue != null && l.BaseValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0 || l.TargetValue != null && l.TargetValue.IndexOf(input.FilterText, StringComparison.CurrentCultureIgnoreCase) >= 0
					select l;
			}
			int num = key.Count<LanguageTextListDto>();
			if (!input.Sorting.IsNullOrEmpty())
			{
				key = key.OrderBy<LanguageTextListDto>(input.Sorting, new object[0]);
			}
			if (input.SkipCount > 0)
			{
				key = key.Skip<LanguageTextListDto>(input.SkipCount);
			}
			if (input.MaxResultCount > 0)
			{
				key = key.Take<LanguageTextListDto>(input.MaxResultCount);
			}
			return new PagedResultOutput<LanguageTextListDto>(num, key.ToList<LanguageTextListDto>());
		}

		public async Task SetDefaultLanguage(SetDefaultLanguageInput input)
		{
			await this._applicationLanguageManager.SetDefaultLanguageAsync(this.AbpSession.TenantId, this.GetCultureInfoByChecking(input.Name).Name);
		}

		[AbpAuthorize(new string[] { "Pages.Administration.Languages.Edit" })]
		protected virtual async Task UpdateLanguageAsync(CreateOrUpdateLanguageInput input)
		{
			CultureInfo cultureInfoByChecking = this.GetCultureInfoByChecking(input.Language.Name);
			LanguageAppService languageAppService = this;
			string name = cultureInfoByChecking.Name;
			int? id = input.Language.Id;
			await languageAppService.CheckLanguageIfAlreadyExists(name, new int?(id.Value));
			IRepository<ApplicationLanguage> repository = this._languageRepository;
			id = input.Language.Id;
			ApplicationLanguage async = await repository.GetAsync(id.Value);
			async.Name = cultureInfoByChecking.Name;
			async.DisplayName = cultureInfoByChecking.DisplayName;
			async.Icon = input.Language.Icon;
			await this._applicationLanguageManager.UpdateAsync(this.AbpSession.TenantId, async);
		}

		public async Task UpdateLanguageText(UpdateLanguageTextInput input)
		{
			CultureInfo cultureInfoByChecking = this.GetCultureInfoByChecking(input.LanguageName);
			ILocalizationSource source = this.LocalizationManager.GetSource(input.SourceName);
			await this._applicationLanguageTextManager.UpdateStringAsync(this.AbpSession.TenantId, source.Name, cultureInfoByChecking, input.Key, input.Value);
		}
	}
}