using Abp.Application.Services.Dto;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	public class LanguageTextListDto : IDto
	{
		public string BaseValue
		{
			get;
			set;
		}

		public string Key
		{
			get;
			set;
		}

		public string TargetValue
		{
			get;
			set;
		}

		public LanguageTextListDto()
		{
		}
	}
}