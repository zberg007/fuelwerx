using Abp.Authorization.Roles;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Customers;
using FuelWerx.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.Dashboard" })]
	public class DashboardController : FuelWerxControllerBase
	{
		private readonly IRoleAppService _roleAppService;

		private readonly IUserAppService _userManagerAppService;

		private readonly UserManager _userManager;

		private readonly IRepository<Role> _roleRepository;

		private readonly IRepository<Customer, long> _customerRepository;

		public DashboardController(IRoleAppService roleAppService, IRepository<Role> roleRepository, IUserAppService userManagerAppService, UserManager userManager, IRepository<Customer, long> customerRepository)
		{
			this._roleAppService = roleAppService;
			this._userManagerAppService = userManagerAppService;
			this._userManager = userManager;
			this._roleRepository = roleRepository;
			this._customerRepository = customerRepository;
		}

		public async Task<ActionResult> Customer()
		{
			ActionResult actionPermanent;
			long? impersonatorUserId;
			long value;
			string str = this.L("KeyName_CustomersRole");
			IRepository<Role> repository = this._roleRepository;
			List<Role> allListAsync = await repository.GetAllListAsync((Role m) => m.TenantId == (int?)((this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value)) && m.DisplayName == str);
			List<Role> roles = allListAsync;
			UserManager userManager = this._userManager;
			if (this.AbpSession.ImpersonatorUserId.HasValue)
			{
				impersonatorUserId = this.AbpSession.ImpersonatorUserId;
				value = impersonatorUserId.Value;
			}
			else
			{
				impersonatorUserId = this.AbpSession.UserId;
				value = impersonatorUserId.Value;
			}
			IList<string> rolesAsync = await userManager.GetRolesAsync(value);
			if (roles.Count != 1 || !rolesAsync.Contains(roles[0].Name))
			{
				actionPermanent = this.RedirectToActionPermanent("EOops", "Error", new { Area = "" });
			}
			else
			{
				IRepository<Customer, long> repository1 = this._customerRepository;
				List<Customer> customers = await repository1.GetAllListAsync((Customer m) => m.UserId == (long?)((this.AbpSession.ImpersonatorUserId.HasValue ? this.AbpSession.ImpersonatorUserId.Value : this.AbpSession.UserId.Value)));
				List<Customer> customers1 = customers;
				if (customers1.Count != 1)
				{
					actionPermanent = this.RedirectToActionPermanent("EOops", "Error", new { Area = "" });
				}
				else
				{
					this.Session["CustomerId"] = customers1[0].Id;
					actionPermanent = this.View();
				}
			}
			return actionPermanent;
		}

		public async Task<ActionResult> Driver()
		{
			ActionResult actionPermanent;
			long? impersonatorUserId;
			long value;
			string str = this.L("KeyName_DriversRole");
			IRepository<Role> repository = this._roleRepository;
			List<Role> allListAsync = await repository.GetAllListAsync((Role m) => m.TenantId == (int?)((this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value)) && m.DisplayName == str);
			List<Role> roles = allListAsync;
			UserManager userManager = this._userManager;
			if (this.AbpSession.ImpersonatorUserId.HasValue)
			{
				impersonatorUserId = this.AbpSession.ImpersonatorUserId;
				value = impersonatorUserId.Value;
			}
			else
			{
				impersonatorUserId = this.AbpSession.UserId;
				value = impersonatorUserId.Value;
			}
			IList<string> rolesAsync = await userManager.GetRolesAsync(value);
			if (roles.Count != 1 || !rolesAsync.Contains(roles[0].Name))
			{
				actionPermanent = this.RedirectToActionPermanent("EOops", "Error", new { Area = "" });
			}
			else
			{
				actionPermanent = this.View();
			}
			return actionPermanent;
		}

		public async Task<ActionResult> Index()
		{
			ActionResult action;
			long? impersonatorUserId;
			long value;
			string str = "";
			UserManager userManager = this._userManager;
			if (this.AbpSession.ImpersonatorUserId.HasValue)
			{
				impersonatorUserId = this.AbpSession.ImpersonatorUserId;
				value = impersonatorUserId.Value;
			}
			else
			{
				impersonatorUserId = this.AbpSession.UserId;
				value = impersonatorUserId.Value;
			}
			IList<string> rolesAsync = await userManager.GetRolesAsync(value);
			if (rolesAsync.Count > 0)
			{
				string str1 = this.L("KeyName_CustomersRole");
				string str2 = this.L("KeyName_DriversRole");
				IRepository<Role> repository = this._roleRepository;
				List<Role> allListAsync = await repository.GetAllListAsync((Role m) => m.TenantId == (int?)((this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value)) && m.DisplayName == str1);
				List<Role> roles = allListAsync;
				IRepository<Role> repository1 = this._roleRepository;
				allListAsync = await repository1.GetAllListAsync((Role m) => m.TenantId == (int?)((this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value)) && m.DisplayName == str2);
				List<Role> roles1 = allListAsync;
				if (roles.Count == 1 && rolesAsync.Contains(roles[0].Name))
				{
					((dynamic)this.ViewBag).WelcomeMessage = "**WELCOME CUSTOMER WE'RE IN DEVELOPMENT**";
					str = "Customer";
				}
				else if (roles1.Count == 1 && rolesAsync.Contains(roles1[0].Name))
				{
					((dynamic)this.ViewBag).WelcomeMessage = "**WELCOME DRIVER WE'RE IN DEVELOPMENT**";
					str = "Driver";
				}
				else if (rolesAsync.Contains("Admin") || rolesAsync.Contains("admin"))
				{
					((dynamic)this.ViewBag).WelcomeMessage = "**WELCOME ADMIN USER WE'RE IN DEVELOPMENT**";
				}
				roles = null;
			}
			if (this.Session["ShowScreencastAtLoginCheck"] != null)
			{
				((dynamic)this.ViewBag).ShowScreencast = true;
				this.Session.Remove("ShowScreencastAtLoginCheck");
			}
			if (str.Length <= 0)
			{
				action = this.View();
			}
			else
			{
				action = this.RedirectToAction(str.ToString(), "Dashboard", new { Area = "Mpa" });
			}
			return action;
		}
	}
}