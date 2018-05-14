using Abp.Runtime.Validation;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration.Tenants.Dto
{
	public class TenantUserManagementSettingsEditDto : IValidate
	{
		public bool AllowSelfRegistration
		{
			get;
			set;
		}

		public bool EnableEmergencyDeliveryFees
		{
			get;
			set;
		}

		public bool IsEmailConfirmationRequiredForLogin
		{
			get;
			set;
		}

		public bool IsNewRegisteredUserActiveByDefault
		{
			get;
			set;
		}

		public bool RequireOnePhoneNumberForRegistration
		{
			get;
			set;
		}

		public bool SendEmailAfterRegistration
		{
			get;
			set;
		}

		public string SendEmailAfterRegistrationMessage
		{
			get;
			set;
		}

		public bool UseCaptchaOnRegistration
		{
			get;
			set;
		}

		public TenantUserManagementSettingsEditDto()
		{
		}
	}
}