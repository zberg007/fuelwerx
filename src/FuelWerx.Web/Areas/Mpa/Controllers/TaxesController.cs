using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using FuelWerx.Administrative.Taxes;
using FuelWerx.Administrative.Taxes.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Taxes;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Taxes" })]
	public class TaxesController : FuelWerxControllerBase
	{
		private readonly ITaxAppService _taxAppService;

		public TaxesController(ITaxAppService taxAppService)
		{
			this._taxAppService = taxAppService;
		}

		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			ITaxAppService taxAppService = this._taxAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetTaxForEditOutput taxForEdit = await taxAppService.GetTaxForEdit(nullableIdInput);
			return this.PartialView("_CreateOrUpdateModal", new CreateOrUpdateTaxModalViewModel(taxForEdit));
		}

		public ActionResult Index(GetTaxesInput input)
		{
			return base.View();
		}
	}
}