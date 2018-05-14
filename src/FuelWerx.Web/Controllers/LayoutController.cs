using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Configuration.Tenants.Dto;
using FuelWerx.MultiTenancy;
using FuelWerx.Sessions;
using FuelWerx.Sessions.Dto;
using FuelWerx.Web.Models.Layout;
using FuelWerx.Web.MultiTenancy;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class LayoutController : FuelWerxControllerBase
	{
		private readonly ISessionAppService _sessionAppService;

		private readonly IUserNavigationManager _userNavigationManager;

		private readonly ITenancyNameFinder _tenancyNameFinder;

		private readonly IMultiTenancyConfig _multiTenancyConfig;

		private readonly TenantManager _tenantManager;

		private readonly ITenantSettingsAppService _tenantSettingsAppService;

		public LayoutController(ISessionAppService sessionAppService, IUserNavigationManager userNavigationManager, ITenancyNameFinder tenancyNameFinder, TenantManager tenantManager, IMultiTenancyConfig multiTenancyConfig, ITenantSettingsAppService tenantSettingsAppService)
		{
			this._sessionAppService = sessionAppService;
			this._userNavigationManager = userNavigationManager;
			this._tenancyNameFinder = tenancyNameFinder;
			this._multiTenancyConfig = multiTenancyConfig;
			this._tenantManager = tenantManager;
			this._tenantSettingsAppService = tenantSettingsAppService;
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

		[ChildActionOnly]
		public PartialViewResult Header(string currentPageName = "")
		{
			HeaderViewModel headerViewModel = new HeaderViewModel();
			if (base.AbpSession.UserId.HasValue)
			{
				headerViewModel.LoginInformations = AsyncHelper.RunSync<GetCurrentLoginInformationsOutput>(() => this._sessionAppService.GetCurrentLoginInformations());
			}
			headerViewModel.Languages = base.LocalizationManager.GetAllLanguages();
			headerViewModel.CurrentLanguage = base.LocalizationManager.CurrentLanguage;
			headerViewModel.Menu = AsyncHelper.RunSync<UserMenu>(() => this._userNavigationManager.GetMenuAsync("Frontend", base.AbpSession.UserId));
			headerViewModel.CurrentPageName = currentPageName;
			TenantLogosEditDto tenantLogosEditDto = AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			headerViewModel.CurrentTenantHeaderLogoId = tenantLogosEditDto.HeaderImageId;
			headerViewModel.CurrentTenantHeaderMobileLogoId = tenantLogosEditDto.HeaderMobileImageId;
			headerViewModel.IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled;
			return this.PartialView("~/Views/Layout/_Header.cshtml", headerViewModel);
		}
	}
}