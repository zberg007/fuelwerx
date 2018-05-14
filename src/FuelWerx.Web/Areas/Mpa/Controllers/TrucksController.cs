using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using FuelWerx.Assets.Trucks;
using FuelWerx.Assets.Trucks.Dto;
using FuelWerx.Generic;
using FuelWerx.Storage;
using FuelWerx.Web.Areas.Mpa.Models.Trucks;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.Trucks" })]
	public class TrucksController : FuelWerxControllerBase
	{
		private readonly ITruckAppService _truckAppService;

		private readonly IGenericAppService _genericAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public TrucksController(ITruckAppService truckAppService, IGenericAppService genericAppService, IBinaryObjectManager binaryObjectManager)
		{
			this._truckAppService = truckAppService;
			this._genericAppService = genericAppService;
			this._binaryObjectManager = binaryObjectManager;
		}

		public PartialViewResult ChangeTruckImage(TruckImageUploadModel model)
		{
			return this.PartialView("_ChangeTruckImageModal", model);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Trucks.Create" })]
		public async Task<PartialViewResult> CreateOrEditModal(int? id)
		{
			ITruckAppService truckAppService = this._truckAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
            {
                Id = id
            };
			GetTruckForEditOutput truckForEdit = await truckAppService.GetTruckForEdit(nullableIdInput);
			return this.PartialView("_CreateOrEditModal", new CreateOrEditTruckModalViewModel(truckForEdit));
		}

		private FileResult GetDefaultTruckImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-truck-image.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetTruckImageById(Guid? truckImageId)
		{
			FileResult defaultTruckImage;
			Guid guid;
			if (!truckImageId.HasValue || !Guid.TryParse(truckImageId.ToString(), out guid))
			{
				defaultTruckImage = this.GetDefaultTruckImage();
			}
			else
			{
				defaultTruckImage = await this.GetTruckImageById(guid);
			}
			return defaultTruckImage;
		}

		private async Task<FileResult> GetTruckImageById(Guid truckImageId)
		{
			FileResult defaultTruckImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(truckImageId);
			if (orNullAsync != null)
			{
				defaultTruckImage = this.File(orNullAsync.Bytes, "image/jpeg");
			}
			else
			{
				defaultTruckImage = this.GetDefaultTruckImage();
			}
			return defaultTruckImage;
		}

		public ActionResult Index(GetTrucksInput input)
		{
			return base.View();
		}

		[UnitOfWork]
		public virtual async Task<JsonResult> UpdateTruckImage(TruckImageUploadModel model)
		{
			JsonResult jsonResult;
			Guid? imageId;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("TruckImage_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength > 512000)
				{
					throw new UserFriendlyException(this.L("TruckImage_Warn_SizeLimit"));
				}
				GetTruckForEditOutput truckForEdit = await this._truckAppService.GetTruckForEdit(new NullableIdInput<long>(new long?((long)model.TruckId)));
				GetTruckForEditOutput nullable = truckForEdit;
				if (nullable.Truck.ImageId.HasValue)
				{
					IBinaryObjectManager binaryObjectManager = this._binaryObjectManager;
					imageId = nullable.Truck.ImageId;
					await binaryObjectManager.DeleteAsync(imageId.Value);
				}
				BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
				await this._binaryObjectManager.SaveAsync(binaryObject);
				nullable.Truck.ImageId = new Guid?(binaryObject.Id);
				model.Id = nullable.Truck.ImageId;
				UpdateTruckImageInput updateTruckImageInput = new UpdateTruckImageInput()
				{
					TruckId = nullable.Truck.Id.Value
				};
				imageId = nullable.Truck.ImageId;
				updateTruckImageInput.ImageId = new Guid?(imageId.Value);
				await this._truckAppService.SaveImageAsync(updateTruckImageInput);
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