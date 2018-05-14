using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using FuelWerx.Administrative.Titles;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Customers;
using FuelWerx.Generic;
using FuelWerx.Invoices;
using FuelWerx.Invoices.Dto;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using FuelWerx.Storage;
using FuelWerx.Suppliers;
using FuelWerx.Tenants;
using FuelWerx.Web.Areas.Mpa.Models.Invoices;
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
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.Invoices", "Rights.Customer.CanViewPayments" })]
	public class InvoicesController : FuelWerxControllerBase
	{
		private readonly IInvoiceAppService _invoiceAppService;

		private readonly ISupplierAppService _supplierAppService;

		private readonly ICustomerAppService _customerAppServer;

		private readonly IProductAppService _productAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly ITitleAppService _titleAppService;

		private readonly IRepository<PaymentSetting, long> _paymentSettingRepository;

		private readonly IRepository<TenantPaymentSettings, long> _tenantPaymentSettingsRepository;

		public InvoicesController(IInvoiceAppService invoiceAppService, ISupplierAppService supplierAppService, ICustomerAppService customerAppServer, IBinaryObjectManager binaryObjectManager, IProductAppService productAppService, ITitleAppService titleAppService, IRepository<PaymentSetting, long> paymentSettingRepository, IRepository<TenantPaymentSettings, long> tenantPaymentSettingsRepository)
		{
			this._invoiceAppService = invoiceAppService;
			this._supplierAppService = supplierAppService;
			this._customerAppServer = customerAppServer;
			this._binaryObjectManager = binaryObjectManager;
			this._titleAppService = titleAppService;
			this._tenantPaymentSettingsRepository = tenantPaymentSettingsRepository;
			this._productAppService = productAppService;
			this._paymentSettingRepository = paymentSettingRepository;
		}

		public async Task<PartialViewResult> CopyModal(long invoiceId)
		{
			IInvoiceAppService invoiceAppService = this._invoiceAppService;
			IdInput<long> idInput = new IdInput<long>()
			{
				Id = invoiceId
			};
			CopyInvoiceModalViewModel copyInvoiceModalViewModel = new CopyInvoiceModalViewModel(await invoiceAppService.GetInvoiceForCopy(idInput));
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (Customer customersForBusiness in await this._customerAppServer.GetCustomersForBusiness())
			{
				if (customersForBusiness.Id == copyInvoiceModalViewModel.Invoice.CustomerId)
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
			return this.PartialView("_CopyModal", copyInvoiceModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Invoices.Create", "Pages.Tenant.Invoices.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			int? impersonatorTenantId;
			int value;
			bool flag;
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
			IInvoiceAppService invoiceAppService = this._invoiceAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateInvoiceModalViewModel createOrUpdateInvoiceModalViewModel = new CreateOrUpdateInvoiceModalViewModel(await invoiceAppService.GetInvoiceForEdit(nullableIdInput));
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
			((dynamic)this.ViewBag).InvoicePaidTotal = new decimal(0);
			if (createOrUpdateInvoiceModalViewModel.Invoice.Id.HasValue)
			{
				IInvoiceAppService invoiceAppService1 = this._invoiceAppService;
				long? nullable = createOrUpdateInvoiceModalViewModel.Invoice.Id;
				List<InvoicePaymentListDto> invoicePaymentsByInvoiceId = await invoiceAppService1.GetInvoicePaymentsByInvoiceId(nullable.Value);
				dynamic viewBag = this.ViewBag;
				flag = (invoicePaymentsByInvoiceId.Any<InvoicePaymentListDto>() ? true : false);
				viewBag.InvoiceHasPayments = flag;
				if (invoicePaymentsByInvoiceId.Any<InvoicePaymentListDto>())
				{
					dynamic obj = this.ViewBag;
					List<InvoicePaymentListDto> invoicePaymentListDtos = invoicePaymentsByInvoiceId;
					obj.InvoicePaidTotal = invoicePaymentListDtos.Sum<InvoicePaymentListDto>((InvoicePaymentListDto s) => s.DollarAmount);
				}
			}
			return this.PartialView("_CreateOrUpdateModal", createOrUpdateInvoiceModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Invoices.Create", "Pages.Tenant.Invoices.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateResourcesModal(long? invoiceId = null, bool? reloadPartial = null)
		{
			PartialViewResult partialViewResult;
			IInvoiceAppService invoiceAppService = this._invoiceAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = invoiceId
			};
			GetInvoiceResourceForEditOutput invoiceResourcesForEdit = await invoiceAppService.GetInvoiceResourcesForEdit(nullableIdInput);
			CreateOrUpdateInvoiceResourcesModalViewModel createOrUpdateInvoiceResourcesModalViewModel = new CreateOrUpdateInvoiceResourcesModalViewModel(invoiceResourcesForEdit)
			{
				InvoiceId = invoiceId.Value
			};
			Invoice invoice = await this._invoiceAppService.GetInvoice(invoiceId.Value);
			((dynamic)this.ViewBag).InvoiceName = invoice.Label;
			partialViewResult = (!reloadPartial.HasValue || !bool.Parse(reloadPartial.ToString()) ? this.PartialView("_CreateOrUpdateResourcesModal", createOrUpdateInvoiceResourcesModalViewModel) : this.PartialView("_ListResourcesView", createOrUpdateInvoiceResourcesModalViewModel));
			return partialViewResult;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Invoices.Create", "Pages.Tenant.Invoices.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateTeamMembersModal(long? id = null)
		{
			int? impersonatorTenantId;
			int value;
			string str;
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
			IInvoiceAppService invoiceAppService = this._invoiceAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateInvoiceTeamMembersModalViewModel createOrUpdateInvoiceTeamMembersModalViewModel = new CreateOrUpdateInvoiceTeamMembersModalViewModel(await invoiceAppService.GetInvoiceTeamMembersForEdit(nullableIdInput));
			CreateOrUpdateInvoiceTeamMembersModalViewModel createOrUpdateInvoiceTeamMembersModalViewModel1 = createOrUpdateInvoiceTeamMembersModalViewModel;
			long value1 = id.Value;
			createOrUpdateInvoiceTeamMembersModalViewModel1.InvoiceId = long.Parse(value1.ToString());
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			ListResultDto<UserListDto> teamMembersByTenantId = await this._invoiceAppService.GetTeamMembersByTenantId(num, true);
			if (!teamMembersByTenantId.Items.Any<UserListDto>())
			{
				this.ViewData["TeamMembers"] = null;
			}
			else
			{
				foreach (UserListDto item in teamMembersByTenantId.Items)
				{
					string name = item.Name;
					str = (item.Name.Length > 0 || item.Surname.Length > 0 ? " " : "");
					string str1 = string.Concat(name, str, item.Surname);
					List<SelectListItem> selectListItems1 = selectListItems;
					SelectListItem selectListItem = new SelectListItem()
					{
						Text = str1,
						Value = item.Id.ToString(),
						Selected = false
					};
					selectListItems1.Add(selectListItem);
				}
				this.ViewData["TeamMembers"] = selectListItems.AsEnumerable<SelectListItem>();
			}
			IInvoiceAppService invoiceAppService1 = this._invoiceAppService;
			value1 = id.Value;
			Invoice invoice = await invoiceAppService1.GetInvoice(long.Parse(value1.ToString()));
			((dynamic)this.ViewBag).InvoiceName = invoice.Label;
			return this.PartialView("_CreateOrUpdateTeamMembersModal", createOrUpdateInvoiceTeamMembersModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.InvoicePayments", "Rights.Customer.CanViewPayments" })]
		public async Task<PartialViewResult> CreateOrViewPaymentModal(long invoiceId, long? id = null, bool c = false)
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
			IInvoiceAppService invoiceAppService = this._invoiceAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrViewPaymentModalViewModel createOrViewPaymentModalViewModel = new CreateOrViewPaymentModalViewModel(await invoiceAppService.GetInvoicePaymentForCreateOrView(nullableIdInput));
			if (createOrViewPaymentModalViewModel.InvoicePayment.InvoiceId <= (long)0)
			{
				createOrViewPaymentModalViewModel.InvoicePayment.InvoiceId = invoiceId;
			}
			IInvoiceAppService invoiceAppService1 = this._invoiceAppService;
			long num = createOrViewPaymentModalViewModel.InvoicePayment.InvoiceId;
			Invoice invoice = await invoiceAppService1.GetInvoice(long.Parse(num.ToString()));
			Invoice invoice1 = invoice;
			createOrViewPaymentModalViewModel.InvoicePayment.Invoice = invoice1;
			Customer customerById = await this._customerAppServer.GetCustomerById(invoice1.CustomerId);
			createOrViewPaymentModalViewModel.InvoicePayment.Customer = customerById.MapTo<Customer>();
			if (c)
			{
				((dynamic)this.ViewBag).IsCustomer = true;
			}
			return this.PartialView("_CreateOrViewPaymentModal", createOrViewPaymentModalViewModel);
		}

		[HttpPost]
		public async Task DeleteInvoiceResource(long id)
		{
			IdInput<long> idInput = new IdInput<long>()
			{
				Id = id
			};
			await this._invoiceAppService.DeleteInvoiceResource(idInput);
		}

		private FileResult GetDefaultInvoiceResourceImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-invoice-resource-image.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetInvoiceResourceById(Guid? resourceId, string fileExt)
		{
			FileResult defaultInvoiceResourceImage;
			Guid guid;
			if (!resourceId.HasValue || !Guid.TryParse(resourceId.ToString(), out guid))
			{
				defaultInvoiceResourceImage = this.GetDefaultInvoiceResourceImage();
			}
			else
			{
				defaultInvoiceResourceImage = await this.GetInvoiceResourceById(guid, fileExt);
			}
			return defaultInvoiceResourceImage;
		}

		private async Task<FileResult> GetInvoiceResourceById(Guid resourceId, string fileExt)
		{
			FileResult defaultInvoiceResourceImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(resourceId);
			if (orNullAsync != null)
			{
				Guid guid = Guid.NewGuid();
				string fileName = string.Concat(guid.ToString(), fileExt);
				InvoiceResourceEditDto invoiceResourceDetailsByBinaryObjectId = await this._invoiceAppService.GetInvoiceResourceDetailsByBinaryObjectId(resourceId);
				if (invoiceResourceDetailsByBinaryObjectId != null && invoiceResourceDetailsByBinaryObjectId.FileName.Length > 0)
				{
					fileName = invoiceResourceDetailsByBinaryObjectId.FileName;
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
				defaultInvoiceResourceImage = this.File(orNullAsync.Bytes, str, fileName);
			}
			else
			{
				defaultInvoiceResourceImage = this.GetDefaultInvoiceResourceImage();
			}
			return defaultInvoiceResourceImage;
		}

		public ActionResult Index(GetInvoicesInput input)
		{
			return base.View();
		}

		[HttpPost]
		public async Task SaveInvoiceResourceDetails(SaveInvoiceResourceDetailsInput input)
		{
			await this._invoiceAppService.SaveInvoiceResourceDetails(input.Id, input.Name, input.Description, input.IsActive);
		}

		[UnitOfWork]
		public virtual async Task<HttpResponseMessage> UploadResources(HttpRequestMessage request, long invoiceId)
		{
			HttpResponseMessage httpResponseMessage;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("InvoiceResource_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength <= 2097152)
				{
					Invoice invoice = await this._invoiceAppService.GetInvoice(invoiceId);
					BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
					await this._binaryObjectManager.SaveAsync(binaryObject);
					UpdateInvoiceResourceInput updateInvoiceResourceInput = new UpdateInvoiceResourceInput()
					{
						Id = (long)0,
						InvoiceId = invoice.Id,
						ResourceId = new Guid?(binaryObject.Id),
						FileName = item.FileName,
						FileExtension = Path.GetExtension(item.FileName),
						FileSize = item.ContentLength.ToString(),
						IsActive = true
					};
					await this._invoiceAppService.SaveInvoiceResourceAsync(updateInvoiceResourceInput);
					httpResponseMessage = request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					HttpResponseMessage httpResponseMessage1 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("InvoiceResource_Warn_SizeLimit"));
					httpResponseMessage = httpResponseMessage1;
				}
			}
			catch (UserFriendlyException)
			{
				HttpResponseMessage httpResponseMessage2 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("InvoiceResource_UploadException"));
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