using Abp;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Extensions;
using FuelWerx;
using FuelWerx.Configuration.Host.Dto;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Configuration.Host
{
	[AbpAuthorize(new string[] { "Pages.Administration.Host.Settings" })]
	public class HostSettingsAppService : FuelWerxAppServiceBase, IHostSettingsAppService, IApplicationService, ITransientDependency
	{
		public HostSettingsAppService()
		{
		}

		public async Task<HostSettingsEditDto> GetAllSettings()
		{
			HostSettingsEditDto hostSettingsEditDto = new HostSettingsEditDto();
			HostSettingsEditDto hostSettingsEditDto1 = hostSettingsEditDto;
			GeneralSettingsEditDto generalSettingsEditDto = new GeneralSettingsEditDto()
			{
				WebSiteRootAddress = await this.SettingManager.GetSettingValueAsync("App.General.WebSiteRootAddress")
			};
			hostSettingsEditDto1.General = generalSettingsEditDto;
			HostSettingsEditDto hostSettingsEditDto2 = hostSettingsEditDto;
			HostUserManagementSettingsEditDto hostUserManagementSettingsEditDto = new HostUserManagementSettingsEditDto();
			HostUserManagementSettingsEditDto hostUserManagementSettingsEditDto1 = hostUserManagementSettingsEditDto;
			bool settingValueAsync = await this.SettingManager.GetSettingValueAsync<bool>("Abp.Zero.UserManagement.IsEmailConfirmationRequiredForLogin");
			hostUserManagementSettingsEditDto1.IsEmailConfirmationRequiredForLogin = settingValueAsync;
			hostSettingsEditDto2.UserManagement = hostUserManagementSettingsEditDto;
			HostSettingsEditDto hostSettingsEditDto3 = hostSettingsEditDto;
			EmailSettingsEditDto emailSettingsEditDto = new EmailSettingsEditDto()
			{
				DefaultFromAddress = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.DefaultFromAddress"),
				DefaultFromDisplayName = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.DefaultFromDisplayName"),
				SmtpHost = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.Host")
			};
			EmailSettingsEditDto emailSettingsEditDto1 = emailSettingsEditDto;
			int num = await this.SettingManager.GetSettingValueAsync<int>("Abp.Net.Mail.Smtp.Port");
			emailSettingsEditDto1.SmtpPort = num;
			emailSettingsEditDto.SmtpUserName = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.UserName");
			emailSettingsEditDto.SmtpPassword = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.Password");
			emailSettingsEditDto.SmtpDomain = await this.SettingManager.GetSettingValueAsync("Abp.Net.Mail.Smtp.Domain");
			EmailSettingsEditDto emailSettingsEditDto2 = emailSettingsEditDto;
			bool flag = await this.SettingManager.GetSettingValueAsync<bool>("Abp.Net.Mail.Smtp.EnableSsl");
			emailSettingsEditDto2.SmtpEnableSsl = flag;
			EmailSettingsEditDto emailSettingsEditDto3 = emailSettingsEditDto;
			bool settingValueAsync1 = await this.SettingManager.GetSettingValueAsync<bool>("Abp.Net.Mail.Smtp.UseDefaultCredentials");
			emailSettingsEditDto3.SmtpUseDefaultCredentials = settingValueAsync1;
			hostSettingsEditDto3.Email = emailSettingsEditDto;
			return hostSettingsEditDto;
		}

		public async Task UpdateAllSettings(HostSettingsEditDto input)
		{
			await this.SettingManager.ChangeSettingForApplicationAsync("App.General.WebSiteRootAddress", input.General.WebSiteRootAddress.EnsureEndsWith('/'));
			ISettingManager settingManager = this.SettingManager;
			bool isEmailConfirmationRequiredForLogin = input.UserManagement.IsEmailConfirmationRequiredForLogin;
			await settingManager.ChangeSettingForApplicationAsync("Abp.Zero.UserManagement.IsEmailConfirmationRequiredForLogin", isEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.DefaultFromAddress", input.Email.DefaultFromAddress);
			await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.DefaultFromDisplayName", input.Email.DefaultFromDisplayName);
			await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Host", input.Email.SmtpHost);
			ISettingManager settingManager1 = this.SettingManager;
			int smtpPort = input.Email.SmtpPort;
			await settingManager1.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Port", smtpPort.ToString(CultureInfo.InvariantCulture));
			await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.UserName", input.Email.SmtpUserName);
			await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Password", input.Email.SmtpPassword);
			await this.SettingManager.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.Domain", input.Email.SmtpDomain);
			ISettingManager settingManager2 = this.SettingManager;
			isEmailConfirmationRequiredForLogin = input.Email.SmtpEnableSsl;
			await settingManager2.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.EnableSsl", isEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
			ISettingManager settingManager3 = this.SettingManager;
			isEmailConfirmationRequiredForLogin = input.Email.SmtpUseDefaultCredentials;
			await settingManager3.ChangeSettingForApplicationAsync("Abp.Net.Mail.Smtp.UseDefaultCredentials", isEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
		}
	}
}