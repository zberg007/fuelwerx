using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using FuelWerx;
using FuelWerx.Authorization.Users;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Customers;
using FuelWerx.Customers.Dto;
using FuelWerx.Dto;
using FuelWerx.EntityFramework;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Invoices.Dto;
using FuelWerx.Invoices.Exporting;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using FuelWerx.Products.Prices.Dto;
using FuelWerx.Projects;
using FuelWerx.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FuelWerx.Invoices
{
	public class InvoiceAppService : FuelWerxAppServiceBase, IInvoiceAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Invoice, long> _invoiceRepository;

		private readonly IRepository<InvoiceTask, long> _invoiceTaskRepository;

		private readonly IRepository<Product, long> _productRepository;

		private readonly IRepository<InvoiceProduct, long> _invoiceProductRepository;

		private readonly IRepository<InvoiceAdhocProduct, long> _invoiceAdhocProductRepository;

		private readonly IRepository<InvoiceAdjustment, long> _invoiceAdjustmentRepository;

		private readonly IRepository<InvoiceTeamMember, long> _invoiceTeamMemberRepository;

		private readonly IRepository<ProductPrice, long> _productPriceRepository;

		private readonly IRepository<InvoiceResource, long> _invoiceResourceRespository;

		private readonly IRepository<ProductOption, long> _productOptionRepository;

		private readonly IRepository<InvoiceProductLineItem, long> _invoiceProductLineItemRepository;

		private readonly IRepository<InvoiceProductLineItemOption, long> _invoiceProductLineItemOptionRepository;

		private readonly IRepository<Customer, long> _customerRepository;

		private readonly IRepository<Project, long> _projectRepository;

		private readonly IRepository<User, long> _userRepository;

		private readonly IInvoiceListExcelExporter _invoiceListExcelExporter;

		private readonly IRepository<InvoiceResource, long> _invoiceResourceRepository;

		private readonly IRepository<InvoicePayment, long> _invoicePaymentRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IBinaryObjectManager _binaryObjectManager;

		private readonly ITenantSettingsAppService _tenantSettingsAppService;

		private readonly IProductAppService _productAppService;

		private readonly IUnitOfWorkManager _unitOfWorkManager;

		private readonly IRepository<InvoiceProductLineItem, long> _productLineItemRepository;

		public InvoiceAppService(IRepository<Invoice, long> invoiceRepository, IRepository<InvoiceTask, long> invoiceOptionRepository, IInvoiceListExcelExporter invoiceListExcelExporter, IRepository<Customer, long> customerRepository, IRepository<Project, long> projectRepository, IRepository<Product, long> productRepository, IRepository<User, long> userRepository, IRepository<InvoiceProduct, long> invoiceProductRepository, IRepository<InvoiceAdhocProduct, long> invoiceAdhocProductRepository, IRepository<InvoiceAdjustment, long> invoiceAdjustmentRepository, IRepository<InvoiceTeamMember, long> invoiceTeamMemberRepository, IRepository<ProductPrice, long> productPriceRepository, IRepository<InvoiceResource, long> invoiceResourceRespository, IRepository<ProductOption, long> productOptionRepository, IRepository<InvoiceResource, long> invoiceResourceRepository, IRepository<InvoicePayment, long> invoicePaymentRepository, IRepository<Address, long> addressRepository, IRepository<CountryRegion> countryRegionRepository, IRepository<InvoiceProductLineItem, long> invoiceProductLineItemRepository, IRepository<InvoiceProductLineItemOption, long> invoiceProductLineItemOptionRepository, IBinaryObjectManager binaryObjectManager, ITenantSettingsAppService tenantSettingsAppService, IProductAppService productAppService, IUnitOfWorkManager unitOfWorkManager, IRepository<InvoiceProductLineItem, long> productLineItemRepository)
		{
			this._invoiceRepository = invoiceRepository;
			this._invoiceTaskRepository = invoiceOptionRepository;
			this._invoiceListExcelExporter = invoiceListExcelExporter;
			this._invoiceTeamMemberRepository = invoiceTeamMemberRepository;
			this._userRepository = userRepository;
			this._invoiceResourceRepository = invoiceResourceRepository;
			this._customerRepository = customerRepository;
			this._binaryObjectManager = binaryObjectManager;
			this._invoiceRepository = invoiceRepository;
			this._invoiceResourceRespository = invoiceResourceRespository;
			this._invoicePaymentRepository = invoicePaymentRepository;
			this._productOptionRepository = productOptionRepository;
			this._invoiceProductRepository = invoiceProductRepository;
			this._countryRegionRepository = countryRegionRepository;
			this._productRepository = productRepository;
			this._productPriceRepository = productPriceRepository;
			this._invoiceProductLineItemRepository = invoiceProductLineItemRepository;
			this._invoiceAdhocProductRepository = invoiceAdhocProductRepository;
			this._invoiceAdjustmentRepository = invoiceAdjustmentRepository;
			this._addressRepository = addressRepository;
			this._invoiceProductLineItemOptionRepository = invoiceProductLineItemOptionRepository;
			this._projectRepository = projectRepository;
			this._tenantSettingsAppService = tenantSettingsAppService;
			this._productAppService = productAppService;
			this._unitOfWorkManager = unitOfWorkManager;
			this._productLineItemRepository = productLineItemRepository;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices.Create", "Pages.Tenant.Invoices.Edit" })]
		public async Task<long> CreateOrUpdateInvoice(CreateOrUpdateInvoiceInput input)
		{
			long? nullableRecordId;
			bool flag;
			long recordId;
			if (input.Invoice.Number != null && input.Invoice.Number == "TBD")
			{
				InvoiceEditDto invoice = input.Invoice;
				invoice.Number = await this._tenantSettingsAppService.GetNextInvoiceNumberWithPrefix();
				invoice = null;
			}
			if (!input.Invoice.Id.HasValue)
			{
				if (!this.PermissionChecker.IsGranted("Pages.Tenant.Invoices.Create"))
				{
					throw new UserFriendlyException(this.L("Permissions_UserNotAuthorizedMessage"));
				}
				flag = true;
				Invoice invoice1 = new Invoice();
				Mapper.Map<InvoiceEditDto, Invoice>(input.Invoice, invoice1);
				recordId = await this._invoiceRepository.InsertAndGetIdAsync(invoice1);
			}
			else
			{
				if (!this.PermissionChecker.IsGranted("Pages.Tenant.Invoices.Edit"))
				{
					throw new UserFriendlyException(this.L("Permissions_UserNotAuthorizedMessage"));
				}
				flag = false;
				nullableRecordId = input.Invoice.Id;
				recordId = nullableRecordId.Value;
				Invoice invoice2 = new Invoice();
				Mapper.Map<InvoiceEditDto, Invoice>(input.Invoice, invoice2);
				invoice2.Tasks = null;
				invoice2.Products = null;
				invoice2.AdhocProducts = null;
				invoice2.Adjustments = null;
				await this._invoiceRepository.UpdateAsync(invoice2);
			}
			IRepository<InvoiceTask, long> repository = this._invoiceTaskRepository;
			List<InvoiceTask> allListAsync = await repository.GetAllListAsync((InvoiceTask x) => x.InvoiceId == recordId);
			List<InvoiceTask> invoiceTasks = allListAsync;
			if (input.Invoice.Tasks.Any<InvoiceTask>())
			{
				if (!flag)
				{
					if (!invoiceTasks.Any<InvoiceTask>())
					{
						foreach (InvoiceTask task in input.Invoice.Tasks)
						{
							task.InvoiceId = recordId;
							await this._invoiceTaskRepository.InsertAsync(task);
						}
					}
					else
					{
						List<InvoiceTask> invoiceTasks1 = invoiceTasks;
						List<long> list = (
							from x in invoiceTasks1
							select x.Id).ToList<long>();
						foreach (InvoiceTask invoiceTask in input.Invoice.Tasks)
						{
							if (invoiceTask.Id <= (long)0)
							{
								invoiceTask.InvoiceId = recordId;
								await this._invoiceTaskRepository.InsertAndGetIdAsync(invoiceTask);
							}
							else
							{
								InvoiceTask async = await this._invoiceTaskRepository.GetAsync(invoiceTask.Id);
								async.IsComplete = invoiceTask.IsComplete;
								async.Cost = invoiceTask.Cost;
								async.Comment = invoiceTask.Comment;
								async.Retail = invoiceTask.Retail;
								async.Name = invoiceTask.Name;
								async.Discount = invoiceTask.Discount;
								await this._invoiceTaskRepository.UpdateAsync(async);
								list.Remove(async.Id);
								async = null;
							}
						}
						if (list.Any<long>())
						{
							IRepository<InvoiceTask, long> repository1 = this._invoiceTaskRepository;
							await repository1.DeleteAsync((InvoiceTask x) => x.InvoiceId == recordId && list.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Invoice.Tasks.Any<InvoiceTask>() && invoiceTasks.Any<InvoiceTask>())
			{
				List<InvoiceTask> invoiceTasks2 = invoiceTasks;
				List<long> nums = (
					from x in invoiceTasks2
					select x.Id).ToList<long>();
				IRepository<InvoiceTask, long> repository2 = this._invoiceTaskRepository;
				await repository2.DeleteAsync((InvoiceTask x) => x.InvoiceId == recordId && nums.Contains(x.Id));
			}
			IRepository<InvoiceAdhocProduct, long> repository3 = this._invoiceAdhocProductRepository;
			List<InvoiceAdhocProduct> invoiceAdhocProducts = await repository3.GetAllListAsync((InvoiceAdhocProduct x) => x.InvoiceId == recordId);
			List<InvoiceAdhocProduct> invoiceAdhocProducts1 = invoiceAdhocProducts;
			if (input.Invoice.AdhocProducts.Any<InvoiceAdhocProduct>())
			{
				if (!flag)
				{
					if (!invoiceAdhocProducts1.Any<InvoiceAdhocProduct>())
					{
						foreach (InvoiceAdhocProduct adhocProduct in input.Invoice.AdhocProducts)
						{
							adhocProduct.InvoiceId = recordId;
							await this._invoiceAdhocProductRepository.InsertAsync(adhocProduct);
						}
					}
					else
					{
						List<InvoiceAdhocProduct> invoiceAdhocProducts2 = invoiceAdhocProducts1;
						List<long> list1 = (
							from x in invoiceAdhocProducts2
							select x.Id).ToList<long>();
						foreach (InvoiceAdhocProduct invoiceAdhocProduct in input.Invoice.AdhocProducts)
						{
							if (invoiceAdhocProduct.Id <= (long)0)
							{
								invoiceAdhocProduct.InvoiceId = recordId;
								await this._invoiceAdhocProductRepository.InsertAndGetIdAsync(invoiceAdhocProduct);
							}
							else
							{
								InvoiceAdhocProduct isTaxable = await this._invoiceAdhocProductRepository.GetAsync(invoiceAdhocProduct.Id);
								isTaxable.IsTaxable = invoiceAdhocProduct.IsTaxable;
								isTaxable.Cost = invoiceAdhocProduct.Cost;
								isTaxable.Description = invoiceAdhocProduct.Description;
								isTaxable.RetailCost = invoiceAdhocProduct.RetailCost;
								isTaxable.Name = invoiceAdhocProduct.Name;
								isTaxable.BaseCost = invoiceAdhocProduct.BaseCost;
								await this._invoiceAdhocProductRepository.UpdateAsync(isTaxable);
								list1.Remove(isTaxable.Id);
								isTaxable = null;
							}
						}
						if (list1.Any<long>())
						{
							IRepository<InvoiceAdhocProduct, long> repository4 = this._invoiceAdhocProductRepository;
							await repository4.DeleteAsync((InvoiceAdhocProduct x) => x.InvoiceId == recordId && list1.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Invoice.AdhocProducts.Any<InvoiceAdhocProduct>() && invoiceAdhocProducts1.Any<InvoiceAdhocProduct>())
			{
				List<InvoiceAdhocProduct> invoiceAdhocProducts3 = invoiceAdhocProducts1;
				List<long> nums1 = (
					from x in invoiceAdhocProducts3
					select x.Id).ToList<long>();
				IRepository<InvoiceAdhocProduct, long> repository5 = this._invoiceAdhocProductRepository;
				await repository5.DeleteAsync((InvoiceAdhocProduct x) => x.InvoiceId == recordId && nums1.Contains(x.Id));
			}
			IRepository<InvoiceAdjustment, long> repository6 = this._invoiceAdjustmentRepository;
			List<InvoiceAdjustment> invoiceAdjustments = await repository6.GetAllListAsync((InvoiceAdjustment x) => x.InvoiceId == recordId);
			List<InvoiceAdjustment> invoiceAdjustments1 = invoiceAdjustments;
			if (input.Invoice.Adjustments.Any<InvoiceAdjustment>())
			{
				if (!flag)
				{
					if (!invoiceAdjustments1.Any<InvoiceAdjustment>())
					{
						foreach (InvoiceAdjustment adjustment in input.Invoice.Adjustments)
						{
							adjustment.InvoiceId = recordId;
							await this._invoiceAdjustmentRepository.InsertAsync(adjustment);
						}
					}
					else
					{
						List<InvoiceAdjustment> invoiceAdjustments2 = invoiceAdjustments1;
						List<long> list2 = (
							from x in invoiceAdjustments2
							select x.Id).ToList<long>();
						foreach (InvoiceAdjustment invoiceAdjustment in input.Invoice.Adjustments)
						{
							if (invoiceAdjustment.Id <= (long)0)
							{
								invoiceAdjustment.InvoiceId = recordId;
								await this._invoiceAdjustmentRepository.InsertAndGetIdAsync(invoiceAdjustment);
							}
							else
							{
								InvoiceAdjustment cost = await this._invoiceAdjustmentRepository.GetAsync(invoiceAdjustment.Id);
								cost.IsTaxable = invoiceAdjustment.IsTaxable;
								cost.Cost = invoiceAdjustment.Cost;
								cost.Description = invoiceAdjustment.Description;
								cost.RetailCost = invoiceAdjustment.RetailCost;
								cost.Name = invoiceAdjustment.Name;
								await this._invoiceAdjustmentRepository.UpdateAsync(cost);
								list2.Remove(cost.Id);
								cost = null;
							}
						}
						if (list2.Any<long>())
						{
							IRepository<InvoiceAdjustment, long> repository7 = this._invoiceAdjustmentRepository;
							await repository7.DeleteAsync((InvoiceAdjustment x) => x.InvoiceId == recordId && list2.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Invoice.Adjustments.Any<InvoiceAdjustment>() && invoiceAdjustments1.Any<InvoiceAdjustment>())
			{
				List<InvoiceAdjustment> invoiceAdjustments3 = invoiceAdjustments1;
				List<long> nums2 = (
					from x in invoiceAdjustments3
					select x.Id).ToList<long>();
				IRepository<InvoiceAdjustment, long> repository8 = this._invoiceAdjustmentRepository;
				await repository8.DeleteAsync((InvoiceAdjustment x) => x.InvoiceId == recordId && nums2.Contains(x.Id));
			}
			IRepository<InvoiceProduct, long> repository9 = this._invoiceProductRepository;
			List<InvoiceProduct> invoiceProducts = await repository9.GetAllListAsync((InvoiceProduct w) => (long?)w.InvoiceId == input.Invoice.Id);
			List<InvoiceProduct> invoiceProducts1 = invoiceProducts;
			if (invoiceProducts1.Any<InvoiceProduct>())
			{
				List<InvoiceProduct> invoiceProducts2 = invoiceProducts1;
				List<long> list3 = (
					from s in invoiceProducts2
					select s.Id).ToList<long>();
				foreach (InvoiceProduct invoiceProduct in invoiceProducts1)
				{
					if ((
						from m in input.Invoice.Products
						where m.ProductId == invoiceProduct.ProductId
						select m).Any<InvoiceProductDto>())
					{
						IRepository<InvoiceProductLineItem, long> repository10 = this._invoiceProductLineItemRepository;
						List<InvoiceProductLineItem> invoiceProductLineItems = await repository10.GetAllListAsync((InvoiceProductLineItem x) => x.ProductId == invoiceProduct.ProductId && x.InvoiceId == recordId);
						List<InvoiceProductLineItem> invoiceProductLineItems1 = invoiceProductLineItems;
						if (invoiceProductLineItems1.Any<InvoiceProductLineItem>())
						{
							List<InvoiceProductLineItem> invoiceProductLineItems2 = invoiceProductLineItems1;
							List<long> nums3 = (
								from s in invoiceProductLineItems2
								select s.Id).ToList<long>();
							IRepository<InvoiceProductLineItemOption, long> repository11 = this._invoiceProductLineItemOptionRepository;
							List<InvoiceProductLineItemOption> invoiceProductLineItemOptions = await repository11.GetAllListAsync((InvoiceProductLineItemOption x) => nums3.Contains(x.ProductLineItemId));
							List<InvoiceProductLineItemOption> invoiceProductLineItemOptions1 = invoiceProductLineItemOptions;
							if (invoiceProductLineItemOptions1.Any<InvoiceProductLineItemOption>())
							{
								List<InvoiceProductLineItemOption> invoiceProductLineItemOptions2 = invoiceProductLineItemOptions1;
								List<long> list4 = (
									from s in invoiceProductLineItemOptions2
									select s.Id).ToList<long>();
								IRepository<InvoiceProductLineItemOption, long> repository12 = this._invoiceProductLineItemOptionRepository;
								await repository12.DeleteAsync((InvoiceProductLineItemOption x) => list4.Contains(x.Id));
							}
							IRepository<InvoiceProductLineItem, long> repository13 = this._invoiceProductLineItemRepository;
							await repository13.DeleteAsync((InvoiceProductLineItem x) => nums3.Contains(x.Id));
						}
						IEnumerable<InvoiceProductDto> products =
							from m in input.Invoice.Products
							where m.ProductId == invoiceProduct.ProductId
							select m;
						InvoiceProductLineItem invoiceProductLineItem = new InvoiceProductLineItem()
						{
							InvoiceId = recordId,
							Cost = products.First<InvoiceProductDto>().LineItem.Cost,
							Quantity = products.First<InvoiceProductDto>().LineItem.Quantity,
							ProductId = invoiceProduct.ProductId,
							Options = new List<InvoiceProductLineItemOption>()
						};
						long num = await this._invoiceProductLineItemRepository.InsertAndGetIdAsync(invoiceProductLineItem);
						if (products.First<InvoiceProductDto>().LineItem.Options.Any<InvoiceProductLineItemOptionDto>())
						{
							foreach (InvoiceProductLineItemOptionDto option in products.First<InvoiceProductDto>().LineItem.Options)
							{
								InvoiceProductLineItemOption invoiceProductLineItemOption = new InvoiceProductLineItemOption()
								{
									ProductLineItemId = num,
									ProductOptionId = option.ProductOptionId
								};
								await this._invoiceProductLineItemOptionRepository.InsertAsync(invoiceProductLineItemOption);
							}
						}
						input.Invoice.Products.Remove((
							from m in input.Invoice.Products
							where m.ProductId == invoiceProduct.ProductId
							select m).First<InvoiceProductDto>());
						list3.RemoveAll((long x) => x == invoiceProduct.Id);
						products = null;
					}
				}
				if (list3.Any<long>())
				{
					IRepository<InvoiceProduct, long> repository14 = this._invoiceProductRepository;
					List<InvoiceProduct> allListAsync1 = await repository14.GetAllListAsync((InvoiceProduct x) => list3.Contains(x.Id));
					foreach (InvoiceProduct invoiceProduct1 in allListAsync1)
					{
						if (invoiceProduct1.LineItemId.HasValue)
						{
							IRepository<InvoiceProductLineItem, long> repository15 = this._invoiceProductLineItemRepository;
							nullableRecordId = invoiceProduct1.LineItemId;
							InvoiceProductLineItem async1 = await repository15.GetAsync(nullableRecordId.Value);
							if (async1 != null && async1.Id > (long)0)
							{
								IRepository<InvoiceProductLineItemOption, long> repository16 = this._invoiceProductLineItemOptionRepository;
								await repository16.DeleteAsync((InvoiceProductLineItemOption x) => x.ProductLineItemId == async1.Id);
							}
							await this._invoiceProductLineItemRepository.DeleteAsync(async1);
						}
						await this._invoiceProductRepository.DeleteAsync(invoiceProduct1);
					}
				}
			}
			ICollection<InvoiceProductDto> invoiceProductDtos = input.Invoice.Products;
			List<long> nums4 = (
				from s in invoiceProductDtos
				select s.ProductId).ToList<long>();
			if (!flag && nums4.Any<long>())
			{
				IRepository<Product, long> repository17 = this._productRepository;
				List<Product> products1 = await repository17.GetAllListAsync((Product m) => nums4.Contains(m.Id));
				List<Product> products2 = products1;
				if (products2.Any<Product>())
				{
					List<Product> products3 = products2;
					List<long> list5 = (
						from s in products3
						select s.Id).ToList<long>();
					foreach (InvoiceProductDto product in input.Invoice.Products)
					{
						if (list5.Contains(product.ProductId))
						{
							InvoiceProduct invoiceProduct2 = new InvoiceProduct()
							{
								InvoiceId = recordId,
								ProductId = product.ProductId,
								IsActive = true
							};
							await this._invoiceProductRepository.InsertAndGetIdAsync(invoiceProduct2);
							InvoiceProductLineItem invoiceProductLineItem1 = new InvoiceProductLineItem()
							{
								InvoiceId = recordId,
								Cost = product.LineItem.Cost,
								Quantity = product.LineItem.Quantity,
								ProductId = product.ProductId,
								Options = new List<InvoiceProductLineItemOption>()
							};
							long num1 = await this._invoiceProductLineItemRepository.InsertAndGetIdAsync(invoiceProductLineItem1);
							if (product.LineItem.Options.Any<InvoiceProductLineItemOptionDto>())
							{
								foreach (InvoiceProductLineItemOptionDto invoiceProductLineItemOptionDto in product.LineItem.Options)
								{
									InvoiceProductLineItemOption invoiceProductLineItemOption1 = new InvoiceProductLineItemOption()
									{
										ProductLineItemId = num1,
										ProductOptionId = invoiceProductLineItemOptionDto.ProductOptionId
									};
									await this._invoiceProductLineItemOptionRepository.InsertAsync(invoiceProductLineItemOption1);
								}
							}
							num1 = (long)0;
						}
					}
					list5 = null;
				}
			}
			await _unitOfWorkManager.Current.SaveChangesAsync();
			using (_unitOfWorkManager.Current.DisableFilter(new string[] { "SoftDelete" }))
			{
				List<InvoiceProductLineItem> invoiceProductLineItems3 = new List<InvoiceProductLineItem>();
				IRepository<InvoiceProductLineItem, long> repository18 = this._invoiceProductLineItemRepository;
				List<InvoiceProductLineItem> allListAsync2 = await repository18.GetAllListAsync((InvoiceProductLineItem m) => m.InvoiceId == recordId && m.IsDeleted);
				List<InvoiceProductLineItem> invoiceProductLineItems4 = allListAsync2;
				bool flag1 = false;
				if (invoiceProductLineItems4.Any<InvoiceProductLineItem>())
				{
					flag1 = true;
				}
				IRepository<InvoiceProductLineItem, long> repository19 = this._invoiceProductLineItemRepository;
				List<InvoiceProductLineItem> allListAsync3 = await repository19.GetAllListAsync((InvoiceProductLineItem m) => m.InvoiceId == recordId && !m.IsDeleted);
				List<InvoiceProductLineItem> invoiceProductLineItems5 = allListAsync3;
				if (invoiceProductLineItems5.Any<InvoiceProductLineItem>())
				{
					foreach (InvoiceProductLineItem invoiceProductLineItem2 in invoiceProductLineItems5)
					{
						if (invoiceProductLineItem2.IsDeleted)
						{
							bool flag2 = await this._productAppService.IncreaseQuantityOnHand(invoiceProductLineItem2.ProductId, invoiceProductLineItem2.Quantity);
						}
						else
						{
							int quantity = -1;
							if (flag1)
							{
								IEnumerable<InvoiceProductLineItem> productId =
									from m in invoiceProductLineItems4
									where m.ProductId == invoiceProductLineItem2.ProductId
									select m;
								InvoiceProductLineItem invoiceProductLineItem3 = (
									from o in productId
									orderby o.Id descending
									select o).FirstOrDefault<InvoiceProductLineItem>();
								if (invoiceProductLineItem3 != null)
								{
									quantity = invoiceProductLineItem3.Quantity;
								}
							}
							if (quantity > 0)
							{
								bool flag3 = await this._productAppService.IncreaseQuantityOnHand(invoiceProductLineItem2.ProductId, quantity);
							}
							bool flag4 = await this._productAppService.ReduceQuantityOnHand(invoiceProductLineItem2.ProductId, invoiceProductLineItem2.Quantity);
						}
					}
					List<InvoiceProductLineItem> invoiceProductLineItems6 = invoiceProductLineItems5;
					List<long> nums5 = (
						from s in invoiceProductLineItems6
						select s.ProductId).ToList<long>();
					IEnumerable<InvoiceProductLineItem> invoiceProductLineItems7 =
						from m in invoiceProductLineItems4
						where !nums5.Contains(m.ProductId)
						select m;
					if (invoiceProductLineItems7.Any<InvoiceProductLineItem>())
					{
						foreach (InvoiceProductLineItem invoiceProductLineItem4 in invoiceProductLineItems7)
						{
							bool flag5 = await this._productAppService.IncreaseQuantityOnHand(invoiceProductLineItem4.ProductId, invoiceProductLineItem4.Quantity);
						}
					}
					if (flag1)
					{
						using (CustomDbContext customDbContext = new CustomDbContext())
						{
							foreach (InvoiceProductLineItem invoiceProductLineItem5 in invoiceProductLineItems4)
							{
								if (invoiceProductLineItem5.Id <= (long)0)
								{
									continue;
								}
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendLine("\tIF EXISTS (");
								stringBuilder.AppendLine("\t\t\tSELECT *");
								stringBuilder.AppendLine("\t\t\tFROM FuelWerxInvoiceProductLineItemOptions");
								stringBuilder.AppendLine("\t\t\tWHERE ProductLineItemId = @p0");
								stringBuilder.AppendLine("\t\t\t)");
								stringBuilder.AppendLine("\tBEGIN");
								stringBuilder.AppendLine("\t\tDELETE");
								stringBuilder.AppendLine("\t\tFROM FuelWerxInvoiceProductLineItemOptions");
								stringBuilder.AppendLine("\t\tWHERE ProductLineItemId = @p0");
								stringBuilder.AppendLine("\tEND");
								stringBuilder.AppendLine("\t\t\t\t");
								stringBuilder.AppendLine("\tIF EXISTS (");
								stringBuilder.AppendLine("\t\t\tSELECT *");
								stringBuilder.AppendLine("\t\t\tFROM FuelWerxInvoiceProducts");
								stringBuilder.AppendLine("\t\t\tWHERE LineItemId = @p0 AND IsDeleted = 1");
								stringBuilder.AppendLine("\t\t\t)");
								stringBuilder.AppendLine("\tBEGIN");
								stringBuilder.AppendLine("\t\tDELETE");
								stringBuilder.AppendLine("\t\tFROM FuelWerxInvoiceProducts");
								stringBuilder.AppendLine("\t\tWHERE LineItemId = @p0 AND IsDeleted = 1");
								stringBuilder.AppendLine("\tEND");
								stringBuilder.AppendLine("\t\t\t\t");
								stringBuilder.AppendLine("\tDELETE");
								stringBuilder.AppendLine("\tFROM FuelWerxInvoiceProductLineItems");
								stringBuilder.AppendLine("\tWHERE Id = @p0");
								stringBuilder.AppendLine("\t\t\t\t");
								stringBuilder.AppendLine("\tSELECT 1");
								Database database = customDbContext.Database;
								string str = stringBuilder.ToString();
								object[] sqlParameter = new object[] { new SqlParameter("@p0", (object)invoiceProductLineItem5.Id) };
								database.SqlQuery<int>(str, sqlParameter).Single<int>();
							}
						}
					}
				}
				invoiceProductLineItems4 = null;
				invoiceProductLineItems5 = null;
			}
			return recordId;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices.Create", "Pages.Tenant.Invoices.Edit" })]
		public async Task CreateOrUpdateInvoiceTeamMembers(CreateOrUpdateInvoiceTeamMemberInput input)
		{
			IRepository<InvoiceTeamMember, long> repository = this._invoiceTeamMemberRepository;
			List<InvoiceTeamMember> allListAsync = await repository.GetAllListAsync((InvoiceTeamMember w) => (long?)w.InvoiceId == input.InvoiceId);
			List<InvoiceTeamMember> invoiceTeamMembers = allListAsync;
			if (invoiceTeamMembers.Any<InvoiceTeamMember>())
			{
				List<InvoiceTeamMember> invoiceTeamMembers1 = invoiceTeamMembers;
				List<long> list = (
					from s in invoiceTeamMembers1
					select s.Id).ToList<long>();
				foreach (InvoiceTeamMember invoiceTeamMember in invoiceTeamMembers)
				{
					if (!input.InvoiceTeamMembers.Where<InvoiceTeamMemberEditDto>((InvoiceTeamMemberEditDto m) => {
						long? id = m.Id;
						long teamMemberId = invoiceTeamMember.TeamMemberId;
						if (id.GetValueOrDefault() != teamMemberId)
						{
							return false;
						}
						return id.HasValue;
					}).Any<InvoiceTeamMemberEditDto>())
					{
						continue;
					}
					input.InvoiceTeamMembers.RemoveAll((InvoiceTeamMemberEditDto x) => {
						long? id = x.Id;
						long teamMemberId = invoiceTeamMember.TeamMemberId;
						if (id.GetValueOrDefault() != teamMemberId)
						{
							return false;
						}
						return id.HasValue;
					});
					list.RemoveAll((long x) => x == invoiceTeamMember.Id);
				}
				if (list.Any<long>())
				{
					IRepository<InvoiceTeamMember, long> repository1 = this._invoiceTeamMemberRepository;
					await repository1.DeleteAsync((InvoiceTeamMember m) => list.Contains(m.Id));
				}
			}
			List<InvoiceTeamMemberEditDto> invoiceTeamMemberEditDtos = input.InvoiceTeamMembers;
			List<long> nums = (
				from s in invoiceTeamMemberEditDtos
				select s.TeamMemberId).ToList<long>();
			if (nums.Any<long>())
			{
				IRepository<User, long> repository2 = this._userRepository;
				List<User> users = await repository2.GetAllListAsync((User m) => nums.Contains(m.Id));
				List<User> users1 = users;
				if (users1.Any<User>())
				{
					List<User> users2 = users1;
					List<long> list1 = (
						from s in users2
						select s.Id).ToList<long>();
					foreach (InvoiceTeamMemberEditDto invoiceTeamMemberEditDto in input.InvoiceTeamMembers)
					{
						if (!list1.Contains(invoiceTeamMemberEditDto.TeamMemberId))
						{
							continue;
						}
						InvoiceTeamMember invoiceTeamMember1 = new InvoiceTeamMember()
						{
							InvoiceId = input.InvoiceId.Value,
							TeamMemberId = invoiceTeamMemberEditDto.TeamMemberId,
							IsActive = true
						};
						await this._invoiceTeamMemberRepository.InsertAsync(invoiceTeamMember1);
					}
					list1 = null;
				}
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices.Delete" })]
		public async Task DeleteInvoice(IdInput<long> input)
		{
			Invoice async = await this._invoiceRepository.GetAsync(input.Id);
			using (_unitOfWorkManager.Current.DisableFilter(new string[] { "SoftDelete" }))
			{
				IRepository<InvoiceProductLineItem, long> repository = this._invoiceProductLineItemRepository;
				List<InvoiceProductLineItem> allListAsync = await repository.GetAllListAsync((InvoiceProductLineItem m) => m.InvoiceId == async.Id && !m.IsDeleted);
				List<InvoiceProductLineItem> invoiceProductLineItems = allListAsync;
				if (invoiceProductLineItems.Any<InvoiceProductLineItem>())
				{
					using (CustomDbContext customDbContext = new CustomDbContext())
					{
						foreach (InvoiceProductLineItem invoiceProductLineItem in invoiceProductLineItems)
						{
							bool flag = await this._productAppService.IncreaseQuantityOnHand(invoiceProductLineItem.ProductId, invoiceProductLineItem.Quantity);
							if (invoiceProductLineItem.Id > (long)0)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendLine("\tIF EXISTS (");
								stringBuilder.AppendLine("\t\t\tSELECT *");
								stringBuilder.AppendLine("\t\t\tFROM FuelWerxInvoiceProductLineItemOptions");
								stringBuilder.AppendLine("\t\t\tWHERE ProductLineItemId = @p0");
								stringBuilder.AppendLine("\t\t\t)");
								stringBuilder.AppendLine("\tBEGIN");
								stringBuilder.AppendLine("\t\tDELETE");
								stringBuilder.AppendLine("\t\tFROM FuelWerxInvoiceProductLineItemOptions");
								stringBuilder.AppendLine("\t\tWHERE ProductLineItemId = @p0");
								stringBuilder.AppendLine("\tEND");
								stringBuilder.AppendLine("\t\t\t\t");
								stringBuilder.AppendLine("\tIF EXISTS (");
								stringBuilder.AppendLine("\t\t\tSELECT *");
								stringBuilder.AppendLine("\t\t\tFROM FuelWerxInvoiceProducts");
								stringBuilder.AppendLine("\t\t\tWHERE LineItemId = @p0");
								stringBuilder.AppendLine("\t\t\t)");
								stringBuilder.AppendLine("\tBEGIN");
								stringBuilder.AppendLine("\t\tDELETE");
								stringBuilder.AppendLine("\t\tFROM FuelWerxInvoiceProducts");
								stringBuilder.AppendLine("\t\tWHERE LineItemId = @p0");
								stringBuilder.AppendLine("\tEND");
								stringBuilder.AppendLine("\t\t\t\t");
								stringBuilder.AppendLine("\tDELETE");
								stringBuilder.AppendLine("\tFROM FuelWerxInvoiceProductLineItems");
								stringBuilder.AppendLine("\tWHERE Id = @p0");
								stringBuilder.AppendLine("\t\t\t\t");
								stringBuilder.AppendLine("\tSELECT 1");
								Database database = customDbContext.Database;
								string str = stringBuilder.ToString();
								object[] sqlParameter = new object[] { new SqlParameter("@p0", (object)invoiceProductLineItem.Id) };
								database.SqlQuery<int>(str, sqlParameter).Single<int>();
							}
						}
					}
				}
			}
			await this._invoiceRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices.Delete" })]
		public async Task DeleteInvoiceResource(IdInput<long> input)
		{
			InvoiceResource async = await this._invoiceResourceRepository.GetAsync(input.Id);
			if (async != null)
			{
				await this._invoiceResourceRepository.DeleteAsync(async.Id);
			}
		}

		private string GetCategoryFromFileExtension(string fileExt)
		{
			string empty = string.Empty;
			empty = (fileExt == ".pdf" || fileExt == ".xlsx" || fileExt == ".xls" || fileExt == ".docx" || fileExt == ".doc" || fileExt == ".zip" ? this.L("ResourceCategoryAttachment") : this.L("ResourceCategoryImage"));
			return empty;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices", "Rights.Customer.CanViewPayments" })]
		public async Task<Invoice> GetInvoice(long invoiceId)
		{
			return await this._invoiceRepository.GetAsync(invoiceId);
		}

		public async Task<GetInvoiceForCopyOutput> GetInvoiceForCopy(IdInput<long> input)
		{
			InvoiceCopyDto invoiceCopyDto = new InvoiceCopyDto();
			Invoice async = await this._invoiceRepository.GetAsync(input.Id);
			if (async != null)
			{
				invoiceCopyDto = null;
				InvoiceCopyDto invoiceCopyDto1 = new InvoiceCopyDto()
				{
					Customer = async.Customer,
					CustomerId = async.CustomerId,
					Number = async.Number,
					InvoiceId = async.Id
				};
				invoiceCopyDto = invoiceCopyDto1;
			}
			return new GetInvoiceForCopyOutput()
			{
				Invoice = invoiceCopyDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices.Create" })]
		public async Task<GetInvoiceForEditOutput> GetInvoiceForEdit(NullableIdInput<long> input)
		{
			InvoiceEditDto invoiceEditDto;
			if (!input.Id.HasValue)
			{
				invoiceEditDto = new InvoiceEditDto()
				{
					Tasks = new List<InvoiceTask>(),
					Products = new List<InvoiceProductDto>(),
					AdhocProducts = new List<InvoiceAdhocProduct>(),
					Adjustments = new List<InvoiceAdjustment>()
				};
			}
			else
			{
				IRepository<Invoice, long> repository = this._invoiceRepository;
				long? id = input.Id;
				Invoice async = await repository.GetAsync(id.Value);
				invoiceEditDto = async.MapTo<InvoiceEditDto>();
				InvoiceEditDto invoiceEditDto1 = invoiceEditDto;
				IRepository<InvoiceTask, long> repository1 = this._invoiceTaskRepository;
				List<InvoiceTask> allListAsync = await repository1.GetAllListAsync((InvoiceTask x) => x.InvoiceId == async.Id);
				invoiceEditDto1.Tasks = allListAsync;
				invoiceEditDto1 = null;
				IRepository<InvoiceProduct, long> repository2 = this._invoiceProductRepository;
				List<InvoiceProduct> invoiceProducts = await repository2.GetAllListAsync((InvoiceProduct x) => x.InvoiceId == async.Id);
				invoiceEditDto.Products = invoiceProducts.MapTo<List<InvoiceProductDto>>();
				if (invoiceEditDto.Products.Any<InvoiceProductDto>())
				{
					foreach (InvoiceProductDto product in invoiceEditDto.Products)
					{
						ProductDto productDto = product.Product;
						IRepository<ProductPrice, long> repository3 = this._productPriceRepository;
						List<ProductPrice> productPrices = await repository3.GetAllListAsync((ProductPrice x) => x.ProductId == product.Product.Id && x.IsActive);
						productDto.Prices = productPrices.MapTo<List<PriceDto>>();
						productDto = null;
						productDto = product.Product;
						IRepository<ProductOption, long> repository4 = this._productOptionRepository;
						List<ProductOption> productOptions = await repository4.GetAllListAsync((ProductOption x) => x.ProductId == product.Product.Id && x.IsActive);
						productDto.Options = productOptions.MapTo<List<ProductOptionDto>>();
						productDto = null;
					}
				}
				invoiceEditDto1 = invoiceEditDto;
				IRepository<InvoiceAdhocProduct, long> repository5 = this._invoiceAdhocProductRepository;
				List<InvoiceAdhocProduct> invoiceAdhocProducts = await repository5.GetAllListAsync((InvoiceAdhocProduct x) => x.InvoiceId == async.Id);
				invoiceEditDto1.AdhocProducts = invoiceAdhocProducts;
				invoiceEditDto1 = null;
				invoiceEditDto1 = invoiceEditDto;
				IRepository<InvoiceAdjustment, long> repository6 = this._invoiceAdjustmentRepository;
				List<InvoiceAdjustment> invoiceAdjustments = await repository6.GetAllListAsync((InvoiceAdjustment x) => x.InvoiceId == async.Id);
				invoiceEditDto1.Adjustments = invoiceAdjustments;
				invoiceEditDto1 = null;
				invoiceEditDto1 = invoiceEditDto;
				Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
				invoiceEditDto1.Customer = customer.MapTo<CustomerDto>();
				invoiceEditDto1 = null;
				if (!async.CustomerAddressId.HasValue)
				{
					invoiceEditDto.CustomerAddress = new AddressDto()
					{
						CountryRegion = new CountryRegionDto()
					};
				}
				else
				{
					invoiceEditDto1 = invoiceEditDto;
					IRepository<Address, long> repository7 = this._addressRepository;
					id = async.CustomerAddressId;
					Address address = await repository7.GetAsync(id.Value);
					invoiceEditDto1.CustomerAddress = address.MapTo<AddressDto>();
					invoiceEditDto1 = null;
					if (invoiceEditDto.CustomerAddress.CountryRegionId.HasValue)
					{
						AddressDto customerAddress = invoiceEditDto.CustomerAddress;
						IRepository<CountryRegion> repository8 = this._countryRegionRepository;
						int? countryRegionId = async.CustomerAddress.CountryRegionId;
						CountryRegion countryRegion = await repository8.GetAsync(countryRegionId.Value);
						customerAddress.CountryRegion = countryRegion.MapTo<CountryRegionDto>();
						customerAddress = null;
					}
				}
			}
			return new GetInvoiceForEditOutput()
			{
				Invoice = invoiceEditDto
			};
		}

		public async Task<GetInvoicePaymentForAddOutput> GetInvoicePaymentForCreateOrView(NullableIdInput<long> input)
		{
			InvoicePaymentAddDto invoicePaymentAddDto;
			if (!input.Id.HasValue)
			{
				invoicePaymentAddDto = new InvoicePaymentAddDto();
			}
			else
			{
				IRepository<InvoicePayment, long> repository = this._invoicePaymentRepository;
				InvoicePayment async = await repository.GetAsync(input.Id.Value);
				invoicePaymentAddDto = async.MapTo<InvoicePaymentAddDto>();
			}
			return new GetInvoicePaymentForAddOutput()
			{
				InvoicePayment = invoicePaymentAddDto
			};
		}

		public async Task<PagedResultOutput<InvoicePaymentListDto>> GetInvoicePayments(GetInvoicePaymentsInput input)
		{
			bool flag;
			IQueryable<InvoicePayment> all = this._invoicePaymentRepository.GetAll();
			flag = (!input.InvoiceId.HasValue ? false : input.InvoiceId.Value > (long)0);
			IQueryable<InvoicePayment> invoicePayments = all.WhereIf<InvoicePayment>(flag, (InvoicePayment p) => p.InvoiceId == input.InvoiceId.Value);
			int num = await invoicePayments.CountAsync<InvoicePayment>();
			List<InvoicePayment> listAsync = await invoicePayments.OrderBy<InvoicePayment>(input.Sorting, new object[0]).PageBy<InvoicePayment>(input).ToListAsync<InvoicePayment>();
			return new PagedResultOutput<InvoicePaymentListDto>(num, listAsync.MapTo<List<InvoicePaymentListDto>>());
		}

		public async Task<List<InvoicePaymentListDto>> GetInvoicePaymentsByInvoiceId(long invoiceId)
		{
			IQueryable<InvoicePayment> all = this._invoicePaymentRepository.GetAll();
			IQueryable<InvoicePayment> invoicePayments =
				from p in all
				where p.InvoiceId == invoiceId
				select p;
			List<InvoicePayment> listAsync = await (
				from o in invoicePayments
				orderby o.TransactionDateTime descending
				select o).ToListAsync<InvoicePayment>();
			return new List<InvoicePaymentListDto>(listAsync.MapTo<List<InvoicePaymentListDto>>());
		}

		public async Task<InvoiceResourceEditDto> GetInvoiceResourceDetailsByBinaryObjectId(Guid resourceId)
		{
			IRepository<InvoiceResource, long> repository = this._invoiceResourceRepository;
			InvoiceResource invoiceResource = await repository.FirstOrDefaultAsync((InvoiceResource m) => m.BinaryObjectId == resourceId);
			InvoiceResource invoiceResource1 = invoiceResource;
			if (invoiceResource1 == null)
			{
				throw new Exception("Invoice Resource was not found in GetInvoiceResourceDetailsByBinaryObjectId();");
			}
			return invoiceResource1.MapTo<InvoiceResourceEditDto>();
		}

		public async Task<GetInvoiceResourceForEditOutput> GetInvoiceResourcesForEdit(NullableIdInput<long> input)
		{
			List<InvoiceResourceEditDto> invoiceResourceEditDtos = new List<InvoiceResourceEditDto>();
			if (input.Id.HasValue)
			{
				IRepository<InvoiceResource, long> repository = this._invoiceResourceRepository;
				List<InvoiceResource> allListAsync = await repository.GetAllListAsync((InvoiceResource m) => m.InvoiceId == input.Id.Value);
				List<InvoiceResource> invoiceResources = allListAsync;
				if (invoiceResources.Any<InvoiceResource>())
				{
					invoiceResourceEditDtos = invoiceResources.MapTo<List<InvoiceResourceEditDto>>();
				}
			}
			return new GetInvoiceResourceForEditOutput()
			{
				InvoiceResources = invoiceResourceEditDtos
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices" })]
		public async Task<PagedResultOutput<InvoiceListDto>> GetInvoices(GetInvoicesInput input)
		{
			int idToGet;
			IQueryable<Invoice> all = this._invoiceRepository.GetAll();
            var invoices = all.Where(p => p.TenantId == AbpSession.TenantId && p.Id == 0); // Really?

			var listAsync = await invoices.Select(s => new { InvoiceId = s.Id }).ToListAsync();
			var collection = listAsync;
			List<long> foundInvoiceIdsFromInputFilter = (
				from s in listAsync
                select s.InvoiceId).ToList<long>();
			bool foundUsingIdFilter = false;
			if (input.Filter.ToLower().StartsWith("id:"))
			{
				try
				{
					string lower = input.Filter.ToLower();
					char[] chrArray = new char[] { ':' };
					int.TryParse(lower.Split(chrArray)[1].ToString(), out idToGet);
					IQueryable<Invoice> all1 = this._invoiceRepository.GetAll();
                    var invoices1 = all1.Where(p => p.TenantId == AbpSession.TenantId && p.Id == idToGet);
					var listAsync1 = await invoices1.Select(s => new { InvoiceId = s.Id }).ToListAsync();
					foundUsingIdFilter = true;
					foundInvoiceIdsFromInputFilter = foundInvoiceIdsFromInputFilter.Union(
						from s in listAsync1
                        select s.InvoiceId).ToList<long>();
				}
				catch (Exception)
				{
				}
			}
			if (!foundUsingIdFilter)
			{
				IQueryable<Invoice> all2 = this._invoiceRepository.GetAll();
                var invoices2 = all2.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
                    p.Label.Contains(input.Filter) ||
                    p.Number.Contains(input.Filter) ||
                    p.PONumber.Contains(input.Filter) ||
                    p.TimeEntryLog.Contains(input.Filter) ||
                    p.CurrentStatus.Contains(input.Filter) ||
                    p.Description.Contains(input.Filter) ||
                    p.BillingType.Contains(input.Filter) ||
                    p.Terms.Contains(input.Filter) ||
                    p.Customer.Email.Contains(input.Filter) ||
                    p.Customer.FirstName.Contains(input.Filter) ||
                    p.Customer.LastName.Contains(input.Filter) ||
                    p.Customer.BusinessName.Contains(input.Filter) ||
                    p.Project.CurrentStatus.Contains(input.Filter) ||
                    p.Project.Description.Contains(input.Filter) ||
                    p.Project.Label.Contains(input.Filter) ||
                    p.Project.Number.Contains(input.Filter) ||
                    p.Project.PONumber.Contains(input.Filter) ||
                    p.Project.Terms.Contains(input.Filter) ||
                    p.Project.TimeEntryLog.Contains(input.Filter));
				var listAsync2 = await invoices2.Select(s => new { InvoiceId = s.Id }).ToListAsync();
				foundInvoiceIdsFromInputFilter = foundInvoiceIdsFromInputFilter.Union(
					from s in listAsync2
                    select s.InvoiceId).ToList();
			}
			IQueryable<Invoice> all3 = this._invoiceRepository.GetAll();
            var invoices3 = all3.Where(m => foundInvoiceIdsFromInputFilter.Contains(m.Id));
			int resultCount = await invoices3.CountAsync();
			if (input.Sorting.Contains("customer"))
			{
				input.Sorting = input.Sorting.Replace("customer", "customerid");
			}
			List<Invoice> listAsync3 = await invoices3.OrderBy(input.Sorting, new object[0]).PageBy(input).ToListAsync();
			List<InvoiceListDto> invoiceListDtos = listAsync3.MapTo<List<InvoiceListDto>>();
			foreach (InvoiceListDto invoiceListDto in invoiceListDtos)
			{;
                int taskTotal = await _invoiceTaskRepository.CountAsync(m => m.InvoiceId == invoiceListDto.Id);
				invoiceListDto.TaskTotal = taskTotal;
				IRepository<InvoicePayment, long> repository1 = this._invoicePaymentRepository;
                int paymentRecordsCount = await _invoicePaymentRepository.CountAsync(m => m.InvoiceId == invoiceListDto.Id);
				invoiceListDto.RelatedPaymentRecordsCount = paymentRecordsCount;
			}
			return new PagedResultOutput<InvoiceListDto>(resultCount, invoiceListDtos);
		}

		public async Task<PagedResultOutput<InvoiceListDto>> GetInvoicesForAddress(GetCustomerInvoicesInput input)
		{
			decimal? nullable;
			if (!input.AddressId.HasValue || !input.Id.HasValue)
			{
				throw new UserFriendlyException(this.L("InvalidOperation"));
			}
			IQueryable<Invoice> all = this._invoiceRepository.GetAll();
			IQueryable<Invoice> customerAddressId =
				from m in all
				where m.CustomerAddressId == (long?)input.AddressId.Value
				select m;
			int num = await customerAddressId.CountAsync<Invoice>();
			List<Invoice> listAsync = await customerAddressId.OrderBy<Invoice>(input.Sorting, new object[0]).PageBy<Invoice>(input).ToListAsync<Invoice>();
			List<InvoiceListDto> invoiceListDtos = listAsync.MapTo<List<InvoiceListDto>>();
			foreach (InvoiceListDto invoiceListDto in invoiceListDtos)
			{
				InvoiceListDto invoiceListDto1 = invoiceListDto;
				IRepository<InvoiceTask, long> repository = this._invoiceTaskRepository;
				int num1 = await repository.CountAsync((InvoiceTask m) => m.InvoiceId == (long)invoiceListDto.Id);
				invoiceListDto1.TaskTotal = num1;
				invoiceListDto1 = null;
				invoiceListDto1 = invoiceListDto;
				IRepository<InvoicePayment, long> repository1 = this._invoicePaymentRepository;
				int num2 = await repository1.CountAsync((InvoicePayment m) => m.InvoiceId == (long)invoiceListDto.Id);
				invoiceListDto1.RelatedPaymentRecordsCount = num2;
				invoiceListDto1 = null;
				IRepository<InvoicePayment, long> repository2 = this._invoicePaymentRepository;
				List<InvoicePayment> allListAsync = await repository2.GetAllListAsync((InvoicePayment m) => m.InvoiceId == (long)invoiceListDto.Id);
				List<InvoicePayment> invoicePayments = allListAsync;
				InvoiceListDto invoiceListDto2 = invoiceListDto;
				if (!invoicePayments.Any<InvoicePayment>())
				{
					nullable = null;
				}
				else
				{
					List<InvoicePayment> invoicePayments1 = invoicePayments;
					nullable = new decimal?(invoicePayments1.Sum<InvoicePayment>((InvoicePayment s) => s.DollarAmount));
				}
				invoiceListDto2.RelatedPaymentRecordsTotalPaid = nullable;
			}
			return new PagedResultOutput<InvoiceListDto>(num, invoiceListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices.ExportData" })]
		public async Task<FileDto> GetInvoicesToExcel()
		{
			List<Invoice> allListAsync = await this._invoiceRepository.GetAllListAsync();
			List<InvoiceListDto> invoiceListDtos = allListAsync.MapTo<List<InvoiceListDto>>();
			return this._invoiceListExcelExporter.ExportToFile(invoiceListDtos);
		}

		public async Task<GetInvoiceTeamMembersForEditOutput> GetInvoiceTeamMembersForEdit(NullableIdInput<long> input)
		{
			List<InvoiceTeamMemberEditDto> invoiceTeamMemberEditDtos = new List<InvoiceTeamMemberEditDto>();
			if (input.Id.HasValue)
			{
				IRepository<InvoiceTeamMember, long> repository = this._invoiceTeamMemberRepository;
				List<InvoiceTeamMember> allListAsync = await repository.GetAllListAsync((InvoiceTeamMember m) => m.InvoiceId == input.Id.Value);
				List<InvoiceTeamMember> invoiceTeamMembers = allListAsync;
				if (invoiceTeamMembers.Any<InvoiceTeamMember>())
				{
					invoiceTeamMemberEditDtos = invoiceTeamMembers.MapTo<List<InvoiceTeamMemberEditDto>>();
				}
			}
			return new GetInvoiceTeamMembersForEditOutput()
			{
				InvoiceTeamMembers = invoiceTeamMemberEditDtos
			};
		}

		public async Task<PagedResultOutput<InvoicePaymentListDto>> GetPayments(GetInvoicePaymentsInput input)
		{
			IQueryable<InvoicePayment> all = this._invoicePaymentRepository.GetAll();
			IQueryable<InvoicePayment> tenantId =
				from m in all
				where (int?)m.TenantId == this.AbpSession.TenantId && !m.IsDeleted
				select m;
			long? id = input.Id;
			bool hasValue = id.HasValue;
			IQueryable<InvoicePayment> invoicePayments = tenantId.WhereIf<InvoicePayment>(hasValue, (InvoicePayment p) => p.Id == input.Id.Value);
			id = input.InvoiceId;
			bool flag = id.HasValue;
			IQueryable<InvoicePayment> invoicePayments1 = invoicePayments.WhereIf<InvoicePayment>(flag, (InvoicePayment p) => p.InvoiceId == input.InvoiceId.Value);
			IQueryable<InvoicePayment> invoicePayments2 = invoicePayments1.WhereIf<InvoicePayment>(!input.Filter.IsNullOrEmpty(), (InvoicePayment p) => p.P_Authorization_Num.Contains(input.Filter) || p.P_Customer_Ref.Contains(input.Filter) || p.P_Exact_Ctr.Contains(input.Filter) || p.X_Trans_Id.Contains(input.Filter) || p.X_Response_Reason_Code.Contains(input.Filter));
			int num = await invoicePayments2.CountAsync<InvoicePayment>();
			List<InvoicePayment> listAsync = await invoicePayments2.OrderBy<InvoicePayment>(input.Sorting, new object[0]).PageBy<InvoicePayment>(input).ToListAsync<InvoicePayment>();
			List<InvoicePaymentListDto> invoicePaymentListDtos = listAsync.MapTo<List<InvoicePaymentListDto>>();
			List<long> nums = new List<long>();
			using (_unitOfWorkManager.Current.DisableFilter(new string[] { "SoftDelete" }))
			{
				foreach (InvoicePaymentListDto invoicePaymentListDto in invoicePaymentListDtos)
				{
					Invoice async = await this._invoiceRepository.GetAsync(invoicePaymentListDto.InvoiceId);
					invoicePaymentListDto.Invoice = async.MapTo<InvoiceEditDto>();
					Customer customer = await this._customerRepository.GetAsync(invoicePaymentListDto.CustomerId);
					invoicePaymentListDto.Customer = customer.MapTo<CustomerDto>();
					if (async.IsDeleted)
					{
						List<long> nums1 = nums;
						id = invoicePaymentListDto.Invoice.Id;
						nums1.Add(id.Value);
					}
					async = null;
				}
			}
			PagedResultOutput<InvoicePaymentListDto> pagedResultOutput = new PagedResultOutput<InvoicePaymentListDto>(num, (
				from m in invoicePaymentListDtos
				where !nums.Contains(m.InvoiceId)
				select m).ToList<InvoicePaymentListDto>());
			return pagedResultOutput;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.InvoicePayments.ExportData" })]
		public async Task<FileDto> GetPaymentsToExcel()
		{
			List<InvoicePayment> allListAsync = await this._invoicePaymentRepository.GetAllListAsync();
			List<InvoicePaymentListDto> invoicePaymentListDtos = allListAsync.MapTo<List<InvoicePaymentListDto>>();
			foreach (InvoicePaymentListDto invoicePaymentListDto in invoicePaymentListDtos)
			{
				Invoice async = await this._invoiceRepository.GetAsync(invoicePaymentListDto.InvoiceId);
				invoicePaymentListDto.Invoice = async.MapTo<InvoiceEditDto>();
				Customer customer = await this._customerRepository.GetAsync(invoicePaymentListDto.CustomerId);
				invoicePaymentListDto.Customer = customer.MapTo<CustomerDto>();
			}
			return this._invoiceListExcelExporter.ExportToFile(invoicePaymentListDtos);
		}

		public async Task<ListResultDto<UserListDto>> GetTeamMembersByTenantId(int tenantId, bool active)
		{
			IQueryable<User> all = this._userRepository.GetAll();
			IQueryable<User> users =
				from p in all
				where p.TenantId == (int?)tenantId
				select p;
			bool flag = active;
			List<User> listAsync = await users.WhereIf<User>(flag, (User p) => p.IsActive == active).OrderBy<User>("Name", new object[0]).ToListAsync<User>();
			return new ListResultDto<UserListDto>(listAsync.MapTo<List<UserListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Invoices.Create", "Pages.Tenant.Invoices.Edit" })]
		public async Task SaveInvoiceResourceAsync(UpdateInvoiceResourceInput input)
		{
			Guid? resourceId;
			if (input.Id <= (long)0)
			{
				InvoiceResource invoiceResource = new InvoiceResource()
				{
					Id = (long)0
				};
				resourceId = input.ResourceId;
				invoiceResource.BinaryObjectId = Guid.Parse(resourceId.ToString());
				invoiceResource.InvoiceId = input.InvoiceId;
				invoiceResource.Name = input.FileName;
				invoiceResource.FileName = input.FileName;
				invoiceResource.FileExtension = input.FileExtension;
				invoiceResource.FileSize = input.FileSize;
				invoiceResource.Category = this.GetCategoryFromFileExtension(input.FileExtension);
				invoiceResource.IsActive = input.IsActive;
				await this._invoiceResourceRepository.InsertAsync(invoiceResource);
			}
			else
			{
				InvoiceResource async = await this._invoiceResourceRepository.GetAsync(input.Id);
				if (async != null)
				{
					resourceId = input.ResourceId;
					async.BinaryObjectId = Guid.Parse(resourceId.ToString());
					async.InvoiceId = input.InvoiceId;
					async.FileExtension = input.FileExtension;
					async.FileName = input.FileName;
					async.FileSize = input.FileSize;
					async.Category = this.GetCategoryFromFileExtension(input.FileExtension);
					async.IsActive = input.IsActive;
				}
				await this._invoiceResourceRepository.UpdateAsync(async);
			}
		}

		public async Task SaveInvoiceResourceDetails(long id, string name, string description, bool isActive)
		{
			InvoiceResource async = await this._invoiceResourceRepository.GetAsync(id);
			if (async != null)
			{
				async.Description = description;
			}
			await this._invoiceResourceRepository.UpdateAsync(async);
		}
	}
}