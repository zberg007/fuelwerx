using Abp.Web.Mvc.Authorization;
using FuelWerx.Web.Areas.Mpa.Models.Common.Modals;
using FuelWerx.Web.Controllers;
using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class CommonController : FuelWerxControllerBase
	{
		public CommonController()
		{
		}

		public PartialViewResult LookupModal(LookupModalViewModel model)
		{
			return this.PartialView("Modals/_LookupModal", model);
		}
	}
}