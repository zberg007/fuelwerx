using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	public class UpdateLanguageTextInput : IInputDto, IDto, IValidate
	{
		[Required]
		[StringLength(256)]
		public string Key
		{
			get;
			set;
		}

		[Required]
		[StringLength(10)]
		public string LanguageName
		{
			get;
			set;
		}

		[Required]
		[StringLength(128)]
		public string SourceName
		{
			get;
			set;
		}

		[Required(AllowEmptyStrings=true)]
		[StringLength(67108864)]
		public string Value
		{
			get;
			set;
		}

		public UpdateLanguageTextInput()
		{
		}
	}
}