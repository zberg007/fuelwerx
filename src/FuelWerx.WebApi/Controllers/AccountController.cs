using Abp.Authorization.Users;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Castle.Core.Logging;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using FuelWerx.WebApi;
using FuelWerx.WebApi.Models;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FuelWerx.WebApi.Controllers
{
	public class AccountController : FuelWerxApiControllerBase
	{
		private readonly UserManager _userManager;

		public static OAuthBearerAuthenticationOptions OAuthBearerOptions
		{
			get;
			private set;
		}

		static AccountController()
		{
			AccountController.OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
		}

		public AccountController(UserManager userManager)
		{
			this._userManager = userManager;
		}

		[HttpPost]
		public async Task<AjaxResponse> Authenticate(LoginModel loginModel)
		{
			this.CheckModelState();
			AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult loginResultAsync = await this.GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, loginModel.TenancyName);
			AuthenticationTicket authenticationTicket = new AuthenticationTicket(loginResultAsync.Identity, new AuthenticationProperties());
			DateTimeOffset utcNow = (new SystemClock()).UtcNow;
			authenticationTicket.Properties.IssuedUtc = new DateTimeOffset?(utcNow);
			authenticationTicket.Properties.ExpiresUtc = new DateTimeOffset?(utcNow.Add(TimeSpan.FromMinutes(30)));
			return new AjaxResponse(AccountController.OAuthBearerOptions.AccessTokenFormat.Protect(authenticationTicket));
		}

		protected virtual void CheckModelState()
		{
			if (!base.ModelState.IsValid)
			{
				throw new UserFriendlyException("Invalid request!");
			}
		}

		private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
		{
			switch (result)
			{
				case AbpLoginResultType.Success:
				{
					return new ApplicationException("Don't call this method with a success result!");
				}
				case AbpLoginResultType.InvalidUserNameOrEmailAddress:
				case AbpLoginResultType.InvalidPassword:
				{
					return new UserFriendlyException(this.L("LoginFailed"), this.L("InvalidUserNameOrPassword"));
				}
				case AbpLoginResultType.UserIsNotActive:
				{
					return new UserFriendlyException(this.L("LoginFailed"), base.L("UserIsNotActiveAndCanNotLogin", new object[] { usernameOrEmailAddress }));
				}
				case AbpLoginResultType.InvalidTenancyName:
				{
					return new UserFriendlyException(this.L("LoginFailed"), base.L("ThereIsNoTenantDefinedWithName{0}", new object[] { tenancyName }));
				}
				case AbpLoginResultType.TenantIsNotActive:
				{
					return new UserFriendlyException(this.L("LoginFailed"), base.L("TenantIsNotActive", new object[] { tenancyName }));
				}
				case AbpLoginResultType.UserEmailIsNotConfirmed:
				{
					return new UserFriendlyException(this.L("LoginFailed"), this.L("UserEmailIsNotConfirmedAndCanNotLogin"));
				}
			}
			base.Logger.Warn(string.Concat("Unhandled login fail reason: ", result));
			return new UserFriendlyException(this.L("LoginFailed"));
		}

		private async Task<AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
		{
			AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult abpLoginResult = await this._userManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);
			AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult abpLoginResult1 = abpLoginResult;
			if (abpLoginResult1.Result != AbpLoginResultType.Success)
			{
				throw this.CreateExceptionForFailedLoginAttempt(abpLoginResult1.Result, usernameOrEmailAddress, tenancyName);
			}
			return abpLoginResult1;
		}
	}
}