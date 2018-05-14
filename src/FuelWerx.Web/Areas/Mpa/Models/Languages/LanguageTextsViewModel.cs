using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Models.Languages
{
	public class LanguageTextsViewModel
	{
		public string BaseLanguageName
		{
			get;
			set;
		}

		public string FilterText
		{
			get;
			set;
		}

		public string LanguageName
		{
			get;
			set;
		}

		public List<LanguageInfo> Languages
		{
			get;
			set;
		}

		public List<SelectListItem> Sources
		{
			get;
			set;
		}

		public string TargetValueFilter
		{
			get;
			set;
		}

		public LanguageTextsViewModel()
		{
		}
	}
}