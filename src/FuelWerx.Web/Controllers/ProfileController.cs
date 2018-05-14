using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.MultiTenancy;
using FuelWerx.Storage;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class ProfileController : FuelWerxControllerBase
	{
		private readonly UserManager _userManager;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public ProfileController(UserManager userManager, IBinaryObjectManager binaryObjectManager)
		{
			this._userManager = userManager;
			this._binaryObjectManager = binaryObjectManager;
		}

		[UnitOfWork]
		public virtual async Task<JsonResult> ChangeProfilePicture()
		{
			JsonResult jsonResult;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("ProfilePicture_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength > 512000)
				{
					throw new UserFriendlyException(this.L("ProfilePicture_Warn_SizeLimit"));
				}
				FuelWerx.Authorization.Users.User userByIdAsync = await this._userManager.GetUserByIdAsync(this.AbpSession.GetUserId());
				FuelWerx.Authorization.Users.User nullable = userByIdAsync;
				if (nullable.ProfilePictureId.HasValue)
				{
					await this._binaryObjectManager.DeleteAsync(nullable.ProfilePictureId.Value);
				}
				BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
				await this._binaryObjectManager.SaveAsync(binaryObject);
				nullable.ProfilePictureId = new Guid?(binaryObject.Id);
				jsonResult = this.Json(new MvcAjaxResponse());
			}
			catch (UserFriendlyException userFriendlyException1)
			{
				UserFriendlyException userFriendlyException = userFriendlyException1;
				jsonResult = this.Json(new MvcAjaxResponse(new ErrorInfo(userFriendlyException.Message), false));
			}
			return jsonResult;
		}

		private FileResult GetDefaultProfilePicture()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-profile-picture.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetProfilePicture()
		{
			FileResult profilePictureById;
			FuelWerx.Authorization.Users.User userByIdAsync = await this._userManager.GetUserByIdAsync(this.AbpSession.GetUserId());
			FuelWerx.Authorization.Users.User user = userByIdAsync;
			if (user.ProfilePictureId.HasValue)
			{
				profilePictureById = await this.GetProfilePictureById(user.ProfilePictureId.Value);
			}
			else
			{
				profilePictureById = this.GetDefaultProfilePicture();
			}
			return profilePictureById;
		}

		[DisableAuditing]
		public async Task<FileResult> GetProfilePictureById(string id = "")
		{
			FileResult defaultProfilePicture;
			if (!id.IsNullOrEmpty())
			{
				FileResult profilePictureById = await this.GetProfilePictureById(Guid.Parse(id));
				defaultProfilePicture = profilePictureById;
			}
			else
			{
				defaultProfilePicture = this.GetDefaultProfilePicture();
			}
			return defaultProfilePicture;
		}

		private async Task<FileResult> GetProfilePictureById(Guid profilePictureId)
		{
			FileResult defaultProfilePicture;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(profilePictureId);
			if (orNullAsync != null)
			{
				defaultProfilePicture = this.File(orNullAsync.Bytes, "image/jpeg");
			}
			else
			{
				defaultProfilePicture = this.GetDefaultProfilePicture();
			}
			return defaultProfilePicture;
		}
	}
}