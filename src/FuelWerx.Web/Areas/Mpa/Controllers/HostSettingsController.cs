using Abp.Web.Mvc.Authorization;
using FuelWerx.Configuration.Host;
using FuelWerx.Configuration.Host.Dto;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.Host.Settings" })]
	public class HostSettingsController : FuelWerxControllerBase
	{
		private readonly IHostSettingsAppService _hostSettingsAppService;

		public HostSettingsController(IHostSettingsAppService hostSettingsAppService)
		{
			this._hostSettingsAppService = hostSettingsAppService;
		}

		public async Task<ActionResult> Index()
		{
			return this.View(await this._hostSettingsAppService.GetAllSettings());
		}
	}
}