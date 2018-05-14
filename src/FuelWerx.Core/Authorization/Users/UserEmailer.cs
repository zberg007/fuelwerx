using Abp;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using FuelWerx;
using FuelWerx.Emailing;
using FuelWerx.MultiTenancy;
using FuelWerx.Security;
using FuelWerx.Web;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FuelWerx.Authorization.Users
{
	public class UserEmailer : FuelWerxServiceBase, IUserEmailer, ITransientDependency
	{
		private readonly IEmailTemplateProvider _emailTemplateProvider;

		private readonly IEmailSender _emailSender;

		private readonly IWebUrlService _webUrlService;

		private readonly IRepository<Tenant> _tenantRepository;

		public UserEmailer(IEmailTemplateProvider emailTemplateProvider, IEmailSender emailSender, IWebUrlService webUrlService, IRepository<Tenant> tenantRepository)
		{
			this._emailTemplateProvider = emailTemplateProvider;
			this._emailSender = emailSender;
			this._webUrlService = webUrlService;
			this._tenantRepository = tenantRepository;
		}

		public async Task SendEmailActivationLinkAsync(User user, string plainPassword = null)
		{
			string tenancyName;
			if (user.EmailConfirmationCode.IsNullOrEmpty())
			{
				throw new ApplicationException("EmailConfirmationCode should be set in order to send email activation link.");
			}
			if (user.TenantId.HasValue)
			{
				IRepository<Tenant> repository = this._tenantRepository;
				tenancyName = repository.Get(user.TenantId.Value).TenancyName;
			}
			else
			{
				tenancyName = null;
			}
			string str = tenancyName;
			string[] siteRootAddress = new string[] { this._webUrlService.GetSiteRootAddress(str), "Account/EmailConfirmation?userId=", null, null, null };
			long id = user.Id;
			siteRootAddress[2] = Uri.EscapeDataString(SimpleStringCipher.Encrypt(id.ToString(), "gsKnGZ041HLL4IM8"));
			siteRootAddress[3] = "&confirmationCode=";
			siteRootAddress[4] = Uri.EscapeDataString(user.EmailConfirmationCode);
			string str1 = string.Concat(siteRootAddress);
			StringBuilder stringBuilder = new StringBuilder(this._emailTemplateProvider.GetDefaultTemplate());
			int year = DateTime.Now.Year;
			stringBuilder.Replace("{CURRENT_YEAR}", year.ToString());
			stringBuilder.Replace("{EMAIL_TITLE}", this.L("EmailActivation_Title"));
			stringBuilder.Replace("{EMAIL_SUB_TITLE}", this.L("EmailActivation_SubTitle"));
			StringBuilder stringBuilder1 = new StringBuilder();
			string[] strArrays = new string[] { "<b>", this.L("NameSurname"), "</b>: ", user.Name, " ", user.Surname, "<br />" };
			stringBuilder1.AppendLine(string.Concat(strArrays));
			if (!str.IsNullOrEmpty())
			{
				string[] strArrays1 = new string[] { "<b>", this.L("TenancyName"), "</b>: ", str, "<br />" };
				stringBuilder1.AppendLine(string.Concat(strArrays1));
			}
			string[] strArrays2 = new string[] { "<b>", this.L("UserName"), "</b>: ", user.UserName, "<br />" };
			stringBuilder1.AppendLine(string.Concat(strArrays2));
			if (!plainPassword.IsNullOrEmpty())
			{
				string[] strArrays3 = new string[] { "<b>", this.L("Password"), "</b>: ", plainPassword, "<br />" };
				stringBuilder1.AppendLine(string.Concat(strArrays3));
			}
			stringBuilder1.AppendLine("<br />");
			stringBuilder1.AppendLine(string.Concat(this.L("EmailActivation_ClickTheLinkBelowToVerifyYourEmail"), "<br /><br />"));
			string[] strArrays4 = new string[] { "<a href=\"", str1, "\">", str1, "</a>" };
			stringBuilder1.AppendLine(string.Concat(strArrays4));
			stringBuilder.Replace("{EMAIL_BODY}", stringBuilder1.ToString());
			await this._emailSender.SendAsync(user.EmailAddress, this.L("EmailActivation_Subject"), stringBuilder.ToString(), true);
		}

		public async Task SendPasswordResetLinkAsync(User user)
		{
			string tenancyName;
			if (user.PasswordResetCode.IsNullOrEmpty())
			{
				throw new ApplicationException("PasswordResetCode should be set in order to send password reset link.");
			}
			if (user.TenantId.HasValue)
			{
				IRepository<Tenant> repository = this._tenantRepository;
				tenancyName = repository.Get(user.TenantId.Value).TenancyName;
			}
			else
			{
				tenancyName = null;
			}
			string str = tenancyName;
			string[] siteRootAddress = new string[] { this._webUrlService.GetSiteRootAddress(str), "Account/ResetPassword?userId=", null, null, null };
			long id = user.Id;
			siteRootAddress[2] = Uri.EscapeDataString(SimpleStringCipher.Encrypt(id.ToString(), "gsKnGZ041HLL4IM8"));
			siteRootAddress[3] = "&resetCode=";
			siteRootAddress[4] = Uri.EscapeDataString(user.PasswordResetCode);
			string str1 = string.Concat(siteRootAddress);
			StringBuilder stringBuilder = new StringBuilder(this._emailTemplateProvider.GetDefaultTemplate());
			int year = DateTime.Now.Year;
			stringBuilder.Replace("{CURRENT_YEAR}", year.ToString());
			stringBuilder.Replace("{EMAIL_TITLE}", this.L("PasswordResetEmail_Title"));
			stringBuilder.Replace("{EMAIL_SUB_TITLE}", this.L("PasswordResetEmail_SubTitle"));
			StringBuilder stringBuilder1 = new StringBuilder();
			string[] strArrays = new string[] { "<b>", this.L("NameSurname"), "</b>: ", user.Name, " ", user.Surname, "<br />" };
			stringBuilder1.AppendLine(string.Concat(strArrays));
			if (!str.IsNullOrEmpty())
			{
				string[] strArrays1 = new string[] { "<b>", this.L("TenancyName"), "</b>: ", str, "<br />" };
				stringBuilder1.AppendLine(string.Concat(strArrays1));
			}
			string[] strArrays2 = new string[] { "<b>", this.L("UserName"), "</b>: ", user.UserName, "<br />" };
			stringBuilder1.AppendLine(string.Concat(strArrays2));
			stringBuilder1.AppendLine("<br />");
			stringBuilder1.AppendLine(string.Concat(this.L("PasswordResetEmail_ClickTheLinkBelowToResetYourPassword"), "<br /><br />"));
			string[] strArrays3 = new string[] { "<a href=\"", str1, "\">", str1, "</a>" };
			stringBuilder1.AppendLine(string.Concat(strArrays3));
			stringBuilder.Replace("{EMAIL_BODY}", stringBuilder1.ToString());
			await this._emailSender.SendAsync(user.EmailAddress, this.L("PasswordResetEmail_Subject"), stringBuilder.ToString(), true);
		}
	}
}