using Abp.Application.Services.Dto;
using Abp.MultiTenancy;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Common;
using FuelWerx.MultiTenancy;
using FuelWerx.MultiTenancy.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Tenants;
using FuelWerx.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Tenants" })]
	public class TenantsController : FuelWerxControllerBase
	{
		private readonly ITenantAppService _tenantAppService;

		private readonly ICommonLookupAppService _lookupAppService;

		private readonly TenantManager _tenantManager;

		public TenantsController(ITenantAppService tenantAppService, ICommonLookupAppService lookupAppService, TenantManager tenantManager)
		{
			this._tenantAppService = tenantAppService;
			this._lookupAppService = lookupAppService;
			this._tenantManager = tenantManager;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenants.Create" })]
		public async Task<PartialViewResult> CreateModal()
		{
			List<ComboboxItemDto> editionComboboxItems = await this.GetEditionComboboxItems(null);
			return this.PartialView("_CreateModal", new CreateTenantViewModel(editionComboboxItems));
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenants.Edit" })]
		public async Task<PartialViewResult> EditModal(int id)
		{
			TenantEditDto tenantForEdit = await this._tenantAppService.GetTenantForEdit(new EntityRequestInput(id));
			TenantEditDto tenantEditDto = tenantForEdit;
			List<ComboboxItemDto> editionComboboxItems = await this.GetEditionComboboxItems(tenantEditDto.EditionId);
			return this.PartialView("_EditModal", new EditTenantViewModel(tenantEditDto, editionComboboxItems));
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenants.ChangeFeatures" })]
		public async Task<PartialViewResult> FeaturesModal(int id)
		{
			Tenant byIdAsync = await this._tenantManager.GetByIdAsync(id);
			GetTenantFeaturesForEditOutput tenantFeaturesForEdit = await this._tenantAppService.GetTenantFeaturesForEdit(new EntityRequestInput(id));
			return this.PartialView("_FeaturesModal", new TenantFeaturesEditViewModel(byIdAsync, tenantFeaturesForEdit));
		}

		private async Task<List<ComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null)
		{
			ListResultOutput<ComboboxItemDto> editionsForCombobox = await this._lookupAppService.GetEditionsForCombobox();
			List<ComboboxItemDto> list = editionsForCombobox.Items.ToList<ComboboxItemDto>();
			ComboboxItemDto comboboxItemDto = new ComboboxItemDto("null", this.L("NotAssigned"));
			list.Insert(0, comboboxItemDto);
			if (!selectedEditionId.HasValue)
			{
				comboboxItemDto.IsSelected = true;
			}
			else
			{
				ComboboxItemDto comboboxItemDto1 = list.FirstOrDefault<ComboboxItemDto>((ComboboxItemDto e) => e.Value == selectedEditionId.Value.ToString());
				if (comboboxItemDto1 != null)
				{
					comboboxItemDto1.IsSelected = true;
				}
			}
			return list;
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}