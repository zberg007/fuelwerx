using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa
{
	public class MpaAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Mpa";
			}
		}

		public MpaAreaRegistration()
		{
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("Mpa_default", "Mpa/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
		}
	}
}