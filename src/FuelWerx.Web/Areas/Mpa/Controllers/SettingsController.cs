using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Configuration.Tenants.Dto;
using FuelWerx.Generic;
using FuelWerx.MultiTenancy;
using FuelWerx.Storage;
using FuelWerx.Web.Areas.Mpa.Models.Tenants;
using FuelWerx.Web.Controllers;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	public class SettingsController : FuelWerxControllerBase
	{
		private readonly ITenantSettingsAppService _tenantSettingsAppService;

		private readonly IMultiTenancyConfig _multiTenancyConfig;

		private readonly TenantManager _tenantManager;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly IRepository<PaymentSetting, long> _paymentSettingRepository;

		public SettingsController(ITenantSettingsAppService tenantSettingsAppService, IMultiTenancyConfig multiTenancyConfig, TenantManager tenantManager, IBinaryObjectManager binaryObjectManager, IRepository<PaymentSetting, long> paymentSettingRepository)
		{
			this._tenantSettingsAppService = tenantSettingsAppService;
			this._multiTenancyConfig = multiTenancyConfig;
			this._tenantManager = tenantManager;
			this._binaryObjectManager = binaryObjectManager;
			this._paymentSettingRepository = paymentSettingRepository;
		}

		private FileResult GetDefaultLogo(string logoType = "header", string viewContrast = "light")
		{
			if (logoType == "mobile")
			{
				return base.File(base.Server.MapPath(string.Format("~/Common/Images/default-logo-header-mobile-{0}.png", viewContrast)), "image/png");
			}
			if (logoType == "mail")
			{
				return base.File(base.Server.MapPath(string.Format("~/Common/Images/default-logo-mail-{0}.png", viewContrast)), "image/png");
			}
			if (logoType == "invoice")
			{
				return base.File(base.Server.MapPath(string.Format("~/Common/Images/default-logo-invoice-{0}.png", viewContrast)), "image/png");
			}
			return base.File(base.Server.MapPath(string.Format("~/Common/Images/default-logo-header-{0}.png", viewContrast)), "image/png");
		}

		[AllowAnonymous]
		[DisableAuditing]
		public async Task<FileResult> GetLogoById(Guid? logoId, string logoType = "header", string viewContrast = "light")
		{
			FileResult defaultLogo;
			Guid guid;
			if (!logoId.HasValue || !Guid.TryParse(logoId.ToString(), out guid))
			{
				defaultLogo = this.GetDefaultLogo(logoType, viewContrast);
			}
			else
			{
				defaultLogo = await this.GetLogoByIdOrDefault(guid, logoType, viewContrast);
			}
			return defaultLogo;
		}

		private async Task<FileResult> GetLogoByIdOrDefault(Guid logoId, string logoType, string viewContrast)
		{
			FileResult defaultLogo;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(logoId);
			if (orNullAsync != null)
			{
				defaultLogo = this.File(orNullAsync.Bytes, "image/jpeg");
			}
			else
			{
				defaultLogo = this.GetDefaultLogo(logoType, viewContrast);
			}
			return defaultLogo;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Tenant.Settings" })]
		public async Task<ActionResult> Index()
		{
			string settingValueAsync;
			int value;
			int? tenantId = null;
			TenantSettingsEditDto allSettings = await this._tenantSettingsAppService.GetAllSettings(tenantId);
			((dynamic)this.ViewBag).IsMultiTenancyEnabled = this._multiTenancyConfig.IsEnabled;
			TenantManager tenantManager = this._tenantManager;
			tenantId = this.AbpSession.TenantId;
			Tenant byIdAsync = await tenantManager.GetByIdAsync(tenantId.Value);
			((dynamic)this.ViewBag).TenantName = byIdAsync.Name.Trim();
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			using (HttpClient httpClient = new HttpClient())
			{
				string str = this.Url.RouteUrl("DefaultApiWithAction", new { httproute = "", controller = "Generic", action = "GetCountriesAsSelectListItems", countryId = 0, selectedCountryId = allSettings.Details.CountryId }, this.Request.Url.Scheme);
				using (HttpResponseMessage async = await httpClient.GetAsync(str))
				{
					if (async.IsSuccessStatusCode)
					{
						settingValueAsync = await async.Content.ReadAsStringAsync();
						selectListItems = JsonConvert.DeserializeObject<List<SelectListItem>>(settingValueAsync);
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
			settingValueAsync = await this.SettingManager.GetSettingValueAsync("App.General.Timezones");
			List<TimeZoneInfo> timeZoneInfos = JsonConvert.DeserializeObject<List<TimeZoneInfo>>(settingValueAsync);
			List<SelectListItem> selectListItems2 = new List<SelectListItem>();
			foreach (TimeZoneInfo timeZoneInfo in timeZoneInfos)
			{
				SelectListItem selectListItem1 = new SelectListItem()
				{
					Text = timeZoneInfo.DisplayName,
					Value = timeZoneInfo.Id,
					Disabled = false,
					Selected = false
				};
				selectListItems2.Add(selectListItem1);
			}
			this.ViewData["Timezones"] = selectListItems2.AsEnumerable<SelectListItem>();
			List<SelectListItem> selectListItems3 = new List<SelectListItem>();
			tenantId = this.AbpSession.TenantId;
			LookupFill lookupFill = new LookupFill("PaymentTerms", tenantId.Value);
			IRepository<PaymentSetting, long> repository = this._paymentSettingRepository;
			List<PaymentSetting> allListAsync = await repository.GetAllListAsync((PaymentSetting x) => (int?)x.TenantId == this.AbpSession.TenantId);
			List<PaymentSetting> paymentSettings = allListAsync;
			if (paymentSettings.Any<PaymentSetting>())
			{
				foreach (PaymentSetting paymentSetting in paymentSettings)
				{
					ICollection<Lookup> lookupItems = lookupFill.LookupItems;
					string str1 = paymentSetting.Setting.ToString();
					tenantId = this.AbpSession.ImpersonatorTenantId;
					if (tenantId.HasValue)
					{
						tenantId = this.AbpSession.ImpersonatorTenantId;
						value = tenantId.Value;
					}
					else
					{
						tenantId = this.AbpSession.TenantId;
						value = tenantId.Value;
					}
					lookupItems.Add(LookupFill.CreateLookupFromString(str1, value));
				}
			}
			ICollection<Lookup> lookups = lookupFill.LookupItems;
			foreach (Lookup lookup in 
				from x in lookups
				orderby x.Text
				select x)
			{
				List<SelectListItem> selectListItems4 = selectListItems3;
				SelectListItem selectListItem2 = new SelectListItem()
				{
					Text = lookup.Text,
					Value = lookup.Value,
					Disabled = lookup.Disabled,
					Selected = lookup.Selected
				};
				selectListItems4.Add(selectListItem2);
			}
			this.ViewData["PaymentTerms"] = selectListItems3;
			return this.View(allSettings);
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<bool> SaveNewPaymentTerm(int invoiceDiscountOffer, int invoiceDiscountDaysOffer, int invoiceDiscountDueDate, int tenantId)
		{
			string str;
			bool flag = true;
			str = (invoiceDiscountOffer > 0 || invoiceDiscountDaysOffer > 0 ? "{0}% {1}, Net {2}" : "Net {0}");
			string str1 = str;
			str1 = (!str1.Contains(",") ? string.Format(str1, invoiceDiscountDueDate) : string.Format(str1, invoiceDiscountOffer, invoiceDiscountDaysOffer, invoiceDiscountDueDate));
			PaymentSetting paymentSetting = new PaymentSetting()
			{
				TenantId = tenantId,
				Setting = str1
			};
			await this._paymentSettingRepository.InsertAsync(paymentSetting);
			return flag;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Administration.Tenant.Settings" })]
		[UnitOfWork]
		public virtual async Task<JsonResult> UpdateTenantLogos(TenantLogosUploadModel model)
		{
			JsonResult jsonResult;
			Guid? headerImageId;
			BinaryObject binaryObject;
			BinaryObject binaryObject1;
			BinaryObject binaryObject2;
			BinaryObject binaryObject3;
			try
			{
				bool flag = false;
				ITenantSettingsAppService tenantSettingsAppService = this._tenantSettingsAppService;
				int? tenantId = this.AbpSession.TenantId;
				TenantLogosEditDto tenantLogos = await tenantSettingsAppService.GetTenantLogos(tenantId.Value);
				if (this.Request.Files.Count > 0)
				{
					foreach (object key in this.Request.Files.Keys)
					{
						HttpPostedFileBase item = this.Request.Files[key.ToString()];
						if (item.ContentLength > 512000)
						{
							throw new UserFriendlyException(this.L("TenantCompanyLogo_Warn_SizeLimit"));
						}
						string str = key.ToString();
						if (str == "HeaderImage")
						{
							headerImageId = tenantLogos.HeaderImageId;
							if (headerImageId.HasValue)
							{
								headerImageId = tenantLogos.HeaderImageId;
								if (headerImageId.Value != Guid.Empty)
								{
									IBinaryObjectManager binaryObjectManager = this._binaryObjectManager;
									headerImageId = tenantLogos.HeaderImageId;
									await binaryObjectManager.DeleteAsync(headerImageId.Value);
								}
							}
							binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
							await this._binaryObjectManager.SaveAsync(binaryObject);
							tenantLogos.HeaderImageId = new Guid?(binaryObject.Id);
							flag = true;
						}
						else if (str == "HeaderMobileImage")
						{
							headerImageId = tenantLogos.HeaderMobileImageId;
							if (headerImageId.HasValue)
							{
								headerImageId = tenantLogos.HeaderMobileImageId;
								if (headerImageId.Value != Guid.Empty)
								{
									IBinaryObjectManager binaryObjectManager1 = this._binaryObjectManager;
									headerImageId = tenantLogos.HeaderMobileImageId;
									await binaryObjectManager1.DeleteAsync(headerImageId.Value);
								}
							}
							binaryObject1 = new BinaryObject(item.InputStream.GetAllBytes());
							await this._binaryObjectManager.SaveAsync(binaryObject1);
							tenantLogos.HeaderMobileImageId = new Guid?(binaryObject1.Id);
							flag = true;
						}
						else if (str == "MailImage")
						{
							headerImageId = tenantLogos.MailImageId;
							if (headerImageId.HasValue)
							{
								headerImageId = tenantLogos.MailImageId;
								if (headerImageId.Value != Guid.Empty)
								{
									IBinaryObjectManager binaryObjectManager2 = this._binaryObjectManager;
									headerImageId = tenantLogos.MailImageId;
									await binaryObjectManager2.DeleteAsync(headerImageId.Value);
								}
							}
							binaryObject2 = new BinaryObject(item.InputStream.GetAllBytes());
							await this._binaryObjectManager.SaveAsync(binaryObject2);
							tenantLogos.MailImageId = new Guid?(binaryObject2.Id);
							flag = true;
						}
						else if (str == "InvoiceImage")
						{
							headerImageId = tenantLogos.InvoiceImageId;
							if (headerImageId.HasValue)
							{
								headerImageId = tenantLogos.InvoiceImageId;
								if (headerImageId.Value != Guid.Empty)
								{
									IBinaryObjectManager binaryObjectManager3 = this._binaryObjectManager;
									headerImageId = tenantLogos.InvoiceImageId;
									await binaryObjectManager3.DeleteAsync(headerImageId.Value);
								}
							}
							binaryObject3 = new BinaryObject(item.InputStream.GetAllBytes());
							await this._binaryObjectManager.SaveAsync(binaryObject3);
							tenantLogos.InvoiceImageId = new Guid?(binaryObject3.Id);
							flag = true;
						}
						binaryObject = null;
						binaryObject1 = null;
						binaryObject2 = null;
						binaryObject3 = null;
						item = null;
					}
				}
				if (model.ClearHeaderImageId.HasValue && model.ClearHeaderImageId.Value)
				{
					headerImageId = model.HeaderImageId;
					if (headerImageId.HasValue)
					{
						IBinaryObjectManager binaryObjectManager4 = this._binaryObjectManager;
						headerImageId = tenantLogos.HeaderImageId;
						await binaryObjectManager4.DeleteAsync(headerImageId.Value);
						headerImageId = null;
						tenantLogos.HeaderImageId = headerImageId;
						flag = true;
					}
				}
				if (model.ClearHeaderMobileImageId.HasValue && model.ClearHeaderMobileImageId.Value)
				{
					headerImageId = model.HeaderMobileImageId;
					if (headerImageId.HasValue)
					{
						IBinaryObjectManager binaryObjectManager5 = this._binaryObjectManager;
						headerImageId = tenantLogos.HeaderMobileImageId;
						await binaryObjectManager5.DeleteAsync(headerImageId.Value);
						headerImageId = null;
						tenantLogos.HeaderMobileImageId = headerImageId;
						flag = true;
					}
				}
				if (model.ClearMailImageId.HasValue && model.ClearMailImageId.Value)
				{
					headerImageId = model.MailImageId;
					if (headerImageId.HasValue)
					{
						IBinaryObjectManager binaryObjectManager6 = this._binaryObjectManager;
						headerImageId = tenantLogos.MailImageId;
						await binaryObjectManager6.DeleteAsync(headerImageId.Value);
						headerImageId = null;
						tenantLogos.MailImageId = headerImageId;
						flag = true;
					}
				}
				if (model.ClearInvoiceImageId.HasValue && model.ClearInvoiceImageId.Value)
				{
					headerImageId = model.InvoiceImageId;
					if (headerImageId.HasValue)
					{
						IBinaryObjectManager binaryObjectManager7 = this._binaryObjectManager;
						headerImageId = tenantLogos.InvoiceImageId;
						await binaryObjectManager7.DeleteAsync(headerImageId.Value);
						headerImageId = null;
						tenantLogos.InvoiceImageId = headerImageId;
						flag = true;
					}
				}
				if (flag)
				{
					await this._tenantSettingsAppService.UpdateTenantLogos(tenantLogos);
				}
				jsonResult = this.Json(new MvcAjaxResponse());
			}
			catch (UserFriendlyException userFriendlyException1)
			{
				UserFriendlyException userFriendlyException = userFriendlyException1;
				jsonResult = this.Json(new MvcAjaxResponse(new ErrorInfo(userFriendlyException.Message), false));
			}
			return jsonResult;
		}
	}
}