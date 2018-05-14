using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using FuelWerx.Editions;
using FuelWerx.Editions.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Editions;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Editions" })]
	public class EditionsController : FuelWerxControllerBase
	{
		private readonly IEditionAppService _editionAppService;

		public EditionsController(IEditionAppService editionAppService)
		{
			this._editionAppService = editionAppService;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Editions.Create", "Pages.Editions.Edit" })]
		public async Task<PartialViewResult> CreateOrEditModal(int? id)
		{
			IEditionAppService editionAppService = this._editionAppService;
			NullableIdInput nullableIdInput = new NullableIdInput()
			{
				Id = id
			};
			GetEditionForEditOutput editionForEdit = await editionAppService.GetEditionForEdit(nullableIdInput);
			return this.PartialView("_CreateOrEditModal", new CreateOrEditEditionModalViewModel(editionForEdit));
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}