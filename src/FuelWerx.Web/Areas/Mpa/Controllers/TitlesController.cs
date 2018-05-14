using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using FuelWerx.Administrative.Titles;
using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.Generic;
using FuelWerx.Storage;
using FuelWerx.Web.Areas.Mpa.Models.Titles;
using FuelWerx.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Titles" })]
	public class TitlesController : FuelWerxControllerBase
	{
		private readonly ITitleAppService _titleAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public TitlesController(ITitleAppService titleAppService, IBinaryObjectManager binaryObjectManager)
		{
			this._titleAppService = titleAppService;
			this._binaryObjectManager = binaryObjectManager;
		}

		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			ITitleAppService titleAppService = this._titleAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateTitleModalViewModel createOrUpdateTitleModalViewModel = new CreateOrUpdateTitleModalViewModel(await titleAppService.GetTitleForEdit(nullableIdInput));
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (Lookup lookupItem in (new LookupFill("TitleTypes", -1)).LookupItems)
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
			SelectListItem selectListItem1 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems.Insert(0, selectListItem1);
			this.ViewData["TitleTypes"] = selectListItems;
			return this.PartialView("_CreateOrUpdateModal", createOrUpdateTitleModalViewModel);
		}

		private FileResult GetDefaultTitleImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-title-image.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetTitleImageById(Guid? titleImageId)
		{
			FileResult defaultTitleImage;
			Guid guid;
			if (!titleImageId.HasValue || !Guid.TryParse(titleImageId.ToString(), out guid))
			{
				defaultTitleImage = this.GetDefaultTitleImage();
			}
			else
			{
				defaultTitleImage = await this.GetTitleImageById(guid);
			}
			return defaultTitleImage;
		}

		private async Task<FileResult> GetTitleImageById(Guid titleImageId)
		{
			FileResult defaultTitleImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(titleImageId);
			if (orNullAsync != null)
			{
				defaultTitleImage = this.File(orNullAsync.Bytes, "image/jpeg");
			}
			else
			{
				defaultTitleImage = this.GetDefaultTitleImage();
			}
			return defaultTitleImage;
		}

		public ActionResult Index(GetTitlesInput input)
		{
			return base.View();
		}

		[UnitOfWork]
		public virtual async Task<JsonResult> UpdateTitleImage(long titleId)
		{
			JsonResult jsonResult;
			Guid? imageId;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("TitleImage_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength > 512000)
				{
					throw new UserFriendlyException(this.L("TitleImage_Warn_SizeLimit"));
				}
				GetTitleForEditOutput titleForEdit = await this._titleAppService.GetTitleForEdit(new NullableIdInput<long>(new long?(titleId)));
				GetTitleForEditOutput nullable = titleForEdit;
				if (nullable.Title.ImageId.HasValue)
				{
					IBinaryObjectManager binaryObjectManager = this._binaryObjectManager;
					imageId = nullable.Title.ImageId;
					await binaryObjectManager.DeleteAsync(imageId.Value);
				}
				BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
				await this._binaryObjectManager.SaveAsync(binaryObject);
				nullable.Title.ImageId = new Guid?(binaryObject.Id);
				UpdateTitleImageInput updateTitleImageInput = new UpdateTitleImageInput()
				{
					TitleId = nullable.Title.Id.Value
				};
				imageId = nullable.Title.ImageId;
				updateTitleImageInput.ImageId = new Guid?(imageId.Value);
				await this._titleAppService.SaveTitleImageAsync(updateTitleImageInput);
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