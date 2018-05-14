using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	public class GetLanguageForEditOutput : IOutputDto, IDto
	{
		public List<ComboboxItemDto> Flags
		{
			get;
			set;
		}

		public ApplicationLanguageEditDto Language
		{
			get;
			set;
		}

		public List<ComboboxItemDto> LanguageNames
		{
			get;
			set;
		}

		public GetLanguageForEditOutput()
		{
			this.LanguageNames = new List<ComboboxItemDto>();
			this.Flags = new List<ComboboxItemDto>();
		}
	}
}