using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	public class GetLanguagesOutput : ListResultOutput<ApplicationLanguageListDto>
	{
		public string DefaultLanguageName
		{
			get;
			set;
		}

		public GetLanguagesOutput()
		{
		}

		public GetLanguagesOutput(IReadOnlyList<ApplicationLanguageListDto> items, string defaultLanguageName) : base(items)
		{
			this.DefaultLanguageName = defaultLanguageName;
		}
	}
}