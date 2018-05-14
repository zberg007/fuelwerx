using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using FuelWerx;
using FuelWerx.Customers;
using FuelWerx.Customers.Dto;
using FuelWerx.Dto;
using FuelWerx.Estimates;
using FuelWerx.Estimates.Dto;
using FuelWerx.Estimates.Exporting;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using FuelWerx.Products.Prices.Dto;
using FuelWerx.Projects;
using FuelWerx.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Estimates
{
	public class EstimateAppService : FuelWerxAppServiceBase, IEstimateAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Estimate, long> _estimateRepository;

		private readonly IRepository<EstimateTask, long> _estimateTaskRepository;

		private readonly IRepository<EstimateProduct, long> _estimateProductRepository;

		private readonly IRepository<EstimateAdhocProduct, long> _estimateAdhocProductRepository;

		private readonly IRepository<EstimateAdjustment, long> _estimateAdjustmentRepository;

		private readonly IRepository<Product, long> _productRepository;

		private readonly IRepository<EstimateResource, long> _estimateResourceRespository;

		private readonly IRepository<Customer, long> _customerRepository;

		private readonly IRepository<Project, long> _projectRepository;

		private readonly IRepository<ProductPrice, long> _productPriceRepository;

		private readonly IRepository<ProductOption, long> _productOptionRepository;

		private readonly IEstimateListExcelExporter _estimateListExcelExporter;

		private readonly IRepository<EstimateProductLineItem, long> _estimateProductLineItemRepository;

		private readonly IRepository<EstimateProductLineItemOption, long> _estimateProductLineItemOptionRepository;

		private readonly IRepository<EstimateResource, long> _estimateResourceRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public EstimateAppService(IRepository<Estimate, long> estimateRepository, IRepository<EstimateTask, long> estimateOptionRepository, IRepository<EstimateResource, long> estimateResourceRespository, IEstimateListExcelExporter estimateListExcelExporter, IRepository<Customer, long> customerRepository, IRepository<Project, long> projectRepository, IRepository<EstimateResource, long> estimateResourceRepository, IBinaryObjectManager binaryObjectManager, IRepository<EstimateProduct, long> estimateProductRepository, IRepository<Product, long> productRepository, IRepository<EstimateAdhocProduct, long> estimateAdhocProductRepository, IRepository<EstimateAdjustment, long> estimateAdjustmentRepository, IRepository<ProductPrice, long> productPriceRepository, IRepository<CountryRegion> countryRegionRepository, IRepository<ProductOption, long> productOptionRepository, IRepository<EstimateProductLineItem, long> estimateProductLineItemRepository, IRepository<Address, long> addressRepository, IRepository<EstimateProductLineItemOption, long> estimateProductLineItemOptionRepository)
		{
			this._estimateRepository = estimateRepository;
			this._estimateResourceRespository = estimateResourceRespository;
			this._estimateTaskRepository = estimateOptionRepository;
			this._estimateListExcelExporter = estimateListExcelExporter;
			this._estimateResourceRepository = estimateResourceRepository;
			this._customerRepository = customerRepository;
			this._projectRepository = projectRepository;
			this._binaryObjectManager = binaryObjectManager;
			this._estimateProductRepository = estimateProductRepository;
			this._productRepository = productRepository;
			this._countryRegionRepository = countryRegionRepository;
			this._productOptionRepository = productOptionRepository;
			this._estimateAdhocProductRepository = estimateAdhocProductRepository;
			this._estimateAdjustmentRepository = estimateAdjustmentRepository;
			this._productPriceRepository = productPriceRepository;
			this._estimateProductLineItemRepository = estimateProductLineItemRepository;
			this._addressRepository = addressRepository;
			this._estimateProductLineItemOptionRepository = estimateProductLineItemOptionRepository;
		}

		public async Task AcceptAsProject(IdInput<long> input)
		{
			Estimate async = await this._estimateRepository.GetAsync(input.Id);
			if (async != null)
			{
				IRepository<EstimateTask, long> repository = this._estimateTaskRepository;
				List<EstimateTask> allListAsync = await repository.GetAllListAsync((EstimateTask m) => m.EstimateId == async.Id);
				List<EstimateTask> estimateTasks = allListAsync;
				IRepository<EstimateProduct, long> repository1 = this._estimateProductRepository;
				List<EstimateProduct> estimateProducts = await repository1.GetAllListAsync((EstimateProduct m) => m.EstimateId == async.Id);
				List<EstimateProduct> estimateProducts1 = estimateProducts;
				IRepository<EstimateAdhocProduct, long> repository2 = this._estimateAdhocProductRepository;
				List<EstimateAdhocProduct> estimateAdhocProducts = await repository2.GetAllListAsync((EstimateAdhocProduct m) => m.EstimateId == async.Id);
				List<EstimateAdhocProduct> estimateAdhocProducts1 = estimateAdhocProducts;
				IRepository<EstimateAdjustment, long> repository3 = this._estimateAdjustmentRepository;
				List<EstimateAdjustment> estimateAdjustments = await repository3.GetAllListAsync((EstimateAdjustment m) => m.EstimateId == async.Id);
				List<EstimateAdjustment> estimateAdjustments1 = estimateAdjustments;
				IRepository<EstimateResource, long> repository4 = this._estimateResourceRespository;
				List<EstimateResource> estimateResources = await repository4.GetAllListAsync((EstimateResource m) => m.EstimateId == async.Id);
				List<EstimateResource> estimateResources1 = estimateResources;
				IMappingExpression<Estimate, Project> mappingExpression = Mapper.CreateMap<Estimate, Project>();
				Expression<Func<Project, object>> id = (Project p) => (object)p.Id;
				IMappingExpression<Estimate, Project> mappingExpression1 = mappingExpression.ForMember(id, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> creationTime = (Project p) => (object)p.CreationTime;
				IMappingExpression<Estimate, Project> mappingExpression2 = mappingExpression1.ForMember(creationTime, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> creatorUserId = (Project p) => (object)p.CreatorUserId;
				IMappingExpression<Estimate, Project> mappingExpression3 = mappingExpression2.ForMember(creatorUserId, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> deletionTime = (Project p) => (object)p.DeletionTime;
				IMappingExpression<Estimate, Project> mappingExpression4 = mappingExpression3.ForMember(deletionTime, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> deleterUserId = (Project p) => (object)p.DeleterUserId;
				IMappingExpression<Estimate, Project> mappingExpression5 = mappingExpression4.ForMember(deleterUserId, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> lastModifierUserId = (Project p) => (object)p.LastModifierUserId;
				IMappingExpression<Estimate, Project> mappingExpression6 = mappingExpression5.ForMember(lastModifierUserId, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> lastModificationTime = (Project p) => (object)p.LastModificationTime;
				IMappingExpression<Estimate, Project> mappingExpression7 = mappingExpression6.ForMember(lastModificationTime, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> tasks = (Project p) => p.Tasks;
				IMappingExpression<Estimate, Project> mappingExpression8 = mappingExpression7.ForMember(tasks, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> resources = (Project p) => p.Resources;
				IMappingExpression<Estimate, Project> mappingExpression9 = mappingExpression8.ForMember(resources, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> taskTotal = (Project p) => (object)p.TaskTotal;
				IMappingExpression<Estimate, Project> mappingExpression10 = mappingExpression9.ForMember(taskTotal, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> teamMembers = (Project p) => p.TeamMembers;
				IMappingExpression<Estimate, Project> mappingExpression11 = mappingExpression10.ForMember(teamMembers, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> products = (Project p) => p.Products;
				IMappingExpression<Estimate, Project> mappingExpression12 = mappingExpression11.ForMember(products, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> adhocProducts = (Project p) => p.AdhocProducts;
				IMappingExpression<Estimate, Project> mappingExpression13 = mappingExpression12.ForMember(adhocProducts, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> adjustments = (Project p) => p.Adjustments;
				IMappingExpression<Estimate, Project> mappingExpression14 = mappingExpression13.ForMember(adjustments, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Expression<Func<Project, object>> expression = (Project p) => (object)p.Id;
				mappingExpression14.ForMember(expression, (IMemberConfigurationExpression<Estimate> options) => options.Ignore());
				Project nullable = Mapper.Map<Estimate, Project>(async, new Project());
				nullable.Id = (long)0;
				nullable.EstimateId = new long?(async.Id);
				nullable.IsActive = true;
				nullable.IsDeleted = false;
				List<ProjectTask> projectTasks = new List<ProjectTask>();
				foreach (EstimateTask estimateTask in estimateTasks)
				{
					ProjectTask projectTask = new ProjectTask()
					{
						ProjectId = (long)0,
						Name = estimateTask.Name,
						Comment = estimateTask.Comment,
						Cost = estimateTask.Cost,
						Retail = estimateTask.Retail,
						Discount = estimateTask.Discount,
						IsComplete = estimateTask.IsComplete,
						IsActive = true
					};
					projectTasks.Add(projectTask);
				}
				if (projectTasks.Any<ProjectTask>())
				{
					nullable.Tasks = projectTasks;
				}
				List<ProjectAdhocProduct> projectAdhocProducts = new List<ProjectAdhocProduct>();
				foreach (EstimateAdhocProduct estimateAdhocProduct in estimateAdhocProducts1)
				{
					ProjectAdhocProduct projectAdhocProduct = new ProjectAdhocProduct()
					{
						ProjectId = (long)0,
						Name = estimateAdhocProduct.Name,
						BaseCost = estimateAdhocProduct.BaseCost,
						Cost = estimateAdhocProduct.Cost,
						Description = estimateAdhocProduct.Description,
						IsTaxable = estimateAdhocProduct.IsTaxable,
						RetailCost = estimateAdhocProduct.RetailCost,
						IsActive = true
					};
					projectAdhocProducts.Add(projectAdhocProduct);
				}
				if (projectAdhocProducts.Any<ProjectAdhocProduct>())
				{
					nullable.AdhocProducts = projectAdhocProducts;
				}
				List<ProjectAdjustment> projectAdjustments = new List<ProjectAdjustment>();
				foreach (EstimateAdjustment estimateAdjustment in estimateAdjustments1)
				{
					ProjectAdjustment projectAdjustment = new ProjectAdjustment()
					{
						ProjectId = (long)0,
						Name = estimateAdjustment.Name,
						Cost = estimateAdjustment.Cost,
						Description = estimateAdjustment.Description,
						IsTaxable = estimateAdjustment.IsTaxable,
						RetailCost = estimateAdjustment.RetailCost,
						IsActive = true
					};
					projectAdjustments.Add(projectAdjustment);
				}
				if (projectAdjustments.Any<ProjectAdjustment>())
				{
					nullable.Adjustments = projectAdjustments;
				}
				List<ProjectProduct> projectProducts = new List<ProjectProduct>();
				foreach (EstimateProduct estimateProduct in estimateProducts1)
				{
					ProjectProduct projectProduct = new ProjectProduct()
					{
						ProjectId = (long)0,
						ProductId = estimateProduct.ProductId,
						IsActive = true
					};
					ProjectProduct projectProduct1 = projectProduct;
					if (estimateProduct.LineItem != null)
					{
						ProjectProductLineItem projectProductLineItem = new ProjectProductLineItem()
						{
							ProductId = estimateProduct.ProductId,
							Cost = estimateProduct.LineItem.Cost,
							Quantity = estimateProduct.LineItem.Quantity,
							Options = new List<ProjectProductLineItemOption>(),
							ProjectId = (long)0
						};
						ProjectProductLineItem projectProductLineItem1 = projectProductLineItem;
						if (estimateProduct.LineItem.Options.Any<EstimateProductLineItemOption>())
						{
							foreach (EstimateProductLineItemOption option in estimateProduct.LineItem.Options)
							{
								ProjectProductLineItemOption projectProductLineItemOption = new ProjectProductLineItemOption()
								{
									ProductOptionId = option.ProductOptionId
								};
								projectProductLineItem1.Options.Add(projectProductLineItemOption);
							}
						}
						projectProduct1.LineItem = projectProductLineItem1;
					}
					projectProducts.Add(projectProduct1);
				}
				if (projectProducts.Any<ProjectProduct>())
				{
					nullable.Products = projectProducts;
				}
				List<ProjectResource> projectResources = new List<ProjectResource>();
				foreach (EstimateResource estimateResource in estimateResources1)
				{
					BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(estimateResource.BinaryObjectId);
					if (orNullAsync != null)
					{
						BinaryObject binaryObject = new BinaryObject()
						{
							Bytes = orNullAsync.Bytes
						};
						ProjectResource projectResource = new ProjectResource()
						{
							ProjectId = (long)0,
							Name = estimateResource.Name,
							Category = estimateResource.Category,
							Description = estimateResource.Description,
							FileExtension = estimateResource.FileExtension,
							FileName = estimateResource.FileName,
							FileSize = estimateResource.FileSize,
							BinaryObject = binaryObject,
							BinaryObjectId = Guid.Empty,
							IsDeleted = false,
							IsActive = true
						};
						projectResources.Add(projectResource);
					}
				}
				if (projectResources.Any<ProjectResource>())
				{
					nullable.Resources = projectResources;
				}
				long num = await this._projectRepository.InsertAndGetIdAsync(nullable);
				if (num > (long)0)
				{
					async.IsActive = false;
					async.ProjectAcceptedTime = new DateTime?(DateTime.Now);
					async.ProjectId = new long?(num);
					await this._estimateRepository.UpdateAsync(async);
				}
				estimateTasks = null;
				estimateProducts1 = null;
				estimateAdhocProducts1 = null;
				estimateAdjustments1 = null;
				nullable = null;
				projectResources = null;
			}
		}

		public async Task CopyEstimate(CopyEstimateInput input)
		{
			long value;
			Estimate async = await this._estimateRepository.GetAsync(input.Estimate.EstimateId);
			Estimate estimate = async;
			if (estimate != null)
			{
				IRepository<EstimateTask, long> repository = this._estimateTaskRepository;
				List<EstimateTask> allListAsync = await repository.GetAllListAsync((EstimateTask m) => m.EstimateId == estimate.Id);
				List<EstimateTask> estimateTasks = allListAsync;
				IRepository<EstimateResource, long> repository1 = this._estimateResourceRespository;
				List<EstimateResource> estimateResources = await repository1.GetAllListAsync((EstimateResource m) => m.EstimateId == estimate.Id);
				List<EstimateResource> estimateResources1 = estimateResources;
				if (input.Estimate.CustomerFirstName.Length > 0 && input.Estimate.CustomerLastName.Length > 0 && input.Estimate.CustomerEmail.Length > 0)
				{
					Customer customer = new Customer()
					{
						FirstName = input.Estimate.CustomerFirstName,
						LastName = input.Estimate.CustomerLastName,
						Email = input.Estimate.CustomerEmail,
						TenantId = this.AbpSession.GetTenantId(),
						IsActive = true
					};
					Customer customer1 = customer;
					EstimateCopyDto estimateCopyDto = input.Estimate;
					estimateCopyDto.CustomerId = await this._customerRepository.InsertAndGetIdAsync(customer1);
					estimateCopyDto = null;
				}
				Estimate nullable = new Estimate()
				{
					Label = input.Estimate.Label,
					Number = input.Estimate.Number,
					CustomerId = input.Estimate.CustomerId,
					CurrentStatus = this.L("EstimateNewlyCopiedDefaultStatus"),
					CreationTime = DateTime.Now
				};
				long? impersonatorUserId = this.AbpSession.ImpersonatorUserId;
				if (impersonatorUserId.HasValue)
				{
					impersonatorUserId = this.AbpSession.ImpersonatorUserId;
					value = impersonatorUserId.Value;
				}
				else
				{
					impersonatorUserId = this.AbpSession.UserId;
					value = impersonatorUserId.Value;
				}
				nullable.CreatorUserId = new long?(value);
				DateTime? date = null;
				nullable.LastModificationTime = date;
				impersonatorUserId = null;
				nullable.LastModifierUserId = impersonatorUserId;
				impersonatorUserId = null;
				nullable.DeleterUserId = impersonatorUserId;
				date = null;
				nullable.DeletionTime = date;
				nullable.IsDeleted = false;
				nullable.TimeEntryLog = string.Empty;
				nullable.IsActive = true;
				date = null;
				nullable.ProjectAcceptedTime = date;
				impersonatorUserId = null;
				nullable.ProjectId = impersonatorUserId;
				date = estimate.Date;
				nullable.Date = new DateTime?(date.Value);
				nullable.Description = estimate.Description;
				nullable.Discount = estimate.Discount;
				nullable.Hours = estimate.Hours;
				nullable.LineTotal = estimate.LineTotal;
				nullable.LogDataAndTasksVisibleToCustomer = estimate.LogDataAndTasksVisibleToCustomer;
				nullable.PONumber = estimate.PONumber;
				nullable.Rate = estimate.Rate;
				nullable.Tax = estimate.Tax;
				nullable.TenantId = estimate.TenantId;
				nullable.Terms = estimate.Terms;
				nullable.BillingType = estimate.BillingType;
				Estimate estimate1 = nullable;
				List<EstimateTask> estimateTasks1 = new List<EstimateTask>();
				foreach (EstimateTask estimateTask in estimateTasks)
				{
					EstimateTask estimateTask1 = new EstimateTask()
					{
						EstimateId = (long)0,
						Name = estimateTask.Name,
						Comment = estimateTask.Comment,
						Cost = estimateTask.Cost,
						Retail = estimateTask.Retail,
						Discount = estimateTask.Discount,
						IsComplete = false,
						IsActive = true
					};
					estimateTasks1.Add(estimateTask1);
				}
				if (estimateTasks1.Any<EstimateTask>())
				{
					estimate1.Tasks = estimateTasks1;
				}
				List<EstimateResource> estimateResources2 = new List<EstimateResource>();
				foreach (EstimateResource estimateResource in estimateResources1)
				{
					BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(estimateResource.BinaryObjectId);
					if (orNullAsync != null)
					{
						BinaryObject binaryObject = new BinaryObject()
						{
							Bytes = orNullAsync.Bytes
						};
						EstimateResource estimateResource1 = new EstimateResource()
						{
							EstimateId = (long)0,
							Name = estimateResource.Name,
							Category = estimateResource.Category,
							Description = estimateResource.Description,
							FileExtension = estimateResource.FileExtension,
							FileName = estimateResource.FileName,
							FileSize = estimateResource.FileSize,
							BinaryObject = binaryObject,
							BinaryObjectId = Guid.Empty,
							IsDeleted = false,
							IsActive = true
						};
						estimateResources2.Add(estimateResource1);
					}
				}
				if (estimateResources2.Any<EstimateResource>())
				{
					estimate1.Resources = estimateResources2;
				}
				await this._estimateRepository.InsertAsync(estimate1);
				estimateTasks = null;
				estimateResources1 = null;
				estimate1 = null;
				estimateResources2 = null;
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates.Create", "Pages.Tenant.Estimates.Edit" })]
		public async Task<long> CreateOrUpdateEstimate(CreateOrUpdateEstimateInput input)
		{
			long? id;
			bool inputEstimateIdIsSet;
			long estimateId;
			if (!input.Estimate.Id.HasValue)
			{
				if (!this.PermissionChecker.IsGranted("Pages.Tenant.Estimates.Create"))
				{
					throw new UserFriendlyException(this.L("Permissions_UserNotAuthorizedMessage"));
				}
				inputEstimateIdIsSet = true;
				Estimate estimate = new Estimate();
				Mapper.Map<EstimateEditDto, Estimate>(input.Estimate, estimate);
				estimateId = await this._estimateRepository.InsertAndGetIdAsync(estimate);
			}
			else
			{
				if (!this.PermissionChecker.IsGranted("Pages.Tenant.Estimates.Edit"))
				{
					throw new UserFriendlyException(this.L("Permissions_UserNotAuthorizedMessage"));
				}
				inputEstimateIdIsSet = false;
				id = input.Estimate.Id;
				estimateId = id.Value;
				Estimate estimate1 = new Estimate();
				Mapper.Map(input.Estimate, estimate1);
				estimate1.Tasks = null;
				estimate1.Products = null;
				estimate1.AdhocProducts = null;
				estimate1.Adjustments = null;
				await this._estimateRepository.UpdateAsync(estimate1);
			}
			List<EstimateTask> estimateTasks = await _estimateTaskRepository.GetAllListAsync(x => x.EstimateId == estimateId);
			if (input.Estimate.Tasks.Any())
			{
				if (!inputEstimateIdIsSet)
				{
					if (!estimateTasks.Any())
					{
						foreach (EstimateTask task in input.Estimate.Tasks)
						{
							task.EstimateId = estimateId;
							await _estimateTaskRepository.InsertAsync(task);
						}
					}
					else
					{
						List<long> estimateTaskIds = (
							from x in estimateTasks
                            select x.Id).ToList<long>();
						foreach (EstimateTask estimateTask in input.Estimate.Tasks)
						{
							if (estimateTask.Id <= 0)
							{
								estimateTask.EstimateId = estimateId;
								await _estimateTaskRepository.InsertAndGetIdAsync(estimateTask);
							}
							else
							{
								EstimateTask existingTask = await _estimateTaskRepository.GetAsync(estimateTask.Id);
								existingTask.IsComplete = estimateTask.IsComplete;
								existingTask.Cost = estimateTask.Cost;
								existingTask.Comment = estimateTask.Comment;
								existingTask.Retail = estimateTask.Retail;
								existingTask.Name = estimateTask.Name;
								existingTask.Discount = estimateTask.Discount;
								await _estimateTaskRepository.UpdateAsync(existingTask);
								estimateTaskIds.Remove(existingTask.Id);
							}
						}
						if (estimateTaskIds.Any())
						{
							await _estimateTaskRepository.DeleteAsync((EstimateTask x) => x.EstimateId == estimateId && estimateTaskIds.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Estimate.Tasks.Any() && estimateTasks.Any())
			{
				List<long> nums = (
					from x in estimateTasks
                    select x.Id).ToList<long>();
				await _estimateTaskRepository.DeleteAsync((EstimateTask x) => x.EstimateId == estimateId && nums.Contains(x.Id));
			}
			List<EstimateAdhocProduct> estimateAdhocProducts = await _estimateAdhocProductRepository.GetAllListAsync((EstimateAdhocProduct x) => x.EstimateId == estimateId);
			if (input.Estimate.AdhocProducts.Any())
			{
				if (!inputEstimateIdIsSet)
				{
					if (!estimateAdhocProducts.Any())
					{
						foreach (EstimateAdhocProduct adhocProduct in input.Estimate.AdhocProducts)
						{
							adhocProduct.EstimateId = estimateId;
							await this._estimateAdhocProductRepository.InsertAsync(adhocProduct);
						}
					}
					else
					{
						List<long> list1 = (
							from x in estimateAdhocProducts
                            select x.Id).ToList();
						foreach (EstimateAdhocProduct estimateAdhocProduct in input.Estimate.AdhocProducts)
						{
							if (estimateAdhocProduct.Id <= 0)
							{
								estimateAdhocProduct.EstimateId = estimateId;
								await this._estimateAdhocProductRepository.InsertAndGetIdAsync(estimateAdhocProduct);
							}
							else
							{
								EstimateAdhocProduct isTaxable = await this._estimateAdhocProductRepository.GetAsync(estimateAdhocProduct.Id);
								isTaxable.IsTaxable = estimateAdhocProduct.IsTaxable;
								isTaxable.Cost = estimateAdhocProduct.Cost;
								isTaxable.Description = estimateAdhocProduct.Description;
								isTaxable.RetailCost = estimateAdhocProduct.RetailCost;
								isTaxable.Name = estimateAdhocProduct.Name;
								isTaxable.BaseCost = estimateAdhocProduct.BaseCost;
								await this._estimateAdhocProductRepository.UpdateAsync(isTaxable);
								list1.Remove(isTaxable.Id);
								isTaxable = null;
							}
						}
						if (list1.Any())
						{
							await _estimateAdhocProductRepository.DeleteAsync((EstimateAdhocProduct x) => x.EstimateId == estimateId && list1.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Estimate.AdhocProducts.Any() && estimateAdhocProducts.Any())
			{
				List<long> nums1 = (
					from x in estimateAdhocProducts
                    select x.Id).ToList();
				await _estimateAdhocProductRepository.DeleteAsync((EstimateAdhocProduct x) => x.EstimateId == estimateId && nums1.Contains(x.Id));
			}
			IRepository<EstimateAdjustment, long> repository6 = this._estimateAdjustmentRepository;
			List<EstimateAdjustment> estimateAdjustments = await _estimateAdjustmentRepository.GetAllListAsync((EstimateAdjustment x) => x.EstimateId == estimateId);
			if (input.Estimate.Adjustments.Any())
			{
				if (!inputEstimateIdIsSet)
				{
					if (!estimateAdjustments.Any())
					{
						foreach (EstimateAdjustment adjustment in input.Estimate.Adjustments)
						{
							adjustment.EstimateId = estimateId;
							await this._estimateAdjustmentRepository.InsertAsync(adjustment);
						}
					}
					else
					{
						List<long> list2 = (
							from x in estimateAdjustments
                            select x.Id).ToList<long>();
						foreach (EstimateAdjustment estimateAdjustment in input.Estimate.Adjustments)
						{
							if (estimateAdjustment.Id <= (long)0)
							{
								estimateAdjustment.EstimateId = estimateId;
								await this._estimateAdjustmentRepository.InsertAndGetIdAsync(estimateAdjustment);
							}
							else
							{
								EstimateAdjustment cost = await this._estimateAdjustmentRepository.GetAsync(estimateAdjustment.Id);
								cost.IsTaxable = estimateAdjustment.IsTaxable;
								cost.Cost = estimateAdjustment.Cost;
								cost.Description = estimateAdjustment.Description;
								cost.RetailCost = estimateAdjustment.RetailCost;
								cost.Name = estimateAdjustment.Name;
								await this._estimateAdjustmentRepository.UpdateAsync(cost);
								list2.Remove(cost.Id);
								cost = null;
							}
						}
						if (list2.Any<long>())
						{
							await _estimateAdjustmentRepository.DeleteAsync((EstimateAdjustment x) => x.EstimateId == estimateId && list2.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Estimate.Adjustments.Any() && estimateAdjustments.Any())
			{
				List<long> nums2 = (
					from x in estimateAdjustments
                    select x.Id).ToList<long>();
				await _estimateAdjustmentRepository.DeleteAsync((EstimateAdjustment x) => x.EstimateId == estimateId && nums2.Contains(x.Id));
			}
			List<EstimateProduct> estimateProducts = await _estimateProductRepository.GetAllListAsync((EstimateProduct w) => (long?)w.EstimateId == input.Estimate.Id);
			if (estimateProducts.Any())
			{
				List<long> estimateProductIds = (
					from s in estimateProducts
                    select s.Id).ToList<long>();
				foreach (EstimateProduct estimateProduct in estimateProducts)
				{
					if ((
						from m in input.Estimate.Products
						where m.ProductId == estimateProduct.ProductId
						select m).Any())
					{
						List<EstimateProductLineItem> estimateProductLineItems = await _estimateProductLineItemRepository.GetAllListAsync((EstimateProductLineItem x) => x.ProductId == estimateProduct.ProductId && x.EstimateId == estimateId);
						if (estimateProductLineItems.Any())
						{
							List<long> nums3 = (
								from s in estimateProductLineItems
                                select s.Id).ToList<long>();
							List<EstimateProductLineItemOption> estimateProductLineItemOptions = await _estimateProductLineItemOptionRepository.GetAllListAsync((EstimateProductLineItemOption x) => nums3.Contains(x.ProductLineItemId));
							if (estimateProductLineItemOptions.Any())
							{
								List<long> list4 = (
									from s in estimateProductLineItemOptions
									select s.Id).ToList<long>();
								await _estimateProductLineItemOptionRepository.DeleteAsync((EstimateProductLineItemOption x) => list4.Contains(x.Id));
							}
							await _estimateProductLineItemRepository.DeleteAsync((EstimateProductLineItem x) => nums3.Contains(x.Id));
						}
						IEnumerable<EstimateProductDto> products = 
							from m in input.Estimate.Products
							where m.ProductId == estimateProduct.ProductId
							select m;
						EstimateProductLineItem estimateProductLineItem = new EstimateProductLineItem()
						{
							EstimateId = estimateId,
							Cost = products.First().LineItem.Cost,
							Quantity = products.First().LineItem.Quantity,
							ProductId = estimateProduct.ProductId,
							Options = new List<EstimateProductLineItemOption>()
						};
						long newId = await _estimateProductLineItemRepository.InsertAndGetIdAsync(estimateProductLineItem);
						if (products.First().LineItem.Options.Any())
						{
							foreach (EstimateProductLineItemOptionDto option in products.First().LineItem.Options)
							{
								EstimateProductLineItemOption estimateProductLineItemOption = new EstimateProductLineItemOption()
								{
									ProductLineItemId = newId,
									ProductOptionId = option.ProductOptionId
								};
								await this._estimateProductLineItemOptionRepository.InsertAsync(estimateProductLineItemOption);
							}
						}
						input.Estimate.Products.Remove((
							from m in input.Estimate.Products
							where m.ProductId == estimateProduct.ProductId
							select m).First());
						estimateProductIds.RemoveAll((long x) => x == estimateProduct.Id);
					}
				}
				if (estimateProductIds.Any())
				{
					List<EstimateProduct> theEstimateProducts = await _estimateProductRepository.GetAllListAsync((EstimateProduct x) => estimateProductIds.Contains(x.Id));
					foreach (EstimateProduct ep in theEstimateProducts)
					{
						if (ep.LineItemId.HasValue)
						{
							id = ep.LineItemId;
							EstimateProductLineItem epi = await _estimateProductLineItemRepository.GetAsync(id.Value);
							if (epi != null && epi.Id > 0)
							{
								await _estimateProductLineItemOptionRepository.DeleteAsync((EstimateProductLineItemOption x) => x.ProductLineItemId == epi.Id);
							}
							await this._estimateProductLineItemRepository.DeleteAsync(epi);
						}
						await this._estimateProductRepository.DeleteAsync(ep);
					}
				}
			}
			ICollection<EstimateProductDto> estimateProductDtos = input.Estimate.Products;
			List<long> productIds = (
				from s in estimateProductDtos
				select s.ProductId).ToList();
			if (!inputEstimateIdIsSet && productIds.Any())
			{
				List<Product> products1 = await _productRepository.GetAllListAsync((Product m) => productIds.Contains(m.Id));
				if (products1.Any())
				{
					List<long> existingProductIds = (
						from s in products1
                        select s.Id).ToList();
					foreach (EstimateProductDto product in input.Estimate.Products)
					{
						if (existingProductIds.Contains(product.ProductId))
						{
							EstimateProduct estimateProduct2 = new EstimateProduct()
							{
								EstimateId = estimateId,
								ProductId = product.ProductId,
								IsActive = true
							};
							await this._estimateProductRepository.InsertAndGetIdAsync(estimateProduct2);
							EstimateProductLineItem estimateProductLineItem1 = new EstimateProductLineItem()
							{
								EstimateId = estimateId,
								Cost = product.LineItem.Cost,
								Quantity = product.LineItem.Quantity,
								ProductId = product.ProductId,
								Options = new List<EstimateProductLineItemOption>()
							};
							long num1 = await this._estimateProductLineItemRepository.InsertAndGetIdAsync(estimateProductLineItem1);
							if (product.LineItem.Options.Any())
							{
								foreach (EstimateProductLineItemOptionDto estimateProductLineItemOptionDto in product.LineItem.Options)
								{
									EstimateProductLineItemOption estimateProductLineItemOption1 = new EstimateProductLineItemOption()
									{
										ProductLineItemId = num1,
										ProductOptionId = estimateProductLineItemOptionDto.ProductOptionId
									};
									await this._estimateProductLineItemOptionRepository.InsertAsync(estimateProductLineItemOption1);
								}
							}
							num1 = (long)0;
						}
					}
					existingProductIds = null;
				}
			}
			return estimateId;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates.Delete" })]
		public async Task DeleteEstimate(IdInput<long> input)
		{
			await this._estimateRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates.Delete" })]
		public async Task DeleteEstimateResource(IdInput<long> input)
		{
			EstimateResource async = await this._estimateResourceRepository.GetAsync(input.Id);
			if (async != null)
			{
				await this._estimateResourceRepository.DeleteAsync(async.Id);
			}
		}

		private string GetCategoryFromFileExtension(string fileExt)
		{
			string empty = string.Empty;
			empty = (fileExt == ".pdf" || fileExt == ".xlsx" || fileExt == ".xls" || fileExt == ".docx" || fileExt == ".doc" || fileExt == ".zip" ? this.L("ResourceCategoryAttachment") : this.L("ResourceCategoryImage"));
			return empty;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates" })]
		public async Task<Estimate> GetEstimate(long estimateId)
		{
			return await this._estimateRepository.GetAsync(estimateId);
		}

		public async Task<GetEstimateForCopyOutput> GetEstimateForCopy(IdInput<long> input)
		{
			EstimateCopyDto estimateCopyDto = new EstimateCopyDto();
			Estimate async = await this._estimateRepository.GetAsync(input.Id);
			if (async != null)
			{
				estimateCopyDto = null;
				EstimateCopyDto estimateCopyDto1 = new EstimateCopyDto()
				{
					Customer = async.Customer,
					CustomerId = async.CustomerId,
					Label = async.Label,
					Number = async.Number,
					EstimateId = async.Id
				};
				estimateCopyDto = estimateCopyDto1;
			}
			return new GetEstimateForCopyOutput()
			{
				Estimate = estimateCopyDto
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates.Create", "Pages.Tenant.Estimates.Edit" })]
		public async Task<GetEstimateForEditOutput> GetEstimateForEdit(NullableIdInput<long> input)
		{
			EstimateEditDto estimateEditDto;
			if (!input.Id.HasValue)
			{
				estimateEditDto = new EstimateEditDto()
				{
					Tasks = new List<EstimateTask>(),
					Products = new List<EstimateProductDto>(),
					AdhocProducts = new List<EstimateAdhocProduct>(),
					Adjustments = new List<EstimateAdjustment>()
				};
			}
			else
			{
				IRepository<Estimate, long> repository = this._estimateRepository;
				long? id = input.Id;
				Estimate async = await repository.GetAsync(id.Value);
				estimateEditDto = async.MapTo<EstimateEditDto>();
				EstimateEditDto estimateEditDto1 = estimateEditDto;
				IRepository<EstimateTask, long> repository1 = this._estimateTaskRepository;
				List<EstimateTask> allListAsync = await repository1.GetAllListAsync((EstimateTask x) => x.EstimateId == async.Id);
				estimateEditDto1.Tasks = allListAsync;
				estimateEditDto1 = null;
				IRepository<EstimateProduct, long> repository2 = this._estimateProductRepository;
				List<EstimateProduct> estimateProducts = await repository2.GetAllListAsync((EstimateProduct x) => x.EstimateId == async.Id);
				estimateEditDto.Products = estimateProducts.MapTo<List<EstimateProductDto>>();
				if (estimateEditDto.Products.Any<EstimateProductDto>())
				{
					foreach (EstimateProductDto product in estimateEditDto.Products)
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
				estimateEditDto1 = estimateEditDto;
				IRepository<EstimateAdhocProduct, long> repository5 = this._estimateAdhocProductRepository;
				List<EstimateAdhocProduct> estimateAdhocProducts = await repository5.GetAllListAsync((EstimateAdhocProduct x) => x.EstimateId == async.Id);
				estimateEditDto1.AdhocProducts = estimateAdhocProducts;
				estimateEditDto1 = null;
				estimateEditDto1 = estimateEditDto;
				IRepository<EstimateAdjustment, long> repository6 = this._estimateAdjustmentRepository;
				List<EstimateAdjustment> estimateAdjustments = await repository6.GetAllListAsync((EstimateAdjustment x) => x.EstimateId == async.Id);
				estimateEditDto1.Adjustments = estimateAdjustments;
				estimateEditDto1 = null;
				estimateEditDto1 = estimateEditDto;
				Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
				estimateEditDto1.Customer = customer.MapTo<CustomerDto>();
				estimateEditDto1 = null;
				if (!async.CustomerAddressId.HasValue)
				{
					estimateEditDto.CustomerAddress = new AddressDto()
					{
						CountryRegion = new CountryRegionDto()
					};
				}
				else
				{
					estimateEditDto1 = estimateEditDto;
					IRepository<Address, long> repository7 = this._addressRepository;
					id = async.CustomerAddressId;
					Address address = await repository7.GetAsync(id.Value);
					estimateEditDto1.CustomerAddress = address.MapTo<AddressDto>();
					estimateEditDto1 = null;
					if (estimateEditDto.CustomerAddress.CountryRegionId.HasValue)
					{
						AddressDto customerAddress = estimateEditDto.CustomerAddress;
						IRepository<CountryRegion> repository8 = this._countryRegionRepository;
						int? countryRegionId = async.CustomerAddress.CountryRegionId;
						CountryRegion countryRegion = await repository8.GetAsync(countryRegionId.Value);
						customerAddress.CountryRegion = countryRegion.MapTo<CountryRegionDto>();
						customerAddress = null;
					}
				}
			}
			return new GetEstimateForEditOutput()
			{
				Estimate = estimateEditDto
			};
		}

		public async Task<EstimateResourceEditDto> GetEstimateResourceDetailsByBinaryObjectId(Guid resourceId)
		{
			IRepository<EstimateResource, long> repository = this._estimateResourceRepository;
			EstimateResource estimateResource = await repository.FirstOrDefaultAsync((EstimateResource m) => m.BinaryObjectId == resourceId);
			EstimateResource estimateResource1 = estimateResource;
			if (estimateResource1 == null)
			{
				throw new Exception("Estimate Resource was not found in GetEstimateResourceDetailsByBinaryObjectId();");
			}
			return estimateResource1.MapTo<EstimateResourceEditDto>();
		}

		public async Task<GetEstimateResourceForEditOutput> GetEstimateResourcesForEdit(NullableIdInput<long> input)
		{
			List<EstimateResourceEditDto> estimateResourceEditDtos = new List<EstimateResourceEditDto>();
			if (input.Id.HasValue)
			{
				IRepository<EstimateResource, long> repository = this._estimateResourceRepository;
				List<EstimateResource> allListAsync = await repository.GetAllListAsync((EstimateResource m) => m.EstimateId == input.Id.Value);
				List<EstimateResource> estimateResources = allListAsync;
				if (estimateResources.Any<EstimateResource>())
				{
					estimateResourceEditDtos = estimateResources.MapTo<List<EstimateResourceEditDto>>();
				}
			}
			return new GetEstimateResourceForEditOutput()
			{
				EstimateResources = estimateResourceEditDtos
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates" })]
		public async Task<PagedResultOutput<EstimateListDto>> GetEstimates(GetEstimatesInput input)
		{
			//EstimateAppService.<>c__DisplayClass19_2 variable = null;
			//EstimateAppService.<>c__DisplayClass19_1 variable1 = null;
			//EstimateAppService.<>c__DisplayClass19_0 variable2 = null;
			int num;
			var all = this._estimateRepository.GetAll();
            var parameterExpression = Expression.Parameter(typeof(Estimate), "p");
            IQueryable<Estimate> estimates = all.Where(p => ((int?)p.TenantId) == AbpSession.TenantId && p.Id == 0);
            var listAsync = await estimates.Select(s => new
            {
                EstimateId = s.Id
            }).ToListAsync();
			var collection = listAsync;
			List<long> foundEstimateIdsFromInputFilter = (
				from s in collection
				select s.EstimateId).ToList();
			bool flag = false;
			if (input.Filter.ToLower().StartsWith("id:"))
			{
				try
				{
					string lower = input.Filter.ToLower();
					char[] chrArray = new char[] { ':' };
					int.TryParse(lower.Split(chrArray)[1].ToString(), out num);
                    
                    IQueryable<Estimate> estimates1 = all.Where(p => ((int?)p.TenantId) == AbpSession.TenantId && p.Id == (long)num);

					var listAsync1 = await estimates1.Select(s => new
                    {
                        EstimateId = s.Id
                    }).ToListAsync();
					flag = true;
					foundEstimateIdsFromInputFilter = foundEstimateIdsFromInputFilter.Union(
						from s in listAsync1
                        select s.EstimateId).ToList<long>();
				}
				catch (Exception)
				{
				}
			}
			if (!flag)
			{
                IQueryable<Estimate> filteredEstimates = all.Where(p => 
                    p.Label.Contains(input.Filter) || 
					p.Number.Contains(input.Filter) || 
					p.PONumber.Contains(input.Filter) || 
					p.TimeEntryLog.Contains(input.Filter) || 
					p.CurrentStatus.Contains(input.Filter) || 
					p.Description.Contains(input.Filter) || 
					p.Terms.Contains(input.Filter) || 
					p.BillingType.Contains(input.Filter) || 
					p.Customer.Email.Contains(input.Filter) || 
					p.Customer.FirstName.Contains(input.Filter) || 
					p.Customer.LastName.Contains(input.Filter) || 
					p.Customer.BusinessName.Contains(input.Filter) || 
					input.Filter == String.Concat(p.Customer.FirstName, " ", p.Customer.LastName));
				var listAsync2 = await filteredEstimates.Select(s => new
                {
                    EstimateId = s.Id
                }).ToListAsync();
				foundEstimateIdsFromInputFilter = foundEstimateIdsFromInputFilter.Union(
					from s in listAsync2
                    select s.EstimateId).ToList<long>();
			}
            IQueryable<Estimate> estimates3 = all.Where(m => foundEstimateIdsFromInputFilter.Contains(m.Id));
			int num1 = await estimates3.CountAsync();
			if (input.Sorting.Contains("customer"))
			{
				input.Sorting = input.Sorting.Replace("customer", "customerid");
			}
			List<Estimate> listAsync3 = await estimates3.OrderBy(input.Sorting, new object[0]).PageBy(input).ToListAsync();
			List<Estimate> estimates4 = listAsync3;
			foreach (Estimate estimate in estimates4)
			{
                int num2 = await _estimateTaskRepository.CountAsync(m => m.EstimateId == estimate.Id);
				estimate.TaskTotal = num2;
			}
			return new PagedResultOutput<EstimateListDto>(num1, estimates4.MapTo<List<EstimateListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates.ExportData" })]
		public async Task<FileDto> GetEstimatesToExcel()
		{
			List<Estimate> allListAsync = await this._estimateRepository.GetAllListAsync();
			List<EstimateListDto> estimateListDtos = allListAsync.MapTo<List<EstimateListDto>>();
			return this._estimateListExcelExporter.ExportToFile(estimateListDtos);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Estimates.Create", "Pages.Tenant.Estimates.Edit" })]
		public async Task SaveEstimateResourceAsync(UpdateEstimateResourceInput input)
		{
			Guid? resourceId;
			if (input.Id <= (long)0)
			{
				EstimateResource estimateResource = new EstimateResource()
				{
					Id = (long)0
				};
				resourceId = input.ResourceId;
				estimateResource.BinaryObjectId = Guid.Parse(resourceId.ToString());
				estimateResource.EstimateId = input.EstimateId;
				estimateResource.Name = input.FileName;
				estimateResource.FileName = input.FileName;
				estimateResource.FileExtension = input.FileExtension;
				estimateResource.FileSize = input.FileSize;
				estimateResource.Category = this.GetCategoryFromFileExtension(input.FileExtension);
				estimateResource.IsActive = input.IsActive;
				await this._estimateResourceRepository.InsertAsync(estimateResource);
			}
			else
			{
				EstimateResource async = await this._estimateResourceRepository.GetAsync(input.Id);
				if (async != null)
				{
					resourceId = input.ResourceId;
					async.BinaryObjectId = Guid.Parse(resourceId.ToString());
					async.EstimateId = input.EstimateId;
					async.FileExtension = input.FileExtension;
					async.FileName = input.FileName;
					async.FileSize = input.FileSize;
					async.Category = this.GetCategoryFromFileExtension(input.FileExtension);
					async.IsActive = input.IsActive;
				}
				await this._estimateResourceRepository.UpdateAsync(async);
			}
		}

		public async Task SaveEstimateResourceDetails(long id, string name, string description, bool isActive)
		{
			EstimateResource async = await this._estimateResourceRepository.GetAsync(id);
			if (async != null)
			{
				async.Description = description;
			}
			await this._estimateResourceRepository.UpdateAsync(async);
		}
	}
}