using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class ApplicationController : FuelWerxControllerBase
	{
		public ApplicationController()
		{
		}

		[DisableAuditing]
		public ActionResult Index()
		{
			return base.RedirectToAction("Index", "Home", new { area = "Mpa" });
		}
	}
}