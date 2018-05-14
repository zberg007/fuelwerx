using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace FuelWerx.Web.Routing
{
	public static class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}", null);
			routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new string[] { "FuelWerx.Web.Controllers" });
		}
	}
}