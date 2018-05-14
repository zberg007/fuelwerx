using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Localization;
using FuelWerx.Localization.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Languages;
using FuelWerx.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Languages" })]
	public class LanguagesController : FuelWerxControllerBase
	{
		private readonly ILanguageAppService _languageAppService;

		private readonly ILanguageManager _languageManager;

		private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;

		public LanguagesController(ILanguageAppService languageAppService, ILanguageManager languageManager, IApplicationLanguageTextManager applicationLanguageTextManager)
		{
			this._languageAppService = languageAppService;
			this._languageManager = languageManager;
			this._applicationLanguageTextManager = applicationLanguageTextManager;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Languages.Create", "Pages.Administration.Languages.Edit" })]
		public async Task<PartialViewResult> CreateOrEditModal(int? id)
		{
			ILanguageAppService languageAppService = this._languageAppService;
			NullableIdInput nullableIdInput = new NullableIdInput()
			{
				Id = id
			};
			GetLanguageForEditOutput languageForEdit = await languageAppService.GetLanguageForEdit(nullableIdInput);
			return this.PartialView("_CreateOrEditModal", new CreateOrEditLanguageModalViewModel(languageForEdit));
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Languages.ChangeTexts" })]
		public PartialViewResult EditTextModal(string sourceName, string baseLanguageName, string languageName, string key)
		{
			IReadOnlyList<LanguageInfo> languages = this._languageManager.GetLanguages();
			LanguageInfo languageInfo = languages.FirstOrDefault<LanguageInfo>((LanguageInfo l) => l.Name == baseLanguageName);
			if (languageInfo == null)
			{
				throw new ApplicationException(string.Concat("Could not find language: ", baseLanguageName));
			}
			LanguageInfo languageInfo1 = languages.FirstOrDefault<LanguageInfo>((LanguageInfo l) => l.Name == languageName);
			if (languageInfo1 == null)
			{
				throw new ApplicationException(string.Concat("Could not find language: ", languageName));
			}
			string stringOrNull = this._applicationLanguageTextManager.GetStringOrNull(base.AbpSession.TenantId, sourceName, CultureInfo.GetCultureInfo(baseLanguageName), key, true);
			string str = this._applicationLanguageTextManager.GetStringOrNull(base.AbpSession.TenantId, sourceName, CultureInfo.GetCultureInfo(languageName), key, false);
			return this.PartialView("_EditTextModal", new EditTextModalViewModel()
			{
				SourceName = sourceName,
				BaseLanguage = languageInfo,
				TargetLanguage = languageInfo1,
				BaseText = stringOrNull,
				TargetText = str,
				Key = key
			});
		}

		public ActionResult Index()
		{
			return base.View(new LanguagesIndexViewModel()
			{
				IsTenantView = base.AbpSession.TenantId.HasValue
			});
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Languages.ChangeTexts" })]
		public ActionResult Texts(string languageName, string sourceName = "", string baseLanguageName = "", string targetValueFilter = "ALL", string filterText = "")
		{
			string str = sourceName;
			if (str.IsNullOrEmpty())
			{
				str = "FuelWerx";
			}
			if (baseLanguageName.IsNullOrEmpty())
			{
				baseLanguageName = base.LocalizationManager.CurrentLanguage.Name;
			}
			LanguageTextsViewModel languageTextsViewModel = new LanguageTextsViewModel()
			{
				LanguageName = languageName,
				Languages = base.LocalizationManager.GetAllLanguages().ToList<LanguageInfo>(),
				Sources = base.LocalizationManager.GetAllSources().Where<ILocalizationSource>((ILocalizationSource s) => {
					if (s.GetType() != typeof(MultiTenantLocalizationSource))
					{
						return false;
					}
					return s.Name == "FuelWerx";
				}).Select<ILocalizationSource, SelectListItem>((ILocalizationSource s) => new SelectListItem()
				{
					Value = s.Name,
					Text = s.Name,
					Selected = s.Name == str
				}).ToList<SelectListItem>(),
				BaseLanguageName = baseLanguageName,
				TargetValueFilter = targetValueFilter,
				FilterText = filterText
			};
			return base.View(languageTextsViewModel);
		}
	}
}