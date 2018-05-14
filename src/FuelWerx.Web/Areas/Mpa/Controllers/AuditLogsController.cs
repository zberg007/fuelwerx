using Abp.Web.Mvc.Authorization;
using FuelWerx.Web.Controllers;
using System;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.AuditLogs" })]
	[DisableAuditing]
	public class AuditLogsController : FuelWerxControllerBase
	{
		public AuditLogsController()
		{
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}