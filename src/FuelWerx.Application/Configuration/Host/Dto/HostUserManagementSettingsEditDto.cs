using Abp.Runtime.Validation;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Host.Dto
{
	public class HostUserManagementSettingsEditDto : IValidate
	{
		public bool IsEmailConfirmationRequiredForLogin
		{
			get;
			set;
		}

		public HostUserManagementSettingsEditDto()
		{
		}
	}
}