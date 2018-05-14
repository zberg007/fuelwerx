using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Assets.FillLots;
using FuelWerx.Assets.FillLots.Dto;
using FuelWerx.Storage;
using FuelWerx.Web.Areas.Mpa.Models.FillLots;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.FillLots" })]
	public class FillLotsController : FuelWerxControllerBase
	{
		private readonly IFillLotAppService _fillLotAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public FillLotsController(IFillLotAppService fillLotAppService, IBinaryObjectManager binaryObjectManager)
		{
			this._fillLotAppService = fillLotAppService;
			this._binaryObjectManager = binaryObjectManager;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.FillLots.Create", "Pages.Tenant.FillLots.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			int? impersonatorTenantId;
			int value;
			if (this.AbpSession.ImpersonatorTenantId.HasValue)
			{
				impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
				value = impersonatorTenantId.Value;
			}
			else
			{
				impersonatorTenantId = this.AbpSession.TenantId;
				value = impersonatorTenantId.Value;
			}
			IFillLotAppService fillLotAppService = this._fillLotAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetFillLotForEditOutput fillLotForEdit = await fillLotAppService.GetFillLotForEdit(nullableIdInput);
			return this.PartialView("_CreateOrUpdateModal", new CreateOrUpdateFillLotModalViewModel(fillLotForEdit));
		}

		public ActionResult Index(GetFillLotsInput input)
		{
			return base.View();
		}
	}
}