using Abp.Web.Mvc.Authorization;
using FuelWerx.Web.Controllers;
using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class WelcomeController : FuelWerxControllerBase
	{
		public WelcomeController()
		{
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}