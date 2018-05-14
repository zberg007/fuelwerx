using Abp.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FuelWerx.Configuration
{
	public class AppSettingProvider : SettingProvider
	{
		public AppSettingProvider()
		{
		}

		public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
		{
			string[] strArrays = new string[] { "(UTC-10:00) Hawaii", "(UTC-09:00) Alaska", "(UTC-08:00) Pacific Time (US & Canada)", "(UTC-07:00) Arizona", "(UTC-07:00) Mountain Time (US & Canada)", "(UTC-06:00) Central Time (US & Canada)", "(UTC-05:00) Eastern Time (US & Canada)", "(UTC-05:00) Indiana (East)" };
			string str = JsonConvert.SerializeObject(
				from tz in TimeZoneInfo.GetSystemTimeZones()
				where strArrays.Contains<string>(tz.DisplayName)
				select tz);
			SettingDefinition[] settingDefinition = new SettingDefinition[] { new SettingDefinition("App.General.WebSiteRootAddress", "http://localhost:6240/", null, null, null, SettingScopes.Application, false, true, null), new SettingDefinition("App.General.Timezones", str, null, null, null, SettingScopes.Tenant, true, true, null), new SettingDefinition("App.General.TimezoneAppDefaultTimezoneId", "Mountain Standard Time", null, null, null, SettingScopes.Tenant, true, true, null), new SettingDefinition("App.General.DefaultPaymentTerm", "Net 30", null, null, null, SettingScopes.Tenant, true, true, null), null, null, null, null, null, null, null };
			settingDefinition[4] = new SettingDefinition("App.UserManagement.AllowSelfRegistration", ConfigurationManager.AppSettings["App.UserManagement.AllowSelfRegistration"] ?? "true", null, null, null, SettingScopes.Tenant, false, true, null);
			settingDefinition[5] = new SettingDefinition("App.UserManagement.IsNewRegisteredUserActiveByDefault", ConfigurationManager.AppSettings["App.UserManagement.IsNewRegisteredUserActiveByDefault"] ?? "false", null, null, null, SettingScopes.Tenant, false, true, null);
			settingDefinition[6] = new SettingDefinition("App.UserManagement.UseCaptchaOnRegistration", ConfigurationManager.AppSettings["App.UserManagement.UseCaptchaOnRegistration"] ?? "true", null, null, null, SettingScopes.Tenant, false, true, null);
			settingDefinition[7] = new SettingDefinition("App.UserManagement.RequireOnePhoneNumberForRegistration", ConfigurationManager.AppSettings["App.UserManagement.RequireOnePhoneNumberForRegistration"] ?? "false", null, null, null, SettingScopes.Tenant, false, true, null);
			settingDefinition[8] = new SettingDefinition("App.UserManagement.SendEmailAfterRegistration", ConfigurationManager.AppSettings["App.UserManagement.SendEmailAfterRegistration"] ?? "false", null, null, null, SettingScopes.Tenant, false, true, null);
			settingDefinition[9] = new SettingDefinition("App.UserManagement.SendEmailAfterRegistrationMessage", ConfigurationManager.AppSettings["App.UserManagement.SendEmailAfterRegistrationMessage"] ?? "", null, null, null, SettingScopes.Tenant, false, true, null);
			settingDefinition[10] = new SettingDefinition("App.UserManagment.EnableEmergencyDeliveryFees", ConfigurationManager.AppSettings["App.UserManagment.EnableEmergencyDeliveryFees"] ?? "false", null, null, null, SettingScopes.Tenant, false, true, null);
			return settingDefinition;
		}
	}
}