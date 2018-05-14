using Abp.Application.Navigation;
using Abp.Application.Services.Dto;
using Abp.Configuration.Startup;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Configuration.Tenants.Dto;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.MultiTenancy;
using FuelWerx.Sessions;
using FuelWerx.Sessions.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Layout;
using FuelWerx.Web.Controllers;
using FuelWerx.Web.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class LayoutController : FuelWerxControllerBase
	{
		private readonly ISessionAppService _sessionAppService;

		private readonly IUserNavigationManager _userNavigationManager;

		private readonly IMultiTenancyConfig _multiTenancyConfig;

		private readonly ITenancyNameFinder _tenancyNameFinder;

		private readonly TenantManager _tenantManager;

		private readonly ITenantSettingsAppService _tenantSettingsAppService;

		private readonly IGenericAppService _genericAppService;

		public LayoutController(ISessionAppService sessionAppService, IUserNavigationManager userNavigationManager, IMultiTenancyConfig multiTenancyConfig, ITenancyNameFinder tenancyNameFinder, TenantManager tenantManager, IGenericAppService genericAppService, ITenantSettingsAppService tenantSettingsAppService)
		{
			this._sessionAppService = sessionAppService;
			this._userNavigationManager = userNavigationManager;
			this._tenancyNameFinder = tenancyNameFinder;
			this._multiTenancyConfig = multiTenancyConfig;
			this._tenantManager = tenantManager;
			this._genericAppService = genericAppService;
			this._tenantSettingsAppService = tenantSettingsAppService;
		}

		public async Task<PartialViewResult> CreateOrUpdateQuickMenuItemModal(long? id)
		{
			IGenericAppService genericAppService = this._genericAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetQuickMenuItemForEditOutput quickMenuItemForEdit = await genericAppService.GetQuickMenuItemForEdit(nullableIdInput);
			return this.PartialView("_CreateOrUpdateQuickMenuItemModal", new CreateOrUpdateQuickMenuItemModalViewModel(quickMenuItemForEdit));
		}

		[ChildActionOnly]
		public PartialViewResult Footer()
		{
			return this.PartialView("_Footer", new FooterViewModel()
			{
				LoginInformations = AsyncHelper.RunSync<GetCurrentLoginInformationsOutput>(() => this._sessionAppService.GetCurrentLoginInformations())
			});
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
		public PartialViewResult Header()
		{
			long? userId;
			TenantLogosEditDto tenantLogosEditDto = AsyncHelper.RunSync<TenantLogosEditDto>(() => this.GetCurrentTenantLogos());
			long value = (long)0;
			if (base.AbpSession.UserId.HasValue)
			{
				userId = base.AbpSession.UserId;
				value = userId.Value;
			}
			GetQuickMenuItemsInput getQuickMenuItemsInput = new GetQuickMenuItemsInput()
			{
				OwnerId = new long?(value)
			};
			List<QuickMenuItemListDto> quickMenuItemListDtos = AsyncHelper.RunSync<List<QuickMenuItemListDto>>(() => this._genericAppService.GetQuickMenuItems(getQuickMenuItemsInput));
			HeaderViewModel headerViewModel = new HeaderViewModel()
			{
				LoginInformations = AsyncHelper.RunSync<GetCurrentLoginInformationsOutput>(() => this._sessionAppService.GetCurrentLoginInformations()),
				Languages = base.LocalizationManager.GetAllLanguages(),
				CurrentLanguage = base.LocalizationManager.CurrentLanguage,
				IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled
			};
			userId = base.AbpSession.ImpersonatorUserId;
			headerViewModel.IsImpersonatedLogin = userId.HasValue;
			headerViewModel.CurrentTenantHeaderLogoId = new Guid?((tenantLogosEditDto.HeaderImageId.HasValue ? tenantLogosEditDto.HeaderImageId.Value : Guid.Empty));
			headerViewModel.CurrentTenantHeaderMobileLogoId = new Guid?((tenantLogosEditDto.HeaderMobileImageId.HasValue ? tenantLogosEditDto.HeaderMobileImageId.Value : Guid.Empty));
			headerViewModel.QuickMenuItems = quickMenuItemListDtos;
			return this.PartialView("_Header", headerViewModel);
		}

		public PartialViewResult QuickMenuReload()
		{
			GetQuickMenuItemsInput getQuickMenuItemsInput = new GetQuickMenuItemsInput()
			{
				OwnerId = new long?(base.AbpSession.UserId.Value)
			};
			List<QuickMenuItemListDto> quickMenuItemListDtos = AsyncHelper.RunSync<List<QuickMenuItemListDto>>(() => this._genericAppService.GetQuickMenuItems(getQuickMenuItemsInput));
			return this.PartialView("_QuickMenu", quickMenuItemListDtos);
		}

		[ChildActionOnly]
		public PartialViewResult Sidebar(string currentPageName = "")
		{
			return this.PartialView("_Sidebar", new SidebarViewModel()
			{
				Menu = AsyncHelper.RunSync<UserMenu>(() => this._userNavigationManager.GetMenuAsync("Mpa", base.AbpSession.UserId)),
				CurrentPageName = currentPageName
			});
		}
	}
}