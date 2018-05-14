using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Generic;
using FuelWerx.MultiTenancy;
using FuelWerx.Web.Areas.Mpa.Models.Users;
using FuelWerx.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Users" })]
	public class UsersController : FuelWerxControllerBase
	{
		private readonly IUserAppService _userAppService;

		private readonly UserManager _userManager;

		public UsersController(IUserAppService userAppService, UserManager userManager)
		{
			this._userAppService = userAppService;
			this._userManager = userManager;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Users.Create", "Pages.Administration.Users.Edit" })]
		public async Task<PartialViewResult> CreateOrEditModal(long? id)
		{
			int num;
			IUserAppService userAppService = this._userAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrEditUserModalViewModel createOrEditUserModalViewModel = new CreateOrEditUserModalViewModel(await userAppService.GetUserForEdit(nullableIdInput));
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			num = (this.AbpSession.TenantId.HasValue ? this.AbpSession.TenantId.Value : 0);
			foreach (Lookup lookupItem in (new LookupFill("PostLoginViewTypes", num)).LookupItems)
			{
				SelectListItem selectListItem = new SelectListItem()
				{
					Text = lookupItem.Text,
					Value = lookupItem.Value,
					Disabled = lookupItem.Disabled,
					Selected = lookupItem.Selected
				};
				selectListItems.Add(selectListItem);
			}
			this.ViewData["PostLoginViewTypes"] = selectListItems;
			return this.PartialView("_CreateOrEditModal", createOrEditUserModalViewModel);
		}

		public ActionResult Index()
		{
			return base.View();
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Users.ChangePermissions" })]
		public async Task<PartialViewResult> PermissionsModal(long id)
		{
			FuelWerx.Authorization.Users.User userByIdAsync = await this._userManager.GetUserByIdAsync(id);
			GetUserPermissionsForEditOutput userPermissionsForEdit = await this._userAppService.GetUserPermissionsForEdit(new IdInput<long>(id));
			return this.PartialView("_PermissionsModal", new UserPermissionsEditViewModel(userPermissionsForEdit, userByIdAsync));
		}
	}
}