using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Models.Account
{
	public class LanguagesViewModel
	{
		public IReadOnlyList<LanguageInfo> AllLanguages
		{
			get;
			set;
		}

		public LanguageInfo CurrentLanguage
		{
			get;
			set;
		}

		public LanguagesViewModel()
		{
		}
	}
}