using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative.Taxes;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Administrative;
using FuelWerx.Web.Controllers;
using FuelWerx.Web.Models.Map;
using Microsoft.AspNet.Identity;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
	public class GenericController : FuelWerxControllerBase
	{
		public IAbpSession _abpSession;

		private readonly IGenericAppService _genericAppService;

		private readonly UserManager _userManager;

		private readonly IRepository<Role> _roleRepository;

		public GenericController(IGenericAppService genericAppService, ITaxAppService taxAppService, IRepository<Role> roleRepository, UserManager userManager)
		{
			this._abpSession = NullAbpSession.Instance;
			this._genericAppService = genericAppService;
			this._userManager = userManager;
			this._roleRepository = roleRepository;
		}

		public async Task<PartialViewResult> CreateOrUpdateAddressModal(long? ownerId, string ownerType, int tenantId, long? id = null)
		{
			long? impersonatorUserId;
			string str;
			long value;
			long num;
			IGenericAppService genericAppService = this._genericAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetAddressForEditOutput addressForEdit = await genericAppService.GetAddressForEdit(nullableIdInput);
			if (string.IsNullOrEmpty(addressForEdit.Address.OwnerType))
			{
				addressForEdit.Address.OwnerType = ownerType;
				AddressEditDto address = addressForEdit.Address;
				num = (ownerId.HasValue ? ownerId.Value : (long)0);
				address.OwnerId = num;
			}
			CreateOrUpdateAddressModalViewModel createOrUpdateAddressModalViewModel = new CreateOrUpdateAddressModalViewModel(addressForEdit);
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			str = (ownerType == "FillLot" ? "FillLotAddressTypes" : "AddressTypes");
			foreach (Lookup lookupItem in (new LookupFill(str, tenantId)).LookupItems)
			{
				SelectListItem selectListItem = new SelectListItem()
				{
					Text = lookupItem.Text,
					Value = lookupItem.Value,
					Disabled = lookupItem.Disabled,
					Selected = lookupItem.Selected
				};
				selectListItems.Add(selectListItem);
			}
			SelectListItem selectListItem1 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems.Insert(0, selectListItem1);
			this.ViewData["AddressTypes"] = selectListItems;
			List<SelectListItem> selectListItems1 = new List<SelectListItem>();
			using (HttpClient httpClient = new HttpClient())
			{
				string str1 = this.Url.RouteUrl("DefaultApiWithAction", new { httproute = "", controller = "Generic", action = "GetCountriesAsSelectListItems", countryId = 0, selectedCountryId = createOrUpdateAddressModalViewModel.Address.CountryId }, this.Request.Url.Scheme);
				using (HttpResponseMessage async = await httpClient.GetAsync(str1))
				{
					if (async.IsSuccessStatusCode)
					{
						string str2 = await async.Content.ReadAsStringAsync();
						selectListItems1 = JsonConvert.DeserializeObject<List<SelectListItem>>(str2);
					}
				}
			}
			List<SelectListItem> selectListItems2 = selectListItems1;
			SelectListItem selectListItem2 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems2.Insert(0, selectListItem2);
			this.ViewData["Countries"] = selectListItems1.AsEnumerable<SelectListItem>();
			string str3 = this.L("KeyName_CustomersRole");
			IRepository<Role> repository = this._roleRepository;
			List<Role> allListAsync = await repository.GetAllListAsync((Role m) => m.TenantId == (int?)((this.AbpSession.ImpersonatorTenantId.HasValue ? this.AbpSession.ImpersonatorTenantId.Value : this.AbpSession.TenantId.Value)) && m.DisplayName == str3);
			List<Role> roles = allListAsync;
			bool count = roles.Count == 1;
			if (count)
			{
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
				bool flag = await userManager.IsInRoleAsync(value, roles[0].Name);
				count = flag;
			}
			if (count)
			{
				((dynamic)this.ViewBag).IsCustomer = true;
			}
			return this.PartialView("_CreateOrUpdateAddressModal", createOrUpdateAddressModalViewModel);
		}

		public async Task<PartialViewResult> CreateOrUpdateBankModal(long? ownerId, string ownerType, int tenantId, long? id = null)
		{
			long num;
			IGenericAppService genericAppService = this._genericAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetBankForEditOutput bankForEdit = await genericAppService.GetBankForEdit(nullableIdInput);
			if (string.IsNullOrEmpty(bankForEdit.Bank.OwnerType))
			{
				bankForEdit.Bank.OwnerType = ownerType;
				BankEditDto bank = bankForEdit.Bank;
				num = (ownerId.HasValue ? ownerId.Value : (long)0);
				bank.OwnerId = num;
			}
			CreateOrUpdateBankModalViewModel createOrUpdateBankModalViewModel = new CreateOrUpdateBankModalViewModel(bankForEdit);
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (Lookup lookupItem in (new LookupFill("BankTypes", tenantId)).LookupItems)
			{
				SelectListItem selectListItem = new SelectListItem()
				{
					Text = lookupItem.Text,
					Value = lookupItem.Value,
					Disabled = lookupItem.Disabled,
					Selected = lookupItem.Selected
				};
				selectListItems.Add(selectListItem);
			}
			SelectListItem selectListItem1 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems.Insert(0, selectListItem1);
			this.ViewData["BankTypes"] = selectListItems;
			return this.PartialView("_CreateOrUpdateBankModal", createOrUpdateBankModalViewModel);
		}

		public async Task<PartialViewResult> CreateOrUpdateDriversDataModal(long? driversId)
		{
			GetDriversDataForEditOutput driversDataForEdit = await this._genericAppService.GetDriversDataForEdit(long.Parse(driversId.ToString()));
			return this.PartialView("_CreateOrUpdateDriversDataModal", new CreateOrUpdateDriversDataModalViewModel(driversDataForEdit));
		}

		public async Task<PartialViewResult> CreateOrUpdatePhoneModal(long? ownerId, string ownerType, int tenantId, long? id = null)
		{
			long num;
			IGenericAppService genericAppService = this._genericAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetPhoneForEditOutput phoneForEdit = await genericAppService.GetPhoneForEdit(nullableIdInput);
			if (string.IsNullOrEmpty(phoneForEdit.Phone.OwnerType))
			{
				phoneForEdit.Phone.OwnerType = ownerType;
				PhoneEditDto phone = phoneForEdit.Phone;
				num = (ownerId.HasValue ? ownerId.Value : (long)0);
				phone.OwnerId = num;
			}
			CreateOrUpdatePhoneModalViewModel createOrUpdatePhoneModalViewModel = new CreateOrUpdatePhoneModalViewModel(phoneForEdit);
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (Lookup lookupItem in (new LookupFill("PhoneTypes", tenantId)).LookupItems)
			{
				SelectListItem selectListItem = new SelectListItem()
				{
					Text = lookupItem.Text,
					Value = lookupItem.Value,
					Disabled = lookupItem.Disabled,
					Selected = lookupItem.Selected
				};
				selectListItems.Add(selectListItem);
			}
			SelectListItem selectListItem1 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems.Insert(0, selectListItem1);
			this.ViewData["PhoneTypes"] = selectListItems;
			return this.PartialView("_CreateOrUpdatePhoneModal", createOrUpdatePhoneModalViewModel);
		}

		public async Task<PartialViewResult> CreateOrUpdateServiceModal(long addressId, long? ownerId, string ownerType, int tenantId, long? id = null)
		{
			long num;
			IGenericAppService genericAppService = this._genericAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetServiceForEditOutput serviceForEdit = await genericAppService.GetServiceForEdit(nullableIdInput);
			if (string.IsNullOrEmpty(serviceForEdit.Service.OwnerType))
			{
				serviceForEdit.Service.OwnerType = ownerType;
				ServiceEditDto service = serviceForEdit.Service;
				num = (ownerId.HasValue ? ownerId.Value : (long)0);
				service.OwnerId = num;
				serviceForEdit.Service.AddressId = addressId;
			}
			CreateOrUpdateServiceModalViewModel createOrUpdateServiceModalViewModel = new CreateOrUpdateServiceModalViewModel(serviceForEdit);
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			LookupFill lookupFill = new LookupFill("ServiceTypes", tenantId);
			foreach (Lookup lookupItem in lookupFill.LookupItems)
			{
				SelectListItem selectListItem = new SelectListItem()
				{
					Text = lookupItem.Text,
					Value = lookupItem.Value,
					Disabled = lookupItem.Disabled,
					Selected = lookupItem.Selected
				};
				selectListItems.Add(selectListItem);
			}
			SelectListItem selectListItem1 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems.Insert(0, selectListItem1);
			this.ViewData["ServiceTypes"] = selectListItems;
			List<SelectListItem> selectListItems1 = new List<SelectListItem>();
			lookupFill = null;
			lookupFill = new LookupFill("RequestedServices", tenantId);
			foreach (Lookup lookup in lookupFill.LookupItems)
			{
				SelectListItem selectListItem2 = new SelectListItem()
				{
					Text = lookup.Text,
					Value = lookup.Value,
					Disabled = lookup.Disabled,
					Selected = lookup.Selected
				};
				selectListItems1.Add(selectListItem2);
			}
			SelectListItem selectListItem3 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems1.Insert(0, selectListItem3);
			this.ViewData["RequestedServices"] = selectListItems1.AsEnumerable<SelectListItem>();
			return this.PartialView("_CreateOrUpdateServiceModal", createOrUpdateServiceModalViewModel);
		}

		public PartialViewResult MapViewModal(double? longitude, double? latitude, string label)
		{
			return this.PartialView("_MapViewModal", new MapView()
			{
				Longitude = longitude,
				Latitude = latitude,
				Label = label
			});
		}
	}
}