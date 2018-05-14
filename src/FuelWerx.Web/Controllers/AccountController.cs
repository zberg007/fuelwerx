using Abp.Auditing;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using Castle.Core.Logging;
using FuelWerx.Authorization.Impersonation;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Configuration.Tenants.Dto;
using FuelWerx.MultiTenancy;
using FuelWerx.Security;
using FuelWerx.Web;
using FuelWerx.Web.Controllers.Results;
using FuelWerx.Web.Models.Account;
using FuelWerx.Web.MultiTenancy;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class AccountController : FuelWerxControllerBase
	{
		private readonly UserManager _userManager;

		private readonly IUserAppService _userAppService;

		private readonly RoleManager _roleManager;

		private readonly TenantManager _tenantManager;

		private readonly IMultiTenancyConfig _multiTenancyConfig;

		private readonly IUserEmailer _userEmailer;

		private readonly IUnitOfWorkManager _unitOfWorkManager;

		private readonly ITenancyNameFinder _tenancyNameFinder;

		private readonly ICacheManager _cacheManager;

		private readonly IWebUrlService _webUrlService;

		private readonly ITenantSettingsAppService _tenantSettingsAppService;

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return base.HttpContext.GetOwinContext().Authentication;
			}
		}

		public AccountController(UserManager userManager, IUserAppService userAppService, IMultiTenancyConfig multiTenancyConfig, IUserEmailer userEmailer, RoleManager roleManager, TenantManager tenantManager, IUnitOfWorkManager unitOfWorkManager, ITenancyNameFinder tenancyNameFinder, ICacheManager cacheManager, IWebUrlService webUrlService, ITenantSettingsAppService tenantSettingsAppService)
		{
			this._userManager = userManager;
			this._userAppService = userAppService;
			this._multiTenancyConfig = multiTenancyConfig;
			this._userEmailer = userEmailer;
			this._roleManager = roleManager;
			this._tenantManager = tenantManager;
			this._unitOfWorkManager = unitOfWorkManager;
			this._tenancyNameFinder = tenancyNameFinder;
			this._cacheManager = cacheManager;
			this._webUrlService = webUrlService;
			this._tenantSettingsAppService = tenantSettingsAppService;
		}

		public virtual async Task<JsonResult> BackToImpersonator()
		{
			if (!this.AbpSession.ImpersonatorUserId.HasValue)
			{
				throw new UserFriendlyException(this.L("NotImpersonatedLoginErrorMessage"));
			}
			AccountController accountController = this;
			int? impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
			long? impersonatorUserId = this.AbpSession.ImpersonatorUserId;
			JsonResult jsonResult = await accountController.SaveImpersonationTokenAndGetTargetUrl(impersonatorTenantId, impersonatorUserId.Value, true);
			JsonResult jsonResult1 = jsonResult;
			this.AuthenticationManager.SignOut(new string[] { "ApplicationCookie" });
			return jsonResult1;
		}

		private void CheckSelfRegistrationIsEnabled()
		{
			if (!this.IsSelfRegistrationEnabled())
			{
				throw new UserFriendlyException(this.L("SelfUserRegistrationIsDisabledMessage_Detail"));
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

		public ActionResult EmailActivation()
		{
			Guid guid;
			Guid guid1;
			((dynamic)base.ViewBag).IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled;
			((dynamic)base.ViewBag).TenancyName = this._tenancyNameFinder.GetCurrentTenancyNameOrNull();
			TenantLogosEditDto tenantLogosEditDto = Abp.Threading.AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			dynamic viewBag = base.ViewBag;
			guid = (tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty);
			viewBag.TenantCompanyHeaderImageId = guid;
			dynamic obj = base.ViewBag;
			guid1 = (tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty);
			obj.TenantCompanyHeaderMobileImageId = guid1;
			return base.View();
		}

		[UnitOfWork]
		public virtual async Task<ActionResult> EmailConfirmation(EmailConfirmationViewModel model)
		{
			string tenancyName;
			this.CheckModelState();
			IActiveUnitOfWork current = this._unitOfWorkManager.Current;
			current.DisableFilter(new string[] { "MayHaveTenant" });
			long num = Convert.ToInt64(SimpleStringCipher.Decrypt(model.UserId, "gsKnGZ041HLL4IM8"));
			FuelWerx.Authorization.Users.User userByIdAsync = await this._userManager.GetUserByIdAsync(num);
			if (userByIdAsync == null || userByIdAsync.EmailConfirmationCode.IsNullOrEmpty() || userByIdAsync.EmailConfirmationCode != model.ConfirmationCode)
			{
				throw new UserFriendlyException(this.L("InvalidEmailConfirmationCode"), this.L("InvalidEmailConfirmationCode_Detail"));
			}
			userByIdAsync.IsEmailConfirmed = true;
			userByIdAsync.EmailConfirmationCode = null;
			await this._userManager.UpdateAsync(userByIdAsync);
			if (!userByIdAsync.TenantId.HasValue)
			{
				tenancyName = "";
			}
			else
			{
				TenantManager tenantManager = this._tenantManager;
				int? tenantId = userByIdAsync.TenantId;
				tenancyName = (await tenantManager.GetByIdAsync(tenantId.Value)).TenancyName;
			}
			string str = tenancyName;
			ActionResult action = this.RedirectToAction("Login", new { successMessage = this.L("YourEmailIsConfirmedMessage"), tenancyName = str, userNameOrEmailAddress = userByIdAsync.UserName });
			return action;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ExternalLogin(string provider, string returnUrl)
		{
			return new ChallengeResult(provider, base.Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl, tenancyName = this._tenancyNameFinder.GetCurrentTenancyNameOrNull() ?? "" }));
		}

		[UnitOfWork]
		public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl, string tenancyName = "")
		{
			ActionResult action;
			ActionResult actionResult;
			Guid guid;
			Guid guid1;
			ExternalLoginInfo externalLoginInfoAsync = await AuthenticationManagerExtensions.GetExternalLoginInfoAsync(this.AuthenticationManager);
			if (externalLoginInfoAsync != null)
			{
				if (tenancyName.IsNullOrEmpty())
				{
					tenancyName = this._tenancyNameFinder.GetCurrentTenancyNameOrNull();
					if (tenancyName.IsNullOrEmpty())
					{
						List<Tenant> tenants = await this.FindPossibleTenantsOfUserAsync(externalLoginInfoAsync.Login);
						int count = tenants.Count;
						if (count == 0)
						{
							actionResult = await this.RegisterView(externalLoginInfoAsync, null);
							action = actionResult;
							return action;
						}
						else if (count == 1)
						{
							tenancyName = tenants[0].TenancyName;
							tenants = null;
						}
						else
						{
							TenantLogosEditDto tenantLogosEditDto = Abp.Threading.AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
							dynamic viewBag = this.ViewBag;
							guid = (tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty);
							viewBag.TenantCompanyHeaderImageId = guid;
							dynamic obj = this.ViewBag;
							guid1 = (tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty);
							obj.TenantCompanyHeaderMobileImageId = guid1;
							AccountController accountController = this;
							TenantSelectionViewModel tenantSelectionViewModel = new TenantSelectionViewModel()
							{
								Action = this.Url.Action("ExternalLoginCallback", "Account", new { returnUrl = returnUrl }),
								Tenants = tenants.MapTo<List<TenantSelectionViewModel.TenantInfo>>()
							};
							action = accountController.View("TenantSelection", tenantSelectionViewModel);
							return action;
						}
					}
				}
				AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult abpLoginResult = await this._userManager.LoginAsync(externalLoginInfoAsync.Login, tenancyName);
				AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult abpLoginResult1 = abpLoginResult;
				AbpLoginResultType result = abpLoginResult1.Result;
				if (result == AbpLoginResultType.Success)
				{
					await this.SignInAsync(abpLoginResult1.User, abpLoginResult1.Identity, false);
					if (string.IsNullOrWhiteSpace(returnUrl))
					{
						returnUrl = this.Url.Action("Index", "Application");
					}
					action = this.Redirect(returnUrl);
				}
				else
				{
					if (result != AbpLoginResultType.UnknownExternalLogin)
					{
						AccountController accountController1 = this;
						AbpLoginResultType abpLoginResultType = abpLoginResult1.Result;
						string email = externalLoginInfoAsync.Email;
						if (email == null)
						{
							email = externalLoginInfoAsync.DefaultUserName;
						}
						throw accountController1.CreateExceptionForFailedLoginAttempt(abpLoginResultType, email, tenancyName);
					}
					actionResult = await this.RegisterView(externalLoginInfoAsync, tenancyName);
					action = actionResult;
				}
			}
			else
			{
				action = this.RedirectToAction("Login");
			}
			return action;
		}

        [UnitOfWork]
        protected virtual async Task<List<Tenant>> FindPossibleTenantsOfUserAsync(UserLoginInfo login)
        {
            List<User> userList = null;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                userList = await _userManager.FindAllAsync(login);
            }
            return userList.Where(u => u.TenantId != null)
                .Select(u => AsyncHelper.RunSync(() => _tenantManager.FindByIdAsync(u.TenantId.Value)))
                .ToList();
        }

        public ActionResult ForgotPassword()
		{
			Guid guid;
			Guid guid1;
			((dynamic)base.ViewBag).IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled;
			((dynamic)base.ViewBag).TenancyName = this._tenancyNameFinder.GetCurrentTenancyNameOrNull();
			TenantLogosEditDto tenantLogosEditDto = Abp.Threading.AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			dynamic viewBag = base.ViewBag;
			guid = (tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty);
			viewBag.TenantCompanyHeaderImageId = guid;
			dynamic obj = base.ViewBag;
			guid1 = (tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty);
			obj.TenantCompanyHeaderMobileImageId = guid1;
			return base.View();
		}

		private async Task<Tenant> GetActiveTenantAsync(string tenancyName)
		{
			Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(tenancyName);
			if (tenant == null)
			{
				AccountController accountController = this;
				object[] objArray = new object[] { tenancyName };
				throw new UserFriendlyException(accountController.L("ThereIsNoTenantDefinedWithName{0}", objArray));
			}
			if (!tenant.IsActive)
			{
				AccountController accountController1 = this;
				object[] objArray1 = new object[] { tenancyName };
				throw new UserFriendlyException(accountController1.L("TenantIsNotActive", objArray1));
			}
			return tenant;
		}

		private async Task<TenantLogosEditDto> GetCurrentTenantLogos()
		{
			TenantLogosEditDto tenantLogosEditDto = null;
			bool flag = false;
			string currentTenancyNameOrNull = this._tenancyNameFinder.GetCurrentTenancyNameOrNull();
			if (currentTenancyNameOrNull == null)
			{
				flag = true;
			}
			else
			{
				Tenant tenant = await this._tenantManager.FindByTenancyNameAsync(currentTenancyNameOrNull);
				TenantLogosEditDto tenantLogos = await this._tenantSettingsAppService.GetTenantLogos(tenant.Id);
				if (tenantLogos == null)
				{
					flag = true;
				}
				else
				{
					tenantLogosEditDto = tenantLogos;
				}
			}
			if (flag)
			{
				TenantLogosEditDto tenantLogosEditDto1 = new TenantLogosEditDto()
				{
					TenantId = 1,
					HeaderImageId = new Guid?(Guid.Empty),
					HeaderMobileImageId = new Guid?(Guid.Empty),
					InvoiceImageId = new Guid?(Guid.Empty),
					MailImageId = new Guid?(Guid.Empty)
				};
				tenantLogosEditDto = tenantLogosEditDto1;
			}
			return tenantLogosEditDto;
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

		private async Task<int?> GetTenantIdOrDefault(string tenancyName)
		{
			int? nullable;
			if (!tenancyName.IsNullOrEmpty())
			{
				Tenant activeTenantAsync = await this.GetActiveTenantAsync(tenancyName);
				nullable = new int?(activeTenantAsync.Id);
			}
			else
			{
				nullable = this.AbpSession.TenantId;
			}
			return nullable;
		}

		private async Task<FuelWerx.Authorization.Users.User> GetUserByChecking(string emailAddress, string tenancyName)
		{
			int? tenantIdOrDefault = await this.GetTenantIdOrDefault(tenancyName);
			IQueryable<FuelWerx.Authorization.Users.User> users = this._userManager.Users;
			FuelWerx.Authorization.Users.User user = await (
				from u in users
				where u.EmailAddress == emailAddress && u.TenantId == tenantIdOrDefault
				select u).FirstOrDefaultAsync<FuelWerx.Authorization.Users.User>();
			FuelWerx.Authorization.Users.User user1 = user;
			if (user1 == null)
			{
				throw new UserFriendlyException(this.L("InvalidEmailAddress"));
			}
			return user1;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Users.Impersonation" })]
		public virtual async Task<JsonResult> Impersonate(ImpersonateModel model)
		{
			this.CheckModelState();
			if (this.AbpSession.ImpersonatorUserId.HasValue)
			{
				throw new UserFriendlyException(this.L("CascadeImpersonationErrorMessage"));
			}
			if (this.AbpSession.TenantId.HasValue)
			{
				if (!model.TenantId.HasValue)
				{
					throw new UserFriendlyException(this.L("FromTenantToHostImpersonationErrorMessage"));
				}
				if (model.TenantId.Value != this.AbpSession.TenantId.Value)
				{
					throw new UserFriendlyException(this.L("DifferentTenantImpersonationErrorMessage"));
				}
			}
			JsonResult jsonResult = await this.SaveImpersonationTokenAndGetTargetUrl(model.TenantId, model.UserId, false);
			JsonResult jsonResult1 = jsonResult;
			this.AuthenticationManager.SignOut(new string[] { "ApplicationCookie" });
			return jsonResult1;
		}

		[UnitOfWork]
		public virtual async Task<ActionResult> ImpersonateSignIn(string tokenId)
		{
			ActionResult action;
			ImpersonationCacheItem orDefaultAsync = await this._cacheManager.GetImpersonationCache().GetOrDefaultAsync(tokenId);
			ImpersonationCacheItem impersonationCacheItem = orDefaultAsync;
			if (impersonationCacheItem == null)
			{
				throw new UserFriendlyException(this.L("ImpersonationTokenErrorMessage"));
			}
			using (IDisposable disposable = this._unitOfWorkManager.Current.SetFilterParameter("MayHaveTenant", "tenantId", impersonationCacheItem.TargetTenantId))
			{
				FuelWerx.Authorization.Users.User user = await this._userManager.FindByIdAsync(impersonationCacheItem.TargetUserId);
				ClaimsIdentity claimsIdentity = await this._userManager.CreateIdentityAsync(user, "ApplicationCookie");
				if (!impersonationCacheItem.IsBackToImpersonator)
				{
					if (impersonationCacheItem.ImpersonatorTenantId.HasValue)
					{
						int value = impersonationCacheItem.ImpersonatorTenantId.Value;
						claimsIdentity.AddClaim(new Claim("http://www.aspnetboilerplate.com/identity/claims/impersonatorTenantId", value.ToString(CultureInfo.InvariantCulture)));
					}
					long impersonatorUserId = impersonationCacheItem.ImpersonatorUserId;
					claimsIdentity.AddClaim(new Claim("http://www.aspnetboilerplate.com/identity/claims/impersonatorUserId", impersonatorUserId.ToString(CultureInfo.InvariantCulture)));
				}
				this.AuthenticationManager.SignOut(new string[] { "ApplicationCookie" });
				IAuthenticationManager authenticationManager = this.AuthenticationManager;
				AuthenticationProperties authenticationProperty = new AuthenticationProperties()
				{
					IsPersistent = false
				};
				authenticationManager.SignIn(authenticationProperty, new ClaimsIdentity[] { claimsIdentity });
				await this._cacheManager.GetImpersonationCache().RemoveAsync(tokenId);
				action = this.RedirectToAction("Index", "Application");
			}
			return action;
		}

		public virtual JsonResult IsImpersonatedLogin()
		{
			return base.Json(new MvcAjaxResponse()
			{
				Result = base.AbpSession.ImpersonatorUserId.HasValue
			});
		}

		private bool IsSelfRegistrationEnabled()
		{
			string currentTenancyNameOrNull = this._tenancyNameFinder.GetCurrentTenancyNameOrNull();
			if (currentTenancyNameOrNull.IsNullOrEmpty())
			{
				return true;
			}
			Tenant tenant = Abp.Threading.AsyncHelper.RunSync<Tenant>(() => this.GetActiveTenantAsync(currentTenancyNameOrNull));
			return base.SettingManager.GetSettingValueForTenant<bool>("App.UserManagement.AllowSelfRegistration", tenant.Id);
		}

		[ChildActionOnly]
		public PartialViewResult Languages()
		{
			return this.PartialView("~/Views/Account/_Languages.cshtml", new LanguagesViewModel()
			{
				AllLanguages = base.LocalizationManager.GetAllLanguages(),
				CurrentLanguage = base.LocalizationManager.CurrentLanguage
			});
		}

		public ActionResult Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "")
		{
			Guid guid;
			Guid guid1;
			if (string.IsNullOrWhiteSpace(returnUrl))
			{
				returnUrl = base.Url.Action("Index", "Application");
			}
			((dynamic)base.ViewBag).ReturnUrl = returnUrl;
			((dynamic)base.ViewBag).IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled;
			TenantLogosEditDto tenantLogosEditDto = Abp.Threading.AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			dynamic viewBag = base.ViewBag;
			guid = (tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty);
			viewBag.TenantCompanyHeaderImageId = guid;
			dynamic obj = base.ViewBag;
			guid1 = (tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty);
			obj.TenantCompanyHeaderMobileImageId = guid1;
			return base.View(new LoginFormViewModel()
			{
				TenancyName = this._tenancyNameFinder.GetCurrentTenancyNameOrNull(),
				IsSelfRegistrationEnabled = this.IsSelfRegistrationEnabled(),
				SuccessMessage = successMessage,
				UserNameOrEmailAddress = userNameOrEmailAddress
			});
		}

		[DisableAuditing]
		[HttpPost]
		[UnitOfWork]
		public virtual async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
		{
			JsonResult jsonResult;
			this.CheckModelState();
			IActiveUnitOfWork current = this._unitOfWorkManager.Current;
			current.DisableFilter(new string[] { "MayHaveTenant" });
			AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult loginResultAsync = await this.GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, loginModel.TenancyName);
			AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult abpLoginResult = loginResultAsync;
			if (!abpLoginResult.User.ShouldChangePasswordOnNextLogin)
			{
				await this.SignInAsync(abpLoginResult.User, abpLoginResult.Identity, loginModel.RememberMe);
				if (string.IsNullOrWhiteSpace(returnUrl))
				{
					returnUrl = this.Url.Action("Index", "Application");
				}
				if (!string.IsNullOrWhiteSpace(returnUrlHash))
				{
					returnUrl = string.Concat(returnUrl, returnUrlHash);
				}
				FuelWerx.Authorization.Users.User user = await this._userManager.FindByNameOrEmailAsync(loginModel.UsernameOrEmailAddress.ToString());
				FuelWerx.Authorization.Users.User user1 = user;
				if (user1 != null)
				{
					string userPostLoginViewType = await this._userAppService.GetUserPostLoginViewType(user1.Id);
					if (!string.IsNullOrEmpty(userPostLoginViewType))
					{
						this.Session.Add("PostLoginRedirectCheck", userPostLoginViewType);
					}
					bool flag = await this._userAppService.ShowScreencastAtLogin(user1.Id);
					if (flag)
					{
						this.Session.Add("ShowScreencastAtLoginCheck", flag);
					}
				}
				AccountController accountController = this;
				MvcAjaxResponse mvcAjaxResponse = new MvcAjaxResponse()
				{
					TargetUrl = returnUrl
				};
				jsonResult = accountController.Json(mvcAjaxResponse);
			}
			else
			{
				abpLoginResult.User.SetNewPasswordResetCode();
				AccountController accountController1 = this;
				MvcAjaxResponse mvcAjaxResponse1 = new MvcAjaxResponse();
				UrlHelper url = this.Url;
				ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
				long id = abpLoginResult.User.Id;
				resetPasswordViewModel.UserId = SimpleStringCipher.Encrypt(id.ToString(), "gsKnGZ041HLL4IM8");
				resetPasswordViewModel.ResetCode = abpLoginResult.User.PasswordResetCode;
				mvcAjaxResponse1.TargetUrl = url.Action("ResetPassword", resetPasswordViewModel);
				jsonResult = accountController1.Json(mvcAjaxResponse1);
			}
			return jsonResult;
		}

		public ActionResult Logout()
		{
			this.AuthenticationManager.SignOut(new string[0]);
			return base.RedirectToAction("Login");
		}

		public ActionResult Register()
		{
			return this.RegisterView(new RegisterViewModel()
			{
				TenancyName = this._tenancyNameFinder.GetCurrentTenancyNameOrNull()
			});
		}

		[HttpPost]
		[UnitOfWork]
		[ValidateAntiForgeryToken]
		public virtual async Task<ActionResult> Register(RegisterViewModel model)
		{
			ActionResult actionResult;
			AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult loginResultAsync;
			Guid guid;
			Guid guid1;
			bool flag;
			AbpUserManager<Tenant, Role, FuelWerx.Authorization.Users.User>.AbpLoginResult abpLoginResult;
			TenantLogosEditDto tenantLogosEditDto = Abp.Threading.AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			dynamic viewBag = this.ViewBag;
			guid = (tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty);
			viewBag.TenantCompanyHeaderImageId = guid;
			dynamic obj = this.ViewBag;
			guid1 = (tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty);
			obj.TenantCompanyHeaderMobileImageId = guid1;
			try
			{
				this.CheckSelfRegistrationIsEnabled();
				this.CheckModelState();
				if (!model.IsExternalLogin && this.UseCaptchaOnRegistration())
				{
					RecaptchaVerificationHelper recaptchaVerificationHelper = this.GetRecaptchaVerificationHelper();
					if (recaptchaVerificationHelper.Response.IsNullOrEmpty())
					{
						throw new UserFriendlyException(this.L("CaptchaCanNotBeEmpty"));
					}
					if (recaptchaVerificationHelper.VerifyRecaptchaResponse() != RecaptchaVerificationResult.Success)
					{
						throw new UserFriendlyException(this.L("IncorrectCaptchaAnswer"));
					}
				}
				if (!this._multiTenancyConfig.IsEnabled)
				{
					model.TenancyName = "Default";
				}
				else if (model.TenancyName.IsNullOrEmpty())
				{
					throw new UserFriendlyException(this.L("TenantNameCanNotBeEmpty"));
				}
				Tenant activeTenantAsync = await this.GetActiveTenantAsync(model.TenancyName);
				bool settingValueForTenantAsync = await SettingManagerExtensions.GetSettingValueForTenantAsync<bool>(this.SettingManager, "App.UserManagement.AllowSelfRegistration", activeTenantAsync.Id);
				if (!settingValueForTenantAsync)
				{
					throw new UserFriendlyException(this.L("SelfUserRegistrationIsDisabledMessage_Detail"));
				}
				settingValueForTenantAsync = await SettingManagerExtensions.GetSettingValueForTenantAsync<bool>(this.SettingManager, "App.UserManagement.IsNewRegisteredUserActiveByDefault", activeTenantAsync.Id);
				bool flag1 = settingValueForTenantAsync;
				settingValueForTenantAsync = await SettingManagerExtensions.GetSettingValueForTenantAsync<bool>(this.SettingManager, "Abp.Zero.UserManagement.IsEmailConfirmationRequiredForLogin", activeTenantAsync.Id);
				bool flag2 = settingValueForTenantAsync;
				FuelWerx.Authorization.Users.User user = new FuelWerx.Authorization.Users.User()
				{
					TenantId = new int?(activeTenantAsync.Id),
					Name = model.Name,
					Surname = model.Surname,
					EmailAddress = model.EmailAddress,
					IsActive = flag1
				};
				FuelWerx.Authorization.Users.User userName = user;
				ExternalLoginInfo externalLoginInfoAsync = null;
				if (model.IsExternalLogin)
				{
					externalLoginInfoAsync = await AuthenticationManagerExtensions.GetExternalLoginInfoAsync(this.AuthenticationManager);
					if (externalLoginInfoAsync == null)
					{
						throw new ApplicationException("Can not external login!");
					}
					FuelWerx.Authorization.Users.User user1 = userName;
					List<UserLogin> userLogins = new List<UserLogin>();
					UserLogin userLogin = new UserLogin()
					{
						LoginProvider = externalLoginInfoAsync.Login.LoginProvider,
						ProviderKey = externalLoginInfoAsync.Login.ProviderKey
					};
					userLogins.Add(userLogin);
					user1.Logins = userLogins;
					model.UserName = model.EmailAddress;
					model.Password = FuelWerx.Authorization.Users.User.CreateRandomPassword();
					if (string.Equals(externalLoginInfoAsync.Email, model.EmailAddress, StringComparison.InvariantCultureIgnoreCase))
					{
						userName.IsEmailConfirmed = true;
					}
				}
				else if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
				{
					throw new UserFriendlyException(this.L("FormIsNotValidMessage"));
				}
				userName.UserName = model.UserName;
				userName.Password = (new PasswordHasher()).HashPassword(model.Password);
				IActiveUnitOfWork current = this._unitOfWorkManager.Current;
				current.EnableFilter(new string[] { "MayHaveTenant" });
				this._unitOfWorkManager.Current.SetFilterParameter("MayHaveTenant", "tenantId", activeTenantAsync.Id);
				userName.Roles = new List<UserRole>();
				IQueryable<Role> roles = this._roleManager.Roles;
				List<Role> listAsync = await (
					from r in roles
					where r.IsDefault
					select r).ToListAsync<Role>();
				foreach (Role role in listAsync)
				{
					ICollection<UserRole> userRoles = userName.Roles;
					userRoles.Add(new UserRole()
					{
						RoleId = role.Id
					});
				}
				this.CheckErrors(await this._userManager.CreateAsync(userName));
				await this._unitOfWorkManager.Current.SaveChangesAsync();
				if (!userName.IsEmailConfirmed)
				{
					userName.SetNewEmailConfirmationCode();
					await this._userEmailer.SendEmailActivationLinkAsync(userName, null);
				}
				if (userName.IsActive && (userName.IsEmailConfirmed || !flag2))
				{
					if (externalLoginInfoAsync == null)
					{
						loginResultAsync = await this.GetLoginResultAsync(userName.UserName, model.Password, activeTenantAsync.TenancyName);
						abpLoginResult = loginResultAsync;
					}
					else
					{
						loginResultAsync = await this._userManager.LoginAsync(externalLoginInfoAsync.Login, activeTenantAsync.TenancyName);
						abpLoginResult = loginResultAsync;
					}
					if (abpLoginResult.Result != AbpLoginResultType.Success)
					{
						this.Logger.Warn(string.Concat("New registered user could not be login. This should not be normally. login result: ", abpLoginResult.Result));
						abpLoginResult = null;
					}
					else
					{
						await this.SignInAsync(abpLoginResult.User, abpLoginResult.Identity, false);
						actionResult = this.Redirect(this.Url.Action("Index", "Application"));
						return actionResult;
					}
				}
				AccountController accountController = this;
				RegisterResultViewModel registerResultViewModel = new RegisterResultViewModel()
				{
					TenancyName = activeTenantAsync.TenancyName,
					NameAndSurname = string.Concat(userName.Name, " ", userName.Surname),
					UserName = userName.UserName,
					EmailAddress = userName.EmailAddress,
					IsActive = userName.IsActive,
					IsEmailConfirmationRequired = flag2
				};
				actionResult = accountController.View("RegisterResult", registerResultViewModel);
			}
			catch (UserFriendlyException userFriendlyException1)
			{
				UserFriendlyException userFriendlyException = userFriendlyException1;
				((dynamic)this.ViewBag).IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled;
				dynamic viewBag1 = this.ViewBag;
				flag = (model.IsExternalLogin ? false : this.UseCaptchaOnRegistration());
				viewBag1.UseCaptcha = flag;
				((dynamic)this.ViewBag).ErrorMessage = userFriendlyException.Message;
				actionResult = this.View("Register", model);
			}
			return actionResult;
		}

		public ActionResult RegisterView(RegisterViewModel model)
		{
			bool flag;
			Guid guid;
			Guid guid1;
			this.CheckSelfRegistrationIsEnabled();
			((dynamic)base.ViewBag).IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled;
			dynamic viewBag = base.ViewBag;
			flag = (model.IsExternalLogin ? false : this.UseCaptchaOnRegistration());
			viewBag.UseCaptcha = flag;
			TenantLogosEditDto tenantLogosEditDto = Abp.Threading.AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			dynamic obj = base.ViewBag;
			guid = (tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty);
			obj.TenantCompanyHeaderImageId = guid;
			dynamic viewBag1 = base.ViewBag;
			guid1 = (tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty);
			viewBag1.TenantCompanyHeaderMobileImageId = guid1;
			return base.View("Register", model);
		}

		private async Task<ActionResult> RegisterView(ExternalLoginInfo loginInfo, string tenancyName = null)
		{
			ActionResult actionResult;
			string defaultUserName = loginInfo.DefaultUserName;
			string str = loginInfo.DefaultUserName;
			bool flag = AccountController.TryExtractNameAndSurnameFromClaims(loginInfo.ExternalIdentity.Claims, ref defaultUserName, ref str);
			RegisterViewModel registerViewModel = new RegisterViewModel()
			{
				TenancyName = tenancyName,
				EmailAddress = loginInfo.Email,
				Name = defaultUserName,
				Surname = str,
				IsExternalLogin = true
			};
			RegisterViewModel registerViewModel1 = registerViewModel;
			if (!(!tenancyName.IsNullOrEmpty() & flag))
			{
				actionResult = this.RegisterView(registerViewModel1);
			}
			else
			{
				actionResult = await this.Register(registerViewModel1);
			}
			return actionResult;
		}

		[UnitOfWork]
		public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			Guid guid;
			Guid guid1;
			this.CheckModelState();
			IActiveUnitOfWork current = this._unitOfWorkManager.Current;
			current.DisableFilter(new string[] { "MayHaveTenant" });
			long num = Convert.ToInt64(SimpleStringCipher.Decrypt(model.UserId, "gsKnGZ041HLL4IM8"));
			FuelWerx.Authorization.Users.User userByIdAsync = await this._userManager.GetUserByIdAsync(num);
			if (userByIdAsync == null || userByIdAsync.PasswordResetCode.IsNullOrEmpty() || userByIdAsync.PasswordResetCode != model.ResetCode)
			{
				throw new UserFriendlyException(this.L("InvalidPasswordResetCode"), this.L("InvalidPasswordResetCode_Detail"));
			}
			TenantLogosEditDto tenantLogosEditDto = Abp.Threading.AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			dynamic viewBag = this.ViewBag;
			guid = (tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty);
			viewBag.TenantCompanyHeaderImageId = guid;
			dynamic obj = this.ViewBag;
			guid1 = (tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty);
			obj.TenantCompanyHeaderMobileImageId = guid1;
			return this.View(model);
		}

		[HttpPost]
		[UnitOfWork]
		public virtual async Task<ActionResult> ResetPassword(ResetPasswordFormViewModel model)
		{
			this.CheckModelState();
			IActiveUnitOfWork current = this._unitOfWorkManager.Current;
			current.DisableFilter(new string[] { "MayHaveTenant" });
			long num = Convert.ToInt64(SimpleStringCipher.Decrypt(model.UserId, "gsKnGZ041HLL4IM8"));
			FuelWerx.Authorization.Users.User userByIdAsync = await this._userManager.GetUserByIdAsync(num);
			if (userByIdAsync == null || userByIdAsync.PasswordResetCode.IsNullOrEmpty() || userByIdAsync.PasswordResetCode != model.ResetCode)
			{
				throw new UserFriendlyException(this.L("InvalidPasswordResetCode"), this.L("InvalidPasswordResetCode_Detail"));
			}
			userByIdAsync.Password = (new PasswordHasher()).HashPassword(model.Password);
			userByIdAsync.PasswordResetCode = null;
			userByIdAsync.IsEmailConfirmed = true;
			userByIdAsync.ShouldChangePasswordOnNextLogin = false;
			await this._userManager.UpdateAsync(userByIdAsync);
			if (userByIdAsync.IsActive)
			{
				await this.SignInAsync(userByIdAsync, null, false);
			}
			return this.RedirectToAction("Index", "Application");
		}

		private async Task<JsonResult> SaveImpersonationTokenAndGetTargetUrl(int? tenantId, long userId, bool isBackToImpersonator)
		{
			ImpersonationCacheItem impersonationCacheItem = new ImpersonationCacheItem(tenantId, userId, isBackToImpersonator);
			if (!isBackToImpersonator)
			{
				impersonationCacheItem.ImpersonatorTenantId = this.AbpSession.TenantId;
				impersonationCacheItem.ImpersonatorUserId = this.AbpSession.GetUserId();
			}
			string str = Guid.NewGuid().ToString();
			await this._cacheManager.GetImpersonationCache().SetAsync(str, impersonationCacheItem, new TimeSpan?(TimeSpan.FromMinutes(1)));
			string tenancyName = null;
			if (tenantId.HasValue)
			{
				Tenant byIdAsync = await this._tenantManager.GetByIdAsync(tenantId.Value);
				tenancyName = byIdAsync.TenancyName;
			}
			string str1 = string.Concat(this._webUrlService.GetSiteRootAddress(tenancyName), "Account/ImpersonateSignIn?tokenId=", str);
			return this.Json(new MvcAjaxResponse()
			{
				TargetUrl = str1
			});
		}

		[HttpPost]
		[UnitOfWork]
		[ValidateAntiForgeryToken]
		public virtual async Task<JsonResult> SendEmailActivationLink(SendEmailActivationLinkViewModel model)
		{
			IActiveUnitOfWork current = this._unitOfWorkManager.Current;
			current.DisableFilter(new string[] { "MayHaveTenant" });
			FuelWerx.Authorization.Users.User userByChecking = await this.GetUserByChecking(model.EmailAddress, model.TenancyName);
			FuelWerx.Authorization.Users.User user = userByChecking;
			user.SetNewEmailConfirmationCode();
			await this._userEmailer.SendEmailActivationLinkAsync(user, null);
			return this.Json(new MvcAjaxResponse());
		}

		[HttpPost]
		[UnitOfWork]
		[ValidateAntiForgeryToken]
		public virtual async Task<JsonResult> SendPasswordResetLink(SendPasswordResetLinkViewModel model)
		{
			this.CheckModelState();
			IActiveUnitOfWork current = this._unitOfWorkManager.Current;
			current.DisableFilter(new string[] { "MayHaveTenant" });
			FuelWerx.Authorization.Users.User userByChecking = await this.GetUserByChecking(model.EmailAddress, model.TenancyName);
			FuelWerx.Authorization.Users.User user = userByChecking;
			user.SetNewPasswordResetCode();
			await this._userEmailer.SendPasswordResetLinkAsync(user);
			return this.Json(new MvcAjaxResponse());
		}

		private async Task SignInAsync(FuelWerx.Authorization.Users.User user, ClaimsIdentity identity = null, bool rememberMe = false)
		{
			if (identity == null)
			{
				identity = await this._userManager.CreateIdentityAsync(user, "ApplicationCookie");
			}
			this.AuthenticationManager.SignOut(new string[] { "ApplicationCookie" });
			IAuthenticationManager authenticationManager = this.AuthenticationManager;
			AuthenticationProperties authenticationProperty = new AuthenticationProperties()
			{
				IsPersistent = rememberMe
			};
			authenticationManager.SignIn(authenticationProperty, new ClaimsIdentity[] { identity });
		}

		private static bool TryExtractNameAndSurnameFromClaims(IEnumerable<Claim> claims, ref string name, ref string surname)
		{
			string value = null;
			string str = null;
			Claim claim = claims.FirstOrDefault<Claim>((Claim c) => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
			if (claim != null && !claim.Value.IsNullOrEmpty())
			{
				value = claim.Value;
			}
			Claim claim1 = claims.FirstOrDefault<Claim>((Claim c) => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname");
			if (claim1 != null && !claim1.Value.IsNullOrEmpty())
			{
				str = claim1.Value;
			}
			if (value == null || str == null)
			{
				Claim claim2 = claims.FirstOrDefault<Claim>((Claim c) => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
				if (claim2 != null)
				{
					string value1 = claim2.Value;
					if (!value1.IsNullOrEmpty())
					{
						int num = value1.LastIndexOf(' ');
						if (num < 1 || num > value1.Length - 2)
						{
							string str1 = value1;
							str = str1;
							value = str1;
						}
						else
						{
							value = value1.Substring(0, num);
							str = value1.Substring(num);
						}
					}
				}
			}
			if (!value.IsNullOrEmpty())
			{
				name = value;
			}
			if (!str.IsNullOrEmpty())
			{
				surname = str;
			}
			if (value == null)
			{
				return false;
			}
			return str != null;
		}

		private bool UseCaptchaOnRegistration()
		{
			string currentTenancyNameOrNull = this._tenancyNameFinder.GetCurrentTenancyNameOrNull();
			if (currentTenancyNameOrNull.IsNullOrEmpty())
			{
				return true;
			}
			Tenant tenant = Abp.Threading.AsyncHelper.RunSync<Tenant>(() => this.GetActiveTenantAsync(currentTenancyNameOrNull));
			return base.SettingManager.GetSettingValueForTenant<bool>("App.UserManagement.UseCaptchaOnRegistration", tenant.Id);
		}
	}
}