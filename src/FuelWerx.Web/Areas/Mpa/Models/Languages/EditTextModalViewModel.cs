using Abp.Localization;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Web.Areas.Mpa.Models.Languages
{
	public class EditTextModalViewModel
	{
		public LanguageInfo BaseLanguage
		{
			get;
			set;
		}

		public string BaseText
		{
			get;
			set;
		}

		public string Key
		{
			get;
			set;
		}

		public string SourceName
		{
			get;
			set;
		}

		public LanguageInfo TargetLanguage
		{
			get;
			set;
		}

		public string TargetText
		{
			get;
			set;
		}

		public EditTextModalViewModel()
		{
		}
	}
}