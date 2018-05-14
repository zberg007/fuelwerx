using Abp.Web.Mvc.Authorization;
using FuelWerx.Authorization.Users.Profile;
using FuelWerx.Authorization.Users.Profile.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Profile;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class ProfileController : Controller
	{
		private readonly IProfileAppService _profileAppService;

		public ProfileController(IProfileAppService profileAppService)
		{
			this._profileAppService = profileAppService;
		}

		public PartialViewResult ChangePasswordModal()
		{
			return base.PartialView("_ChangePasswordModal");
		}

		public PartialViewResult ChangePictureModal()
		{
			return base.PartialView("_ChangePictureModal");
		}

		public async Task<PartialViewResult> MySettingsModal()
		{
			CurrentUserProfileEditDto currentUserProfileForEdit = await this._profileAppService.GetCurrentUserProfileForEdit();
			return this.PartialView("_MySettingsModal", new MySettingsViewModel(currentUserProfileForEdit));
		}
	}
}