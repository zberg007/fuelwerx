using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Authorization.Users;
using FuelWerx.Web.Controllers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] {  })]
	public class HomeController : FuelWerxControllerBase
	{
		private readonly IUserAppService _userAppService;

		public HomeController(IUserAppService userAppService)
		{
			this._userAppService = userAppService;
		}

		public async Task<ActionResult> Index()
		{
			ActionResult action;
			bool flag;
			if (this.AbpSession.MultiTenancySide == MultiTenancySides.Host)
			{
				flag = await this.IsGrantedAsync("Pages.Tenants");
				if (flag)
				{
					action = this.RedirectToAction("Index", "Tenants");
					return action;
				}
			}
			else if (this.Session["PostLoginRedirectCheck"] == null)
			{
				flag = await this.IsGrantedAsync("Pages.Tenant.Dashboard");
				if (flag)
				{
					action = this.RedirectToAction("Index", "Dashboard");
					return action;
				}
			}
			else
			{
				string str = this.Session["PostLoginRedirectCheck"].ToString();
				this.Session.Remove("PostLoginRedirectCheck");
				if (str == "Delivery Schedule")
				{
					flag = await this.IsGrantedAsync("Pages.Tenant.Dashboard");
					if (flag)
					{
						action = this.RedirectToAction("Index", "Dashboard");
						return action;
					}
				}
				else if (str == "Invoices")
				{
					flag = await this.IsGrantedAsync("Pages.Tenant.Invoices");
					if (flag)
					{
						action = this.RedirectToAction("", "Invoices", new { area = "Mpa" });
						return action;
					}
				}
				else if (str == "Suppliers")
				{
					flag = await this.IsGrantedAsync("Pages.Tenant.Suppliers");
					if (flag)
					{
						action = this.RedirectToAction("", "Suppliers", new { area = "Mpa" });
						return action;
					}
				}
				else
				{
					flag = await this.IsGrantedAsync("Pages.Tenant.Dashboard");
					if (flag)
					{
						action = this.RedirectToAction("Index", "Dashboard");
						return action;
					}
				}
			}
			action = this.RedirectToAction("Index", "Welcome");
			return action;
		}
	}
}