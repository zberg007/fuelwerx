using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Localization.Dto
{
	public class SetDefaultLanguageInput : IInputDto, IDto, IValidate
	{
		[Required]
		[StringLength(10)]
		public virtual string Name
		{
			get;
			set;
		}

		public SetDefaultLanguageInput()
		{
		}
	}
}