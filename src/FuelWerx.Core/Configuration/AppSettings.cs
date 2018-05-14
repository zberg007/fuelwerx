using System;

namespace FuelWerx.Configuration
{
	public static class AppSettings
	{
		public static class General
		{
			public const string WebSiteRootAddress = "App.General.WebSiteRootAddress";

			public const string Timezones = "App.General.Timezones";

			public const string TimezoneAppDefaultTimezoneId = "App.General.TimezoneAppDefaultTimezoneId";

			public const string DefaultPaymentTerm = "App.General.DefaultPaymentTerm";
		}

		public static class UserManagement
		{
			public const string AllowSelfRegistration = "App.UserManagement.AllowSelfRegistration";

			public const string IsNewRegisteredUserActiveByDefault = "App.UserManagement.IsNewRegisteredUserActiveByDefault";

			public const string UseCaptchaOnRegistration = "App.UserManagement.UseCaptchaOnRegistration";

			public const string RequireOnePhoneNumberForRegistration = "App.UserManagement.RequireOnePhoneNumberForRegistration";

			public const string SendEmailAfterRegistration = "App.UserManagement.SendEmailAfterRegistration";

			public const string SendEmailAfterRegistrationMessage = "App.UserManagement.SendEmailAfterRegistrationMessage";

			public const string EnableEmergencyDeliveryFees = "App.UserManagment.EnableEmergencyDeliveryFees";
		}
	}
}