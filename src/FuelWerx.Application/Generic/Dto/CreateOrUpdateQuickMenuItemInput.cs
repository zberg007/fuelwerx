using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Generic.Dto
{
	public class CreateOrUpdateQuickMenuItemInput : IInputDto, IDto, IValidate
	{
		[Required]
		public QuickMenuItemEditDto QuickMenuItem
		{
			get;
			set;
		}

		public CreateOrUpdateQuickMenuItemInput()
		{
		}
	}
}