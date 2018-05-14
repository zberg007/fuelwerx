using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Roles.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Roles;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Roles" })]
	public class RolesController : FuelWerxControllerBase
	{
		private readonly IRoleAppService _roleAppService;

		public RolesController(IRoleAppService roleAppService)
		{
			this._roleAppService = roleAppService;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Roles.Create", "Pages.Administration.Roles.Edit" })]
		public async Task<PartialViewResult> CreateOrEditModal(int? id)
		{
			IRoleAppService roleAppService = this._roleAppService;
			NullableIdInput nullableIdInput = new NullableIdInput()
			{
				Id = id
			};
			GetRoleForEditOutput roleForEdit = await roleAppService.GetRoleForEdit(nullableIdInput);
			return this.PartialView("_CreateOrEditModal", new CreateOrEditRoleModalViewModel(roleForEdit));
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}