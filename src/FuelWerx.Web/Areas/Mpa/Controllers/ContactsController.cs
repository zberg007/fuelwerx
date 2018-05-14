using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using FuelWerx.Administrative.Contacts;
using FuelWerx.Administrative.Contacts.Dto;
using FuelWerx.Storage;
using FuelWerx.Web.Areas.Mpa.Models.Contacts;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Contacts" })]
	public class ContactsController : FuelWerxControllerBase
	{
		private readonly IContactAppService _contactAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public ContactsController(IContactAppService contactAppService, IBinaryObjectManager binaryObjectManager)
		{
			this._contactAppService = contactAppService;
			this._binaryObjectManager = binaryObjectManager;
		}

		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			IContactAppService contactAppService = this._contactAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetContactForEditOutput contactForEdit = await contactAppService.GetContactForEdit(nullableIdInput);
			return this.PartialView("_CreateOrUpdateModal", new CreateOrUpdateContactModalViewModel(contactForEdit));
		}

		[DisableAuditing]
		public async Task<FileResult> GetContactImageById(Guid? contactImageId)
		{
			FileResult defaultContactImage;
			Guid guid;
			if (!contactImageId.HasValue || !Guid.TryParse(contactImageId.ToString(), out guid))
			{
				defaultContactImage = this.GetDefaultContactImage();
			}
			else
			{
				defaultContactImage = await this.GetContactImageById(guid);
			}
			return defaultContactImage;
		}

		private async Task<FileResult> GetContactImageById(Guid contactImageId)
		{
			FileResult defaultContactImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(contactImageId);
			if (orNullAsync != null)
			{
				defaultContactImage = this.File(orNullAsync.Bytes, "image/jpeg");
			}
			else
			{
				defaultContactImage = this.GetDefaultContactImage();
			}
			return defaultContactImage;
		}

		private FileResult GetDefaultContactImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-contact-image.png"), "image/png");
		}

		public ActionResult Index(GetContactsInput input)
		{
			return base.View();
		}

		[UnitOfWork]
		public virtual async Task<JsonResult> UpdateContactImage(long contactId)
		{
			JsonResult jsonResult;
			Guid? imageId;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("ContactImage_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength > 512000)
				{
					throw new UserFriendlyException(this.L("ContactImage_Warn_SizeLimit"));
				}
				GetContactForEditOutput contactForEdit = await this._contactAppService.GetContactForEdit(new NullableIdInput<long>(new long?(contactId)));
				GetContactForEditOutput nullable = contactForEdit;
				if (nullable.Contact.ImageId.HasValue)
				{
					IBinaryObjectManager binaryObjectManager = this._binaryObjectManager;
					imageId = nullable.Contact.ImageId;
					await binaryObjectManager.DeleteAsync(imageId.Value);
				}
				BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
				await this._binaryObjectManager.SaveAsync(binaryObject);
				nullable.Contact.ImageId = new Guid?(binaryObject.Id);
				UpdateContactImageInput updateContactImageInput = new UpdateContactImageInput()
				{
					ContactId = nullable.Contact.Id.Value
				};
				imageId = nullable.Contact.ImageId;
				updateContactImageInput.ImageId = new Guid?(imageId.Value);
				await this._contactAppService.SaveContactImageAsync(updateContactImageInput);
				jsonResult = this.Json(new MvcAjaxResponse());
			}
			catch (UserFriendlyException userFriendlyException1)
			{
				UserFriendlyException userFriendlyException = userFriendlyException1;
				jsonResult = this.Json(new MvcAjaxResponse(new ErrorInfo(userFriendlyException.Message), false));
			}
			return jsonResult;
		}
	}
}