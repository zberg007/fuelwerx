using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Profile.Dto
{
	public class ChangePasswordInput : IInputDto, IDto, IValidate
	{
		[Required]
		[StringLength(32)]
		public string CurrentPassword
		{
			get;
			set;
		}

		[Required]
		[StringLength(32, MinimumLength=6)]
		public string NewPassword
		{
			get;
			set;
		}

		public ChangePasswordInput()
		{
		}
	}
}