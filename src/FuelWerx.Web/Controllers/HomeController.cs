using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Controllers
{
	public class HomeController : FuelWerxControllerBase
	{
		public HomeController()
		{
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}