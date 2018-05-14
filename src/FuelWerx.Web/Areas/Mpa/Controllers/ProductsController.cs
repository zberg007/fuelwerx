using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using FuelWerx.Administrative.TaxRules;
using FuelWerx.Administrative.TaxRules.Dto;
using FuelWerx.Customers;
using FuelWerx.Customers.Dto;
using FuelWerx.Generic;
using FuelWerx.Organizations;
using FuelWerx.Organizations.Dto;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using FuelWerx.Products.Prices;
using FuelWerx.Products.Prices.Dto;
using FuelWerx.Products.SpecificPrices;
using FuelWerx.Products.SpecificPrices.Dto;
using FuelWerx.Storage;
using FuelWerx.Suppliers;
using FuelWerx.Suppliers.Dto;
using FuelWerx.Web.Areas.Mpa.Models.Products;
using FuelWerx.Web.Areas.Mpa.Models.Products.Prices;
using FuelWerx.Web.Areas.Mpa.Models.Products.SpecificPrices;
using FuelWerx.Web.Controllers;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
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
	public class ProductsController : FuelWerxControllerBase
	{
		private readonly IRepository<ProductOption, long> _productOptionRepository;

		private readonly ICustomerAppService _customerAppService;

		private readonly IProductAppService _productAppService;

		private readonly IPriceAppService _priceAppService;

		private readonly ISpecificPriceAppService _specificPriceAppService;

		private readonly ITaxRuleAppService _taxRuleAppService;

		private readonly ISupplierAppService _supplierAppService;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly IOrganizationUnitAppService _organizationUnitAppService;

		public ProductsController(IProductAppService productAppService, IPriceAppService priceAppService, ICustomerAppService customerAppService, ISpecificPriceAppService specificPriceAppService, ITaxRuleAppService taxRuleAppService, ISupplierAppService supplierAppService, IBinaryObjectManager binaryObjectManager, IOrganizationUnitAppService organizationUnitAppService, IRepository<ProductOption, long> productOptionRepository)
		{
			this._productAppService = productAppService;
			this._priceAppService = priceAppService;
			this._customerAppService = customerAppService;
			this._specificPriceAppService = specificPriceAppService;
			this._taxRuleAppService = taxRuleAppService;
			this._supplierAppService = supplierAppService;
			this._binaryObjectManager = binaryObjectManager;
			this._organizationUnitAppService = organizationUnitAppService;
			this._productOptionRepository = productOptionRepository;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateModal(long? id = null)
		{
			IProductAppService productAppService = this._productAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdateProductModalViewModel createOrUpdateProductModalViewModel = new CreateOrUpdateProductModalViewModel(await productAppService.GetProductForEdit(nullableIdInput));
			if (!id.HasValue)
			{
				createOrUpdateProductModalViewModel.CanMakeActive = new bool?(false);
			}
			else
			{
				IPriceAppService priceAppService = this._priceAppService;
				GetProductPricesInput getProductPricesInput = new GetProductPricesInput()
				{
					ProductId = id.Value
				};
				PagedResultOutput<ProductPriceListDto> prices = await priceAppService.GetPrices(getProductPricesInput);
				CreateOrUpdateProductModalViewModel nullable = createOrUpdateProductModalViewModel;
				IReadOnlyList<ProductPriceListDto> items = prices.Items;
				nullable.CanMakeActive = new bool?((
					from x in items
					where x.IsActive
					select x).Any<ProductPriceListDto>());
			}
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (Lookup lookupItem in (new LookupFill("QuantityTypes", -1)).LookupItems)
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
			this.ViewData["QuantitySoldInTypes"] = selectListItems;
			return this.PartialView("_CreateOrUpdateModal", createOrUpdateProductModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdatePriceModal(long productId, long? id = null)
		{
			IPriceAppService priceAppService = this._priceAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			CreateOrUpdatePriceModalViewModel createOrUpdatePriceModalViewModel = new CreateOrUpdatePriceModalViewModel(await priceAppService.GetProductPriceForEdit(nullableIdInput));
			Product product = await this._productAppService.GetProduct(productId);
			createOrUpdatePriceModalViewModel.ProductIsCurrentlyActive = product.IsActive;
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			ITaxRuleAppService taxRuleAppService = this._taxRuleAppService;
			int? tenantId = this.AbpSession.TenantId;
			ListResultDto<TaxRuleListDto> taxRulesByTenantId = await taxRuleAppService.GetTaxRulesByTenantId(tenantId.Value, true);
			if (!taxRulesByTenantId.Items.Any<TaxRuleListDto>())
			{
				this.ViewData["TaxRules"] = null;
			}
			else
			{
				foreach (TaxRuleListDto item in taxRulesByTenantId.Items)
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
				this.ViewData["TaxRules"] = selectListItems.AsEnumerable<SelectListItem>();
			}
			return this.PartialView("_CreateOrUpdatePriceModal", createOrUpdatePriceModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateResourcesModal(long? productId = null, bool? reloadPartial = null)
		{
			PartialViewResult partialViewResult;
			IProductAppService productAppService = this._productAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = productId
			};
			GetProductResourceForEditOutput productResourcesForEdit = await productAppService.GetProductResourcesForEdit(nullableIdInput);
			CreateOrUpdateProductResourcesModalViewModel createOrUpdateProductResourcesModalViewModel = new CreateOrUpdateProductResourcesModalViewModel(productResourcesForEdit)
			{
				ProductId = productId.Value
			};
			Product product = await this._productAppService.GetProduct(productId.Value);
			((dynamic)this.ViewBag).ProductName = product.Name;
			partialViewResult = (!reloadPartial.HasValue || !bool.Parse(reloadPartial.ToString()) ? this.PartialView("_CreateOrUpdateResourcesModal", createOrUpdateProductResourcesModalViewModel) : this.PartialView("_ListResourcesView", createOrUpdateProductResourcesModalViewModel));
			return partialViewResult;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateSpecificPriceModal(long productId, long? id = null)
		{
			int? nullable;
			bool flag;
			bool flag1;
			ISpecificPriceAppService specificPriceAppService = this._specificPriceAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = id
			};
			GetProductSpecificPriceForEditOutput productSpecificPriceForEdit = await specificPriceAppService.GetProductSpecificPriceForEdit(nullableIdInput);
			CreateOrUpdateSpecificPriceModalViewModel createOrUpdateSpecificPriceModalViewModel = new CreateOrUpdateSpecificPriceModalViewModel(productSpecificPriceForEdit);
			Product product = await this._productAppService.GetProduct(productId);
			createOrUpdateSpecificPriceModalViewModel.BaseCost = new decimal?(product.FinalPrice);
			createOrUpdateSpecificPriceModalViewModel.QuantitySoldIn = product.QuantitySoldIn;
			if (productSpecificPriceForEdit.SpecificPrice.ForCustomerId.HasValue)
			{
				ICustomerAppService customerAppService = this._customerAppService;
				GetCustomersInput getCustomersInput = new GetCustomersInput()
				{
					MaxResultCount = 1,
					Sorting = "FirstName",
					SkipCount = 0
				};
				long value = productSpecificPriceForEdit.SpecificPrice.ForCustomerId.Value;
				getCustomersInput.Filter = string.Concat("id:", value.ToString());
				PagedResultOutput<CustomerListDto> customers = await customerAppService.GetCustomers(getCustomersInput);
				createOrUpdateSpecificPriceModalViewModel.CurrentlySelectedCustomerName = string.Concat(customers.Items[0].FullName, " - ", customers.Items[0].Email);
			}
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			using (HttpClient httpClient = new HttpClient())
			{
				UrlHelper url = this.Url;
				nullable = (createOrUpdateSpecificPriceModalViewModel.SpecificPrice.ForCountryId.HasValue ? createOrUpdateSpecificPriceModalViewModel.SpecificPrice.ForCountryId : new int?(0));
				string str = url.RouteUrl("DefaultApiWithAction", new { httproute = "", controller = "Generic", action = "GetCountriesAsSelectListItems", countryId = 0, selectedCountryId = nullable }, this.Request.Url.Scheme);
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
			List<OrganizationUnitDto> organizationUnitsByProperty = await this._organizationUnitAppService.GetOrganizationUnitsByProperty("SpecificPricesEnabled", "true");
			List<SelectListItem> selectListItems2 = new List<SelectListItem>();
			foreach (OrganizationUnitDto organizationUnitDto in organizationUnitsByProperty)
			{
				List<SelectListItem> selectListItems3 = selectListItems2;
				SelectListItem selectListItem1 = new SelectListItem();
				string displayName = organizationUnitDto.DisplayName;
				int memberCount = organizationUnitDto.MemberCount;
				selectListItem1.Text = string.Format("{0} ({1} {2})", displayName, memberCount.ToString(), this.L("OUMemberCount"));
				selectListItem1.Value = organizationUnitDto.Id.ToString();
				selectListItem1.Disabled = false;
				selectListItem1.Group = null;
				flag = (!createOrUpdateSpecificPriceModalViewModel.SpecificPrice.ForOrganizationalUnitId.HasValue || createOrUpdateSpecificPriceModalViewModel.SpecificPrice.ForOrganizationalUnitId.Value != organizationUnitDto.Id ? false : true);
				selectListItem1.Selected = flag;
				selectListItems3.Add(selectListItem1);
			}
			SelectListItem selectListItem2 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems2.Insert(0, selectListItem2);
			this.ViewData["OrganizationUnits"] = selectListItems2.AsEnumerable<SelectListItem>();
			IRepository<ProductOption, long> repository = this._productOptionRepository;
			List<ProductOption> allListAsync = await repository.GetAllListAsync((ProductOption x) => x.ProductId == product.Id && x.IsActive);
			List<ProductOption> productOptions = allListAsync;
			List<SelectListItem> selectListItems4 = new List<SelectListItem>();
			foreach (ProductOption productOption in productOptions)
			{
				List<SelectListItem> selectListItems5 = selectListItems4;
				SelectListItem selectListItem3 = new SelectListItem()
				{
					Text = productOption.Name,
					Value = productOption.Id.ToString(),
					Disabled = false,
					Group = null
				};
				flag1 = (!createOrUpdateSpecificPriceModalViewModel.SpecificPrice.ProductOptionId.HasValue || createOrUpdateSpecificPriceModalViewModel.SpecificPrice.ProductOptionId.Value != productOption.Id ? false : true);
				selectListItem3.Selected = flag1;
				selectListItems5.Add(selectListItem3);
			}
			SelectListItem selectListItem4 = new SelectListItem()
			{
				Text = "",
				Value = "",
				Disabled = false
			};
			selectListItems4.Insert(0, selectListItem4);
			this.ViewData["ProductOptions"] = selectListItems4.AsEnumerable<SelectListItem>();
			return this.PartialView("_CreateOrUpdateSpecificPriceModal", createOrUpdateSpecificPriceModalViewModel);
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Products.Create", "Pages.Tenant.Products.Edit" })]
		public async Task<PartialViewResult> CreateOrUpdateSuppliersModal(long? productId = null)
		{
			IProductAppService productAppService = this._productAppService;
			NullableIdInput<long> nullableIdInput = new NullableIdInput<long>()
			{
				Id = productId
			};
			GetProductSuppliersForEditOutput productSuppliersForEdit = await productAppService.GetProductSuppliersForEdit(nullableIdInput);
			CreateOrUpdateProductSuppliersModalViewModel createOrUpdateProductSuppliersModalViewModel = new CreateOrUpdateProductSuppliersModalViewModel(productSuppliersForEdit)
			{
				ProductId = productId.Value
			};
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			ISupplierAppService supplierAppService = this._supplierAppService;
			int? tenantId = this.AbpSession.TenantId;
			ListResultDto<SupplierListDto> suppliersByTenantId = await supplierAppService.GetSuppliersByTenantId(tenantId.Value, true);
			if (!suppliersByTenantId.Items.Any<SupplierListDto>())
			{
				this.ViewData["Suppliers"] = null;
			}
			else
			{
				foreach (SupplierListDto item in suppliersByTenantId.Items)
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
				this.ViewData["Suppliers"] = selectListItems.AsEnumerable<SelectListItem>();
			}
			return this.PartialView("_CreateOrUpdateSuppliersModal", createOrUpdateProductSuppliersModalViewModel);
		}

		[HttpPost]
		public async Task DeleteProductResource(long id)
		{
			IdInput<long> idInput = new IdInput<long>()
			{
				Id = id
			};
			await this._productAppService.DeleteProductResource(idInput);
		}

		private FileResult GetDefaultProductImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-product-image.png"), "image/png");
		}

		private FileResult GetDefaultProductResourceImage()
		{
			return base.File(base.Server.MapPath("~/Common/Images/default-product-resource-image.png"), "image/png");
		}

		[DisableAuditing]
		public async Task<FileResult> GetProductImageById(Guid? productImageId)
		{
			FileResult defaultProductImage;
			Guid guid;
			if (!productImageId.HasValue || !Guid.TryParse(productImageId.ToString(), out guid))
			{
				defaultProductImage = this.GetDefaultProductImage();
			}
			else
			{
				defaultProductImage = await this.GetProductImageById(guid);
			}
			return defaultProductImage;
		}

		private async Task<FileResult> GetProductImageById(Guid productImageId)
		{
			FileResult defaultProductImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(productImageId);
			if (orNullAsync != null)
			{
				defaultProductImage = this.File(orNullAsync.Bytes, "image/jpeg");
			}
			else
			{
				defaultProductImage = this.GetDefaultProductImage();
			}
			return defaultProductImage;
		}

		[DisableAuditing]
		public async Task<FileResult> GetProductResourceById(Guid? resourceId, string fileExt)
		{
			FileResult defaultProductResourceImage;
			Guid guid;
			if (!resourceId.HasValue || !Guid.TryParse(resourceId.ToString(), out guid))
			{
				defaultProductResourceImage = this.GetDefaultProductResourceImage();
			}
			else
			{
				defaultProductResourceImage = await this.GetProductResourceById(guid, fileExt);
			}
			return defaultProductResourceImage;
		}

		private async Task<FileResult> GetProductResourceById(Guid resourceId, string fileExt)
		{
			FileResult defaultProductResourceImage;
			BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(resourceId);
			if (orNullAsync != null)
			{
				Guid guid = Guid.NewGuid();
				string fileName = string.Concat(guid.ToString(), fileExt);
				ProductResourceEditDto productResourceDetailsByBinaryObjectId = await this._productAppService.GetProductResourceDetailsByBinaryObjectId(resourceId);
				if (productResourceDetailsByBinaryObjectId != null && productResourceDetailsByBinaryObjectId.FileName.Length > 0)
				{
					fileName = productResourceDetailsByBinaryObjectId.FileName;
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
				defaultProductResourceImage = this.File(orNullAsync.Bytes, str, fileName);
			}
			else
			{
				defaultProductResourceImage = this.GetDefaultProductResourceImage();
			}
			return defaultProductResourceImage;
		}

		[AbpMvcAuthorize(new string[] { "Pages.Tenant.Products" })]
		public ActionResult Index(GetProductsInput input)
		{
			return base.View();
		}

		[HttpPost]
		public async Task SaveProductResourceDetails(SaveProductResourceDetailsInput input)
		{
			await this._productAppService.SaveProductResourceDetails(input.Id, input.Name, input.Description, input.IsActive);
		}

		[UnitOfWork]
		public virtual async Task<JsonResult> UpdateProductImage(long productId)
		{
			JsonResult jsonResult;
			Guid? imageId;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("ProductImage_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength > 512000)
				{
					throw new UserFriendlyException(this.L("ProductImage_Warn_SizeLimit"));
				}
				GetProductForEditOutput productForEdit = await this._productAppService.GetProductForEdit(new NullableIdInput<long>(new long?(productId)));
				GetProductForEditOutput nullable = productForEdit;
				if (nullable.Product.ImageId.HasValue)
				{
					IBinaryObjectManager binaryObjectManager = this._binaryObjectManager;
					imageId = nullable.Product.ImageId;
					await binaryObjectManager.DeleteAsync(imageId.Value);
				}
				BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
				await this._binaryObjectManager.SaveAsync(binaryObject);
				nullable.Product.ImageId = new Guid?(binaryObject.Id);
				UpdateProductImageInput updateProductImageInput = new UpdateProductImageInput()
				{
					ProductId = nullable.Product.Id.Value
				};
				imageId = nullable.Product.ImageId;
				updateProductImageInput.ImageId = new Guid?(imageId.Value);
				await this._productAppService.SaveProductImageAsync(updateProductImageInput);
				jsonResult = this.Json(new MvcAjaxResponse());
			}
			catch (UserFriendlyException userFriendlyException1)
			{
				UserFriendlyException userFriendlyException = userFriendlyException1;
				jsonResult = this.Json(new MvcAjaxResponse(new ErrorInfo(userFriendlyException.Message), false));
			}
			return jsonResult;
		}

		[UnitOfWork]
		public virtual async Task<HttpResponseMessage> UploadResources(HttpRequestMessage request, long productId)
		{
			HttpResponseMessage httpResponseMessage;
			try
			{
				if (this.Request.Files.Count <= 0 || this.Request.Files[0] == null)
				{
					throw new UserFriendlyException(this.L("ProductResource_Change_Error"));
				}
				HttpPostedFileBase item = this.Request.Files[0];
				if (item.ContentLength <= 2097152)
				{
					Product product = await this._productAppService.GetProduct(productId);
					BinaryObject binaryObject = new BinaryObject(item.InputStream.GetAllBytes());
					await this._binaryObjectManager.SaveAsync(binaryObject);
					UpdateProductResourceInput updateProductResourceInput = new UpdateProductResourceInput()
					{
						Id = (long)0,
						ProductId = product.Id,
						ResourceId = new Guid?(binaryObject.Id),
						FileName = item.FileName,
						FileExtension = Path.GetExtension(item.FileName),
						FileSize = item.ContentLength.ToString(),
						IsActive = true
					};
					await this._productAppService.SaveProductResourceAsync(updateProductResourceInput);
					httpResponseMessage = request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					HttpResponseMessage httpResponseMessage1 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("ProductResource_Warn_SizeLimit"));
					httpResponseMessage = httpResponseMessage1;
				}
			}
			catch (UserFriendlyException)
			{
				HttpResponseMessage httpResponseMessage2 = request.CreateResponse<string>(HttpStatusCode.BadRequest, this.L("ProductResource_UploadException"));
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