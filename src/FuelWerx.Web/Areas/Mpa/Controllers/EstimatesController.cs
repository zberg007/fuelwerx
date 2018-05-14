using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative.Titles;
using FuelWerx.Customers;
using FuelWerx.Estimates;
using FuelWerx.Estimates.Dto;
using FuelWerx.Generic;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using FuelWerx.Storage;
using FuelWerx.Suppliers;
using FuelWerx.Tenants;
using FuelWerx.Web.Areas.Mpa.Models.Estimates;
using FuelWerx.Web.Controllers;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuelWerx.Web.Areas.Mpa.Controllers
{
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.Estimates" })]
	public class EstimatesController : FuelWerxControllerBase
	{
		private readonly IEstimateAppService _estimateAppService;

		private readonly ISupplierAppService _supplierAppService;

		private readonly ICustomerAppService _customerAppServer;

		private readonly IProductAppService _productAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly ITitleAppService _titleAppService;

		private readonly IRepository<PaymentSetting, long> _paymentSettingRepository;

		private readonly IRepository<TenantPaymentSettings, long> _tenantPaymentSettingsRepository;

		public EstimatesController(IEstimateAppService estimateAppService, ISupplierAppService supplierAppService, ICustomerAppService customerAppServer, IBinaryObjectManager binaryObjectManager, IProductAppService productAppService, ITitleAppService titleAppService, IRepository<PaymentSetting, long> paymentSettingRepository, IRepository<TenantPaymentSettings, long> tenantPaymentSettingsRepository)
		{
			this._estimateAppService = estimateAppService;
			this._supplierAppService = supplierAppService;
			this._customerAppServer = customerAppServer;
			this._binaryObjectManager = binaryObjectManager;
			this._titleAppService = titleAppService;
			this._tenantPaymentSettingsRepository = tenantPaymentSettingsRepository;
			this._productAppService = productAppService;
			this._paymentSettingRepository = paymentSettingRepository;
		}

		public async Task<PartialViewResult> CopyModal(long estimateId)
		{
			IEstimateAppService estimateAppService = this._estimateAppService;
			IdInput<long> idInput = new IdInput<long>()
			{
				Id = estimateId
			};
			CopyEstimateModalViewModel copyEstimateModalViewModel = new CopyEstimateModalViewModel(await estimateAppService.GetEstimateForCopy(idInput));
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (Customer customersForBusiness in await this._customerAppServer.GetCustomersForBusiness())
			{
				if (customersForBusiness.Id == copyEstimateModalViewModel.Estimate.CustomerId)
				{
					continue;
				}
				List<SelectListItem> selectListItems1 = selectListItems;
				SelectListItem selectListItem = new SelectListItem()
				{
					Text = string.Concat(customersForBusiness.LastName, ", ", customersForBusiness.FirstName),
					Value = customersForBusiness.Id.ToString(),
					Disabled = false,
					Selected = false
				};
				selectListItems1.Add(selectListItem);
			}
			List<SelectListItem> selectListItems2 = selectListItems;
			SelectListItem selectListItem1 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems2.Insert(0, selectListItem1);
			this.ViewData["Customers"] = selectListItems;
			return this.PartialView("_CopyModal", copyEstimateModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Estimates.Create", "Pages.Tenant.Estimates.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			int? impersonatorTenantId;
			int value;
			if (this.AbpSession.ImpersonatorTenantId.HasValue)
			{
				impersonatorTenantId = this.AbpSession.ImpersonatorTenantId;
				value = impersonatorTenantId.Value;
			}
			else
			{
				impersonatorTenantId = this.AbpSession.TenantId;
				value = impersonatorTenantId.Value;
			}
			int num = value;
			IEstimateAppService estimateAppService = this._estimateAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateEstimateModalViewModel createOrUpdateEstimateModalViewModel = new CreateOrUpdateEstimateModalViewModel(await estimateAppService.GetEstimateForEdit(nullableIdInput));
			IRepository<TenantPaymentSettings, long> repository = this._tenantPaymentSettingsRepository;
			List<TenantPaymentSettings> allListAsync = await repository.GetAllListAsync((TenantPaymentSettings x) => x.TenantId == num);
			List<TenantPaymentSettings> tenantPaymentSettings = allListAsync;
			if (tenantPaymentSettings.Count == 1 && !string.IsNullOrEmpty(tenantPaymentSettings[0].DefaultPaymentTerm) && tenantPaymentSettings[0].DefaultPaymentTerm.Length > 0)
			{
				this.Session["DefaultTermType"] = tenantPaymentSettings[0].DefaultPaymentTerm;
			}
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			ListResultDto<ProductListDto> productsByTenantId = await this._productAppService.GetProductsByTenantId(num, true, "");
			ListResultDto<ProductListDto> listResultDto = productsByTenantId;
			if (!listResultDto.Items.Any<ProductListDto>())
			{
				this.ViewData["Products"] = null;
			}
			else
			{
				foreach (ProductListDto item in listResultDto.Items)
				{
					List<SelectListItem> selectListItems1 = selectListItems;
					SelectListItem selectListItem = new SelectListItem()
					{
						Text = item.Name,
						Value = item.Id.ToString(),
						Selected = false
					};
					selectListItems1.Add(selectListItem);
				}
				this.ViewData["Products"] = selectListItems.AsEnumerable<SelectListItem>();
			}
			List<SelectListItem> selectListItems2 = new List<SelectListItem>();
			foreach (Customer customersForBusiness in await this._customerAppServer.GetCustomersForBusiness())
			{
				List<SelectListItem> selectListItems3 = selectListItems2;
				SelectListItem selectListItem1 = new SelectListItem()
				{
					Text = string.Concat(customersForBusiness.LastName, ", ", customersForBusiness.FirstName),
					Value = customersForBusiness.Id.ToString(),
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
			this.ViewData["Customers"] = selectListItems2;
			List<SelectListItem> selectListItems5 = new List<SelectListItem>();
			foreach (Lookup lookupItem in (new LookupFill("BillingTypes", num)).LookupItems)
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
			this.ViewData["BillingTypes"] = selectListItems5;
			List<SelectListItem> selectListItems6 = new List<SelectListItem>();
			LookupFill lookupFill = new LookupFill("PaymentTerms", num);
			IRepository<PaymentSetting, long> repository1 = this._paymentSettingRepository;
			List<PaymentSetting> paymentSettings = await repository1.GetAllListAsync((PaymentSetting x) => x.TenantId == num);
			List<PaymentSetting> paymentSettings1 = paymentSettings;
			if (paymentSettings1.Any<PaymentSetting>())
			{
				foreach (PaymentSetting paymentSetting in paymentSettings1)
				{
					lookupFill.LookupItems.Add(LookupFill.CreateLookupFromString(paymentSetting.Setting.ToString(), num));
				}
			}
			ICollection<Lookup> lookupItems = lookupFill.LookupItems;
			foreach (Lookup lookup in 
				from x in lookupItems
				orderby x.Text
				select x)
			{
				if (lookup.Text == "< Add New >")
				{
					continue;
				}
				List<SelectListItem> selectListItems7 = selectListItems6;
				SelectListItem selectListItem5 = new SelectListItem()
				{
					Text = lookup.Text,
					Value = lookup.Value,
					Disabled = lookup.Disabled,
					Selected = lookup.Selected
				};
				selectListItems7.Add(selectListItem5);
			}
			this.ViewData["TermTypes"] = selectListItems6;
			return this.PartialView("_CreateOrUpdateModal", createOrUpdateEstimateModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Estimates.Create", "Pages.Tenant.Estimates.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateResourcesModal(long? estimateId = null, bool? reloadPartial = null)
		{
			PartialViewResult partialViewResult;
			IEstimateAppService estimateAppService = this._estimateAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = estimateId
			};
			GetEstimateResourceForEditOutput estimateResourcesForEdit = await estimateAppService.GetEstimateResourcesForEdit(nullableIdInput);
			CreateOrUpdateEstimateResourcesModalViewModel createOrUpdateEstimateResourcesModalViewModel = new CreateOrUpdateEstimateResourcesModalViewModel(estimateResourcesForEdit)
			{
				EstimateId = estimateId.Value
			};
			Estimate estimate = await this._estimateAppService.GetEstimate(estimateId.Value);
			((dynamic)this.ViewBag).EstimateName = estimate.Label;
			partialViewResult = (!reloadPartial.HasValue || !bool.Parse(reloadPartial.ToString()) ? this.PartialView("_CreateOrUpdateResourcesModal", createOrUpdateEstimateResourcesModalViewModel) : this.PartialView("_ListResourcesView", createOrUpdateEstimateResourcesModalViewModel));
			return partialViewResult;
		}

		[HttpPost]
		public async Task DeleteEstimateResource(long id)
		{
			IdInput<long> idInput = new IdInput<long>()
			{
				Id = id
			};
			await this._estimateAppService.DeleteEstimateResource(idInput);
		}

		private FileResult GetDefaultEstimateResourceImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-estimate-resource-image.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetEstimateResourceById(Guid? resourceId, string fileExt)
		{
			FileResult defaultEstimateResourceImage;
			Guid guid;
			if (!resourceId.HasValue || !Guid.TryParse(resourceId.ToString(), out guid))
			{
				defaultEstimateResourceImage = this.GetDefaultEstimateResourceImage();
			}
			else
			{
				defaultEstimateResourceImage = await this.GetEstimateResourceById(guid, fileExt);
			}
			return defaultEstimateResourceImage;
		}

		private async Task<FileResult> GetEstimateResourceById(Guid resourceId, string fileExt)
		{
			FileResult defaultEstimateResourceImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(resourceId);
			if (orNullAsync != null)
			{
				Guid guid = Guid.NewGuid();
				string fileName = string.Concat(guid.ToString(), fileExt);
				EstimateResourceEditDto estimateResourceDetailsByBinaryObjectId = await this._estimateAppService.GetEstimateResourceDetailsByBinaryObjectId(resourceId);
				if (estimateResourceDetailsByBinaryObjectId != null && estimateResourceDetailsByBinaryObjectId.FileName.Length > 0)
				{
					fileName = estimateResourceDetailsByBinaryObjectId.FileName;
				}
				string str = "image/jpeg";
				string str1 = fileExt;
				if (str1 == ".xls" || str1 == ".xlsx")
				{
					str = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				}
				else if (str1 == ".doc" || str1 == ".docx")
				{
					str = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
				}
				else if (str1 == ".png")
				{
					str = "image/png";
				}
				else if (str1 == ".pdf")
				{
					str = "application/pdf";
				}
				else if (str1 == ".zip")
				{
					str = "application/zip";
				}
				defaultEstimateResourceImage = this.File(orNullAsync.Bytes, str, fileName);
			}
			else
			{
				defaultEstimateResourceImage = this.GetDefaultEstimateResourceImage();
			}
			return defaultEstimateResourceImage;
		}

		public ActionResult Index(GetEstimatesInput input)
		{
			return base.View();
		}

		[HttpPost]
		public async Task SaveEstimateResourceDetails(SaveEstimateResourceDetailsInput input)
		{
			await this._estimateAppService.SaveEstimateResourceDetails(input.Id, input.Name, input.Description, input.IsActive);
		}

		[UnitOfWork]
		public virtual async Task<HttpResponseMessage> UploadResources(HttpRequestMessage request, long estimateId)
		{
			HttpResponseMessage httpResponseMessage;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("EstimateResource_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength <= 2097152)
				{
					Estimate estimate = await this._estimateAppService.GetEstimate(estimateId);
					BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
					await this._binaryObjectManager.SaveAsync(binaryObject);
					UpdateEstimateResourceInput updateEstimateResourceInput = new UpdateEstimateResourceInput()
					{
						Id = (long)0,
						EstimateId = estimate.Id,
						ResourceId = new Guid?(binaryObject.Id),
						FileName = item.FileName,
						FileExtension = Path.GetExtension(item.FileName),
						FileSize = item.ContentLength.ToString(),
						IsActive = true
					};
					await this._estimateAppService.SaveEstimateResourceAsync(updateEstimateResourceInput);
					httpResponseMessage = request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					HttpResponseMessage httpResponseMessage1 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("EstimateResource_Warn_SizeLimit"));
					httpResponseMessage = httpResponseMessage1;
				}
			}
			catch (UserFriendlyException)
			{
				HttpResponseMessage httpResponseMessage2 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("EstimateResource_UploadException"));
				httpResponseMessage = httpResponseMessage2;
			}
			return httpResponseMessage;
		}

		public ActionResult ViewResource(Guid? resourceId, string fileExt)
		{
			return base.View("ViewResource", new ViewResourceViewModel()
			{
				BinaryObjectId = resourceId,
				FileExtension = fileExt
			});
		}
	}
}