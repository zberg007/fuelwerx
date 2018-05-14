using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class AboutController : FuelWerxControllerBase
	{
		public AboutController()
		{
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}