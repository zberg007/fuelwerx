using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative;
using FuelWerx.Administrative.Taxes;
using FuelWerx.Administrative.TaxRules;
using FuelWerx.Administrative.TaxRules.Dto;
using FuelWerx.Generic;
using FuelWerx.Web.Areas.Mpa.Models.TaxRules;
using FuelWerx.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Administration.TaxRules" })]
	public class TaxRulesController : FuelWerxControllerBase
	{
		private readonly ITaxRuleAppService _taxRuleAppService;

		private readonly ITaxAppService _taxAppService;

		public TaxRulesController(ITaxRuleAppService taxRuleAppService, ITaxAppService taxAppService)
		{
			this._taxRuleAppService = taxRuleAppService;
			this._taxAppService = taxAppService;
		}

		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			ITaxRuleAppService taxRuleAppService = this._taxRuleAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetTaxRuleForEditOutput taxRuleForEdit = await taxRuleAppService.GetTaxRuleForEdit(nullableIdInput);
			return this.PartialView("_CreateOrUpdateModal", new CreateOrUpdateTaxRuleModalViewModel(taxRuleForEdit));
		}

		public async Task<PartialViewResult> CreateOrUpdateTaxRuleRuleModal(long taxRuleId, long? id = null)
		{
			ITaxRuleAppService taxRuleAppService = this._taxRuleAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetTaxRuleRuleForEditOutput taxRuleRuleForEdit = await taxRuleAppService.GetTaxRuleRuleForEdit(nullableIdInput);
			if (!taxRuleRuleForEdit.TaxRuleRule.Id.HasValue)
			{
				taxRuleRuleForEdit.TaxRuleRule.TaxRuleId = taxRuleId;
			}
			CreateOrUpdateTaxRuleRuleModalViewModel createOrUpdateTaxRuleRuleModalViewModel = new CreateOrUpdateTaxRuleRuleModalViewModel(taxRuleRuleForEdit);
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			using (HttpClient httpClient = new HttpClient())
			{
				string str = this.Url.RouteUrl("DefaultApiWithAction", new { httproute = "", controller = "Generic", action = "GetCountriesAsSelectListItems", countryId = 0, selectedCountryId = createOrUpdateTaxRuleRuleModalViewModel.TaxRuleRule.CountryId }, this.Request.Url.Scheme);
				using (HttpResponseMessage async = await httpClient.GetAsync(str))
				{
					if (async.IsSuccessStatusCode)
					{
						string str1 = await async.Content.ReadAsStringAsync();
						selectListItems = JsonConvert.DeserializeObject<List<SelectListItem>>(str1);
					}
				}
			}
			List<SelectListItem> selectListItems1 = selectListItems;
			SelectListItem selectListItem = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems1.Insert(0, selectListItem);
			this.ViewData["Countries"] = selectListItems.AsEnumerable<SelectListItem>();
			List<SelectListItem> selectListItems2 = new List<SelectListItem>();
			foreach (Tax taxesForTaxRule in await this._taxAppService.GetTaxesForTaxRules())
			{
				List<SelectListItem> selectListItems3 = selectListItems2;
				SelectListItem selectListItem1 = new SelectListItem()
				{
					Text = string.Format("{0} - {1}%", taxesForTaxRule.Name, taxesForTaxRule.Rate),
					Value = taxesForTaxRule.Id.ToString(),
					Disabled = false,
					Selected = false
				};
				selectListItems3.Add(selectListItem1);
			}
			List<SelectListItem> selectListItems4 = selectListItems2;
			SelectListItem selectListItem2 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems4.Insert(0, selectListItem2);
			this.ViewData["Taxes"] = selectListItems2.AsEnumerable<SelectListItem>();
			List<SelectListItem> selectListItems5 = new List<SelectListItem>();
			foreach (Lookup lookupItem in (new LookupFill("TaxRuleBehaviors", this.AbpSession.TenantId.Value)).LookupItems)
			{
				SelectListItem selectListItem3 = new SelectListItem()
				{
					Text = lookupItem.Text,
					Value = lookupItem.Value,
					Disabled = lookupItem.Disabled,
					Selected = lookupItem.Selected
				};
				selectListItems5.Add(selectListItem3);
			}
			SelectListItem selectListItem4 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems5.Insert(0, selectListItem4);
			this.ViewData["TaxRuleBehaviors"] = selectListItems5;
			return this.PartialView("_CreateOrUpdateTaxRuleRuleModal", createOrUpdateTaxRuleRuleModalViewModel);
		}

		public ActionResult Index(GetTaxRulesInput input)
		{
			return base.View();
		}
	}
}