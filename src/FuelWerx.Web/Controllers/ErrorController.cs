using Abp.Auditing;
using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class ErrorController : FuelWerxControllerBase
	{
		public ErrorController()
		{
		}

		[DisableAuditing]
		public ActionResult E404()
		{
			return base.View();
		}

		public ActionResult EOops()
		{
			return base.View();
		}
	}
}