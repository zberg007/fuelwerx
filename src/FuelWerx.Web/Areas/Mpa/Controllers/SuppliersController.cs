using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using FuelWerx.Generic;
using FuelWerx.Storage;
using FuelWerx.Suppliers;
using FuelWerx.Suppliers.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Suppliers;
using FuelWerx.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.Suppliers" })]
	public class SuppliersController : FuelWerxControllerBase
	{
		private readonly ISupplierAppService _supplierAppService;

		private readonly IGenericAppService _genericAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public SuppliersController(ISupplierAppService supplierAppService, IGenericAppService genericAppService, IBinaryObjectManager binaryObjectManager)
		{
			this._supplierAppService = supplierAppService;
			this._genericAppService = genericAppService;
			this._binaryObjectManager = binaryObjectManager;
		}

		public PartialViewResult ChangeSupplierLogo(SupplierLogoUploadModel model)
		{
			return this.PartialView("_ChangeSupplierLogoModal", model);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Suppliers.Create" })]
		public async Task<PartialViewResult> CreateOrEditModal(int? id)
		{
			ISupplierAppService supplierAppService = this._supplierAppService;
            NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
            {
                Id = id
            };
			CreateOrEditSupplierModalViewModel createOrEditSupplierModalViewModel = new CreateOrEditSupplierModalViewModel(await supplierAppService.GetSupplierForEdit(nullableIdInput));
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			using (HttpClient httpClient = new HttpClient())
			{
				string str = this.Url.RouteUrl("DefaultApiWithAction", new { httproute = "", controller = "Generic", action = "GetCountriesAsSelectListItems", countryId = 0, selectedCountryId = createOrEditSupplierModalViewModel.Supplier.CountryId }, this.Request.Url.Scheme);
				using (HttpResponseMessage async = await httpClient.GetAsync(str))
				{
					if (async.IsSuccessStatusCode)
					{
						string str1 = await async.Content.ReadAsStringAsync();
						selectListItems = JsonConvert.DeserializeObject<List<SelectListItem>>(str1);
					}
				}
			}
			List<SelectListItem> selectListItems1 = selectListItems;
			SelectListItem selectListItem = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems1.Insert(0, selectListItem);
			this.ViewData["Countries"] = selectListItems.AsEnumerable<SelectListItem>();
			return this.PartialView("_CreateOrEditModal", createOrEditSupplierModalViewModel);
		}

		private FileResult GetDefaultSupplierLogo()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-supplier-logo.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetSupplierLogoById(Guid? supplierLogoId)
		{
			FileResult defaultSupplierLogo;
			Guid guid;
			if (!supplierLogoId.HasValue || !Guid.TryParse(supplierLogoId.ToString(), out guid))
			{
				defaultSupplierLogo = this.GetDefaultSupplierLogo();
			}
			else
			{
				defaultSupplierLogo = await this.GetSupplierLogoById(guid);
			}
			return defaultSupplierLogo;
		}

		private async Task<FileResult> GetSupplierLogoById(Guid supplierLogoId)
		{
			FileResult defaultSupplierLogo;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(supplierLogoId);
			if (orNullAsync != null)
			{
				defaultSupplierLogo = this.File(orNullAsync.Bytes, "image/jpeg");
			}
			else
			{
				defaultSupplierLogo = this.GetDefaultSupplierLogo();
			}
			return defaultSupplierLogo;
		}

		public ActionResult Index(GetSuppliersInput input)
		{
			return base.View();
		}

		[UnitOfWork]
		public virtual async Task<JsonResult> UpdateSupplierLogo(SupplierLogoUploadModel model)
		{
			JsonResult jsonResult;
			Guid? logoId;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("SupplierLogo_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength > 512000)
				{
					throw new UserFriendlyException(this.L("SupplierLogo_Warn_SizeLimit"));
				}
				GetSupplierForEditOutput supplierForEdit = await this._supplierAppService.GetSupplierForEdit(new NullableIdInput<long>(new long?((long)model.SupplierId)));
				GetSupplierForEditOutput nullable = supplierForEdit;
				if (nullable.Supplier.LogoId.HasValue)
				{
					IBinaryObjectManager binaryObjectManager = this._binaryObjectManager;
					logoId = nullable.Supplier.LogoId;
					await binaryObjectManager.DeleteAsync(logoId.Value);
				}
				BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
				await this._binaryObjectManager.SaveAsync(binaryObject);
				nullable.Supplier.LogoId = new Guid?(binaryObject.Id);
				model.Id = nullable.Supplier.LogoId;
				UpdateSupplierLogoInput updateSupplierLogoInput = new UpdateSupplierLogoInput()
				{
					SupplierId = nullable.Supplier.Id.Value
				};
				logoId = nullable.Supplier.LogoId;
				updateSupplierLogoInput.LogoId = new Guid?(logoId.Value);
				await this._supplierAppService.SaveLogoAsync(updateSupplierLogoInput);
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