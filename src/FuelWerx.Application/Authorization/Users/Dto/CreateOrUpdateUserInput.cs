using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Users.Dto
{
	public class CreateOrUpdateUserInput : IInputDto, IDto, IValidate
	{
		[Required]
		public string[] AssignedRoleNames
		{
			get;
			set;
		}

		public int? ConvertToUserCustomerId
		{
			get;
			set;
		}

		public bool SendActivationEmail
		{
			get;
			set;
		}

		[Required]
		public UserEditDto User
		{
			get;
			set;
		}

		public UserSettingDataEditDto UserSettingData
		{
			get;
			set;
		}

		public CreateOrUpdateUserInput()
		{
		}
	}
}