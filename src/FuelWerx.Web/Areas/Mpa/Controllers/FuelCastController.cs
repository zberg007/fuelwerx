using Abp.Web.Mvc.Authorization;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class FuelCastController : FuelWerxControllerBase
	{
		public FuelCastController()
		{
		}

		public async Task<ActionResult> Index()
		{
			return this.View();
		}
	}
}