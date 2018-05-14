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
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Customers;
using FuelWerx.Generic;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using FuelWerx.Projects;
using FuelWerx.Projects.Dto;
using FuelWerx.Storage;
using FuelWerx.Suppliers;
using FuelWerx.Tenants;
using FuelWerx.Web.Areas.Mpa.Models.Projects;
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
	[AbpMvcAuthorize(new string[] { "Pages.Tenant.Projects" })]
	public class ProjectsController : FuelWerxControllerBase
	{
		private readonly IProjectAppService _projectAppService;

		private readonly ISupplierAppService _supplierAppService;

		private readonly ICustomerAppService _customerAppServer;

		private readonly IProductAppService _productAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly ITitleAppService _titleAppService;

		private readonly IRepository<PaymentSetting, long> _paymentSettingRepository;

		private readonly IRepository<TenantPaymentSettings, long> _tenantPaymentSettingsRepository;

		public ProjectsController(IProjectAppService projectAppService, ISupplierAppService supplierAppService, ICustomerAppService customerAppServer, IBinaryObjectManager binaryObjectManager, IProductAppService productAppService, ITitleAppService titleAppService, IRepository<PaymentSetting, long> paymentSettingRepository, IRepository<TenantPaymentSettings, long> tenantPaymentSettingsRepository)
		{
			this._projectAppService = projectAppService;
			this._supplierAppService = supplierAppService;
			this._customerAppServer = customerAppServer;
			this._binaryObjectManager = binaryObjectManager;
			this._titleAppService = titleAppService;
			this._tenantPaymentSettingsRepository = tenantPaymentSettingsRepository;
			this._productAppService = productAppService;
			this._paymentSettingRepository = paymentSettingRepository;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Projects.Create", "Pages.Tenant.Projects.Edit" })]
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
			IProjectAppService projectAppService = this._projectAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateProjectModalViewModel createOrUpdateProjectModalViewModel = new CreateOrUpdateProjectModalViewModel(await projectAppService.GetProjectForEdit(nullableIdInput));
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
			return this.PartialView("_CreateOrUpdateModal", createOrUpdateProjectModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Projects.Create", "Pages.Tenant.Projects.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateResourcesModal(long? projectId = null, bool? reloadPartial = null)
		{
			PartialViewResult partialViewResult;
			IProjectAppService projectAppService = this._projectAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = projectId
			};
			GetProjectResourceForEditOutput projectResourcesForEdit = await projectAppService.GetProjectResourcesForEdit(nullableIdInput);
			CreateOrUpdateProjectResourcesModalViewModel createOrUpdateProjectResourcesModalViewModel = new CreateOrUpdateProjectResourcesModalViewModel(projectResourcesForEdit)
			{
				ProjectId = projectId.Value
			};
			Project project = await this._projectAppService.GetProject(projectId.Value);
			((dynamic)this.ViewBag).ProjectName = project.Label;
			partialViewResult = (!reloadPartial.HasValue || !bool.Parse(reloadPartial.ToString()) ? this.PartialView("_CreateOrUpdateResourcesModal", createOrUpdateProjectResourcesModalViewModel) : this.PartialView("_ListResourcesView", createOrUpdateProjectResourcesModalViewModel));
			return partialViewResult;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Projects.Create", "Pages.Tenant.Projects.Edit" })]
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
			IProjectAppService projectAppService = this._projectAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateProjectTeamMembersModalViewModel createOrUpdateProjectTeamMembersModalViewModel = new CreateOrUpdateProjectTeamMembersModalViewModel(await projectAppService.GetProjectTeamMembersForEdit(nullableIdInput));
			CreateOrUpdateProjectTeamMembersModalViewModel createOrUpdateProjectTeamMembersModalViewModel1 = createOrUpdateProjectTeamMembersModalViewModel;
			long value1 = id.Value;
			createOrUpdateProjectTeamMembersModalViewModel1.ProjectId = long.Parse(value1.ToString());
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			ListResultDto<UserListDto> teamMembersByTenantId = await this._projectAppService.GetTeamMembersByTenantId(num, true);
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
			IProjectAppService projectAppService1 = this._projectAppService;
			value1 = id.Value;
			Project project = await projectAppService1.GetProject(long.Parse(value1.ToString()));
			((dynamic)this.ViewBag).ProjectName = project.Label;
			return this.PartialView("_CreateOrUpdateTeamMembersModal", createOrUpdateProjectTeamMembersModalViewModel);
		}

		[HttpPost]
		public async Task DeleteProjectResource(long id)
		{
			IdInput<long> idInput = new IdInput<long>()
			{
				Id = id
			};
			await this._projectAppService.DeleteProjectResource(idInput);
		}

		private FileResult GetDefaultProjectResourceImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-project-resource-image.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetProjectResourceById(Guid? resourceId, string fileExt)
		{
			FileResult defaultProjectResourceImage;
			Guid guid;
			if (!resourceId.HasValue || !Guid.TryParse(resourceId.ToString(), out guid))
			{
				defaultProjectResourceImage = this.GetDefaultProjectResourceImage();
			}
			else
			{
				defaultProjectResourceImage = await this.GetProjectResourceById(guid, fileExt);
			}
			return defaultProjectResourceImage;
		}

		private async Task<FileResult> GetProjectResourceById(Guid resourceId, string fileExt)
		{
			FileResult defaultProjectResourceImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(resourceId);
			if (orNullAsync != null)
			{
				Guid guid = Guid.NewGuid();
				string fileName = string.Concat(guid.ToString(), fileExt);
				ProjectResourceEditDto projectResourceDetailsByBinaryObjectId = await this._projectAppService.GetProjectResourceDetailsByBinaryObjectId(resourceId);
				if (projectResourceDetailsByBinaryObjectId != null && projectResourceDetailsByBinaryObjectId.FileName.Length > 0)
				{
					fileName = projectResourceDetailsByBinaryObjectId.FileName;
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
				defaultProjectResourceImage = this.File(orNullAsync.Bytes, str, fileName);
			}
			else
			{
				defaultProjectResourceImage = this.GetDefaultProjectResourceImage();
			}
			return defaultProjectResourceImage;
		}

		public ActionResult Index(GetProjectsInput input)
		{
			return base.View();
		}

		[HttpPost]
		public async Task SaveProjectResourceDetails(SaveProjectResourceDetailsInput input)
		{
			await this._projectAppService.SaveProjectResourceDetails(input.Id, input.Name, input.Description, input.IsActive);
		}

		[UnitOfWork]
		public virtual async Task<HttpResponseMessage> UploadResources(HttpRequestMessage request, long projectId)
		{
			HttpResponseMessage httpResponseMessage;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("ProjectResource_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength <= 2097152)
				{
					Project project = await this._projectAppService.GetProject(projectId);
					BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
					await this._binaryObjectManager.SaveAsync(binaryObject);
					UpdateProjectResourceInput updateProjectResourceInput = new UpdateProjectResourceInput()
					{
						Id = (long)0,
						ProjectId = project.Id,
						ResourceId = new Guid?(binaryObject.Id),
						FileName = item.FileName,
						FileExtension = Path.GetExtension(item.FileName),
						FileSize = item.ContentLength.ToString(),
						IsActive = true
					};
					await this._projectAppService.SaveProjectResourceAsync(updateProjectResourceInput);
					httpResponseMessage = request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					HttpResponseMessage httpResponseMessage1 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("ProjectResource_Warn_SizeLimit"));
					httpResponseMessage = httpResponseMessage1;
				}
			}
			catch (UserFriendlyException)
			{
				HttpResponseMessage httpResponseMessage2 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("ProjectResource_UploadException"));
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