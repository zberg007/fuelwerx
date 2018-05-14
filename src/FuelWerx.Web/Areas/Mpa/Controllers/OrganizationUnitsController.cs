using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.Web.Mvc.Authorization;
using FuelWerx.Organizations;
using FuelWerx.Organizations.Dto;
using FuelWerx.Web.Areas.Mpa.Models.OrganizationUnits;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.OrganizationUnits" })]
	public class OrganizationUnitsController : FuelWerxControllerBase
	{
		private readonly IOrganizationUnitAppService _organizationUnitAppService;

		private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;

		private readonly IRepository<OrganizationUnitProperties, long> _organizationUnitPropertiesRepository;

		public OrganizationUnitsController(IOrganizationUnitAppService organizationUnitAppService, IRepository<OrganizationUnit, long> organizationUnitRepository, IRepository<OrganizationUnitProperties, long> organizationUnitPropertiesRepository)
		{
			this._organizationUnitAppService = organizationUnitAppService;
			this._organizationUnitRepository = organizationUnitRepository;
			this._organizationUnitPropertiesRepository = organizationUnitPropertiesRepository;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageOrganizationTree" })]
		public PartialViewResult CreateModal(long? parentId)
		{
			return this.PartialView("_CreateModal", new CreateOrganizationUnitModalViewModel(parentId));
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageOrganizationTree" })]
		public async Task<PartialViewResult> EditModal(long id)
		{
			OrganizationUnit async = await this._organizationUnitRepository.GetAsync(id);
			return this.PartialView("_EditModal", async.MapTo<EditOrganizationUnitModalViewModel>());
		}

		public ActionResult Index()
		{
			return base.View();
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.OrganizationUnits.ManageProperties" })]
		public async Task<PartialViewResult> PropertiesModal(long organizationUnitId)
		{
			PartialViewResult partialViewResult;
			OrganizationUnitPropertiesDto organizationUnitProperties = await this._organizationUnitAppService.GetOrganizationUnitProperties(organizationUnitId);
			if (organizationUnitProperties == null || organizationUnitProperties.Id <= (long)0)
			{
				partialViewResult = this.PartialView("_PropertiesModal", new UpdateOrganizationUnitPropertiesModalViewModel(organizationUnitId));
			}
			else
			{
				UpdateOrganizationUnitPropertiesModalViewModel updateOrganizationUnitPropertiesModalViewModel = new UpdateOrganizationUnitPropertiesModalViewModel(organizationUnitProperties.OrganizationUnitId)
				{
					Id = new long?(organizationUnitProperties.Id),
					Discount = organizationUnitProperties.Discount,
					ShowPrice = organizationUnitProperties.ShowPrice,
					Upcharge = organizationUnitProperties.Upcharge,
					SpecificPricesEnabled = organizationUnitProperties.SpecificPricesEnabled
				};
				partialViewResult = this.PartialView("_PropertiesModal", updateOrganizationUnitPropertiesModalViewModel);
			}
			return partialViewResult;
		}
	}
}