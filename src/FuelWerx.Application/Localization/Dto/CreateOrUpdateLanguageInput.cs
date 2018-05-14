using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	public class CreateOrUpdateLanguageInput : IInputDto, IDto, IValidate
	{
		[Required]
		public ApplicationLanguageEditDto Language
		{
			get;
			set;
		}

		public CreateOrUpdateLanguageInput()
		{
		}
	}
}