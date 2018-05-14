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
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using FuelWerx;
using FuelWerx.Authorization.Users;
using FuelWerx.Authorization.Users.Dto;
using FuelWerx.Customers;
using FuelWerx.Customers.Dto;
using FuelWerx.Dto;
using FuelWerx.Estimates;
using FuelWerx.Generic;
using FuelWerx.Generic.Dto;
using FuelWerx.Invoices;
using FuelWerx.Products;
using FuelWerx.Products.Dto;
using FuelWerx.Products.Prices.Dto;
using FuelWerx.Projects.Dto;
using FuelWerx.Projects.Exporting;
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

namespace FuelWerx.Projects
{
	public class ProjectAppService : FuelWerxAppServiceBase, IProjectAppService, IApplicationService, ITransientDependency
	{
		private readonly IRepository<Project, long> _projectRepository;

		private readonly IRepository<ProjectTask, long> _projectTaskRepository;

		private readonly IRepository<Product, long> _productRepository;

		private readonly IRepository<ProjectProduct, long> _projectProductRepository;

		private readonly IRepository<ProjectAdhocProduct, long> _projectAdhocProductRepository;

		private readonly IRepository<ProjectAdjustment, long> _projectAdjustmentRepository;

		private readonly IRepository<ProjectTeamMember, long> _projectTeamMemberRepository;

		private readonly IRepository<ProductPrice, long> _productPriceRepository;

		private readonly IRepository<ProjectResource, long> _projectResourceRespository;

		private readonly IRepository<ProductOption, long> _productOptionRepository;

		private readonly IRepository<ProjectProductLineItem, long> _projectProductLineItemRepository;

		private readonly IRepository<ProjectProductLineItemOption, long> _projectProductLineItemOptionRepository;

		private readonly IRepository<Customer, long> _customerRepository;

		private readonly IRepository<Invoice, long> _invoiceRepository;

		private readonly IRepository<User, long> _userRepository;

		private readonly IProjectListExcelExporter _projectListExcelExporter;

		private readonly IRepository<ProjectResource, long> _projectResourceRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IBinaryObjectManager _binaryObjectManager;

		public ProjectAppService(IRepository<Project, long> projectRepository, IRepository<ProjectTask, long> projectOptionRepository, IProjectListExcelExporter projectListExcelExporter, IRepository<ProjectTeamMember, long> projectTeamMemberRepository, IRepository<Customer, long> customerRepository, IRepository<User, long> userRepository, IRepository<ProjectResource, long> projectResourceRepository, IBinaryObjectManager binaryObjectManager, IRepository<ProjectResource, long> projectResourceRespository, IRepository<Invoice, long> invoiceRepository, IRepository<ProjectProduct, long> projectProductRepository, IRepository<Product, long> productRepository, IRepository<ProjectAdhocProduct, long> projectAdhocProductRepository, IRepository<ProjectAdjustment, long> projectAdjustmentRepository, IRepository<ProductPrice, long> productPriceRepository, IRepository<ProductOption, long> productOptionRepository, IRepository<ProjectProductLineItem, long> projectProductLineItemRepository, IRepository<CountryRegion> countryRegionRepository, IRepository<Address, long> addressRepository, IRepository<ProjectProductLineItemOption, long> projectProductLineItemOptionRepository)
		{
			this._projectRepository = projectRepository;
			this._projectTaskRepository = projectOptionRepository;
			this._projectListExcelExporter = projectListExcelExporter;
			this._projectTeamMemberRepository = projectTeamMemberRepository;
			this._userRepository = userRepository;
			this._projectResourceRepository = projectResourceRepository;
			this._customerRepository = customerRepository;
			this._binaryObjectManager = binaryObjectManager;
			this._invoiceRepository = invoiceRepository;
			this._projectResourceRespository = projectResourceRespository;
			this._productOptionRepository = productOptionRepository;
			this._projectProductRepository = projectProductRepository;
			this._countryRegionRepository = countryRegionRepository;
			this._productRepository = productRepository;
			this._productPriceRepository = productPriceRepository;
			this._projectProductLineItemRepository = projectProductLineItemRepository;
			this._projectAdhocProductRepository = projectAdhocProductRepository;
			this._projectAdjustmentRepository = projectAdjustmentRepository;
			this._addressRepository = addressRepository;
			this._projectProductLineItemOptionRepository = projectProductLineItemOptionRepository;
		}

		public async Task ConvertToInvoice(IdInput<long> input)
		{
			Project async = await this._projectRepository.GetAsync(input.Id);
			if (async != null)
			{
				IRepository<ProjectTask, long> repository = this._projectTaskRepository;
				List<ProjectTask> allListAsync = await repository.GetAllListAsync((ProjectTask m) => m.ProjectId == async.Id);
				List<ProjectTask> projectTasks = allListAsync;
				IRepository<ProjectProduct, long> repository1 = this._projectProductRepository;
				List<ProjectProduct> projectProducts = await repository1.GetAllListAsync((ProjectProduct m) => m.ProjectId == async.Id);
				List<ProjectProduct> projectProducts1 = projectProducts;
				IRepository<ProjectAdhocProduct, long> repository2 = this._projectAdhocProductRepository;
				List<ProjectAdhocProduct> projectAdhocProducts = await repository2.GetAllListAsync((ProjectAdhocProduct m) => m.ProjectId == async.Id);
				List<ProjectAdhocProduct> projectAdhocProducts1 = projectAdhocProducts;
				IRepository<ProjectAdjustment, long> repository3 = this._projectAdjustmentRepository;
				List<ProjectAdjustment> projectAdjustments = await repository3.GetAllListAsync((ProjectAdjustment m) => m.ProjectId == async.Id);
				List<ProjectAdjustment> projectAdjustments1 = projectAdjustments;
				IRepository<ProjectResource, long> repository4 = this._projectResourceRespository;
				List<ProjectResource> projectResources = await repository4.GetAllListAsync((ProjectResource m) => m.ProjectId == async.Id);
				List<ProjectResource> projectResources1 = projectResources;
				IRepository<ProjectTeamMember, long> repository5 = this._projectTeamMemberRepository;
				List<ProjectTeamMember> projectTeamMembers = await repository5.GetAllListAsync((ProjectTeamMember m) => m.ProjectId == async.Id);
				List<ProjectTeamMember> projectTeamMembers1 = projectTeamMembers;
				IMappingExpression<Project, Invoice> mappingExpression = Mapper.CreateMap<Project, Invoice>();
				Expression<Func<Invoice, object>> id = (Invoice p) => (object)p.Id;
				IMappingExpression<Project, Invoice> mappingExpression1 = mappingExpression.ForMember(id, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> creationTime = (Invoice p) => (object)p.CreationTime;
				IMappingExpression<Project, Invoice> mappingExpression2 = mappingExpression1.ForMember(creationTime, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> creatorUserId = (Invoice p) => (object)p.CreatorUserId;
				IMappingExpression<Project, Invoice> mappingExpression3 = mappingExpression2.ForMember(creatorUserId, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> deletionTime = (Invoice p) => (object)p.DeletionTime;
				IMappingExpression<Project, Invoice> mappingExpression4 = mappingExpression3.ForMember(deletionTime, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> deleterUserId = (Invoice p) => (object)p.DeleterUserId;
				IMappingExpression<Project, Invoice> mappingExpression5 = mappingExpression4.ForMember(deleterUserId, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> lastModifierUserId = (Invoice p) => (object)p.LastModifierUserId;
				IMappingExpression<Project, Invoice> mappingExpression6 = mappingExpression5.ForMember(lastModifierUserId, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> lastModificationTime = (Invoice p) => (object)p.LastModificationTime;
				IMappingExpression<Project, Invoice> mappingExpression7 = mappingExpression6.ForMember(lastModificationTime, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> tasks = (Invoice p) => p.Tasks;
				IMappingExpression<Project, Invoice> mappingExpression8 = mappingExpression7.ForMember(tasks, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> resources = (Invoice p) => p.Resources;
				IMappingExpression<Project, Invoice> mappingExpression9 = mappingExpression8.ForMember(resources, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> taskTotal = (Invoice p) => (object)p.TaskTotal;
				IMappingExpression<Project, Invoice> mappingExpression10 = mappingExpression9.ForMember(taskTotal, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> teamMembers = (Invoice p) => p.TeamMembers;
				IMappingExpression<Project, Invoice> mappingExpression11 = mappingExpression10.ForMember(teamMembers, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> products = (Invoice p) => p.Products;
				IMappingExpression<Project, Invoice> mappingExpression12 = mappingExpression11.ForMember(products, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> adhocProducts = (Invoice p) => p.AdhocProducts;
				IMappingExpression<Project, Invoice> mappingExpression13 = mappingExpression12.ForMember(adhocProducts, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> adjustments = (Invoice p) => p.Adjustments;
				IMappingExpression<Project, Invoice> mappingExpression14 = mappingExpression13.ForMember(adjustments, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Expression<Func<Invoice, object>> expression = (Invoice p) => (object)p.Id;
				mappingExpression14.ForMember(expression, (IMemberConfigurationExpression<Project> options) => options.Ignore());
				Invoice nullable = Mapper.Map<Project, Invoice>(async, new Invoice());
				nullable.Id = (long)0;
				nullable.ProjectId = new long?(async.Id);
				nullable.IsActive = true;
				nullable.IsDeleted = false;
				List<InvoiceTask> invoiceTasks = new List<InvoiceTask>();
				foreach (ProjectTask projectTask in projectTasks)
				{
					InvoiceTask invoiceTask = new InvoiceTask()
					{
						InvoiceId = (long)0,
						Name = projectTask.Name,
						Comment = projectTask.Comment,
						Cost = projectTask.Cost,
						Retail = projectTask.Retail,
						Discount = projectTask.Discount,
						IsComplete = projectTask.IsComplete,
						IsActive = true
					};
					invoiceTasks.Add(invoiceTask);
				}
				if (invoiceTasks.Any<InvoiceTask>())
				{
					nullable.Tasks = invoiceTasks;
				}
				List<InvoiceAdhocProduct> invoiceAdhocProducts = new List<InvoiceAdhocProduct>();
				foreach (ProjectAdhocProduct projectAdhocProduct in projectAdhocProducts1)
				{
					InvoiceAdhocProduct invoiceAdhocProduct = new InvoiceAdhocProduct()
					{
						InvoiceId = (long)0,
						Name = projectAdhocProduct.Name,
						BaseCost = projectAdhocProduct.BaseCost,
						Cost = projectAdhocProduct.Cost,
						Description = projectAdhocProduct.Description,
						IsTaxable = projectAdhocProduct.IsTaxable,
						RetailCost = projectAdhocProduct.RetailCost,
						IsActive = true
					};
					invoiceAdhocProducts.Add(invoiceAdhocProduct);
				}
				if (invoiceAdhocProducts.Any<InvoiceAdhocProduct>())
				{
					nullable.AdhocProducts = invoiceAdhocProducts;
				}
				List<InvoiceAdjustment> invoiceAdjustments = new List<InvoiceAdjustment>();
				foreach (ProjectAdjustment projectAdjustment in projectAdjustments1)
				{
					InvoiceAdjustment invoiceAdjustment = new InvoiceAdjustment()
					{
						InvoiceId = (long)0,
						Name = projectAdjustment.Name,
						Cost = projectAdjustment.Cost,
						Description = projectAdjustment.Description,
						IsTaxable = projectAdjustment.IsTaxable,
						RetailCost = projectAdjustment.RetailCost,
						IsActive = true
					};
					invoiceAdjustments.Add(invoiceAdjustment);
				}
				if (invoiceAdjustments.Any<InvoiceAdjustment>())
				{
					nullable.Adjustments = invoiceAdjustments;
				}
				List<InvoiceProduct> invoiceProducts = new List<InvoiceProduct>();
				foreach (ProjectProduct projectProduct in projectProducts1)
				{
					InvoiceProduct invoiceProduct = new InvoiceProduct()
					{
						InvoiceId = (long)0,
						ProductId = projectProduct.ProductId,
						IsActive = true
					};
					InvoiceProduct invoiceProduct1 = invoiceProduct;
					if (projectProduct.LineItem != null)
					{
						InvoiceProductLineItem invoiceProductLineItem = new InvoiceProductLineItem()
						{
							ProductId = projectProduct.ProductId,
							Cost = projectProduct.LineItem.Cost,
							Quantity = projectProduct.LineItem.Quantity,
							Options = new List<InvoiceProductLineItemOption>(),
							InvoiceId = (long)0
						};
						InvoiceProductLineItem invoiceProductLineItem1 = invoiceProductLineItem;
						if (projectProduct.LineItem.Options.Any<ProjectProductLineItemOption>())
						{
							foreach (ProjectProductLineItemOption option in projectProduct.LineItem.Options)
							{
								InvoiceProductLineItemOption invoiceProductLineItemOption = new InvoiceProductLineItemOption()
								{
									ProductOptionId = option.ProductOptionId
								};
								invoiceProductLineItem1.Options.Add(invoiceProductLineItemOption);
							}
						}
						invoiceProduct1.LineItem = invoiceProductLineItem1;
					}
					invoiceProducts.Add(invoiceProduct1);
				}
				if (invoiceProducts.Any<InvoiceProduct>())
				{
					nullable.Products = invoiceProducts;
				}
				List<InvoiceResource> invoiceResources = new List<InvoiceResource>();
				foreach (ProjectResource projectResource in projectResources1)
				{
					BinaryObject orNullAsync = await this._binaryObjectManager.GetOrNullAsync(projectResource.BinaryObjectId);
					if (orNullAsync != null)
					{
						BinaryObject binaryObject = new BinaryObject()
						{
							Bytes = orNullAsync.Bytes
						};
						InvoiceResource invoiceResource = new InvoiceResource()
						{
							InvoiceId = (long)0,
							Name = projectResource.Name,
							Category = projectResource.Category,
							Description = projectResource.Description,
							FileExtension = projectResource.FileExtension,
							FileName = projectResource.FileName,
							FileSize = projectResource.FileSize,
							BinaryObject = binaryObject,
							BinaryObjectId = Guid.Empty,
							IsDeleted = false,
							IsActive = true
						};
						invoiceResources.Add(invoiceResource);
					}
				}
				if (invoiceResources.Any<InvoiceResource>())
				{
					nullable.Resources = invoiceResources;
				}
				List<InvoiceTeamMember> invoiceTeamMembers = new List<InvoiceTeamMember>();
				foreach (ProjectTeamMember projectTeamMember in projectTeamMembers1)
				{
					InvoiceTeamMember invoiceTeamMember = new InvoiceTeamMember()
					{
						InvoiceId = (long)0,
						TeamMemberId = projectTeamMember.TeamMemberId
					};
				}
				if (invoiceTeamMembers.Any<InvoiceTeamMember>())
				{
					nullable.TeamMembers = invoiceTeamMembers;
				}
				long num = await this._invoiceRepository.InsertAndGetIdAsync(nullable);
				if (num > (long)0)
				{
					async.IsActive = false;
					async.InvoiceAcceptedTime = new DateTime?(DateTime.Now);
					async.InvoiceId = new long?(num);
					await this._projectRepository.UpdateAsync(async);
				}
				projectTasks = null;
				projectProducts1 = null;
				projectAdhocProducts1 = null;
				projectAdjustments1 = null;
				projectResources1 = null;
				projectTeamMembers1 = null;
				nullable = null;
				invoiceResources = null;
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects.Create", "Pages.Tenant.Projects.Edit" })]
		public async Task<long> CreateOrUpdateProject(CreateOrUpdateProjectInput input)
		{
			long? id;
			bool newProject;
			long recordId;
			if (!input.Project.Id.HasValue)
			{
				if (!this.PermissionChecker.IsGranted("Pages.Tenant.Projects.Create"))
				{
					throw new UserFriendlyException(this.L("Permissions_UserNotAuthorizedMessage"));
				}
				newProject = true;
				Project project = new Project();
				Mapper.Map<ProjectEditDto, Project>(input.Project, project);
				recordId = await this._projectRepository.InsertAndGetIdAsync(project);
			}
			else
			{
				if (!this.PermissionChecker.IsGranted("Pages.Tenant.Projects.Edit"))
				{
					throw new UserFriendlyException(this.L("Permissions_UserNotAuthorizedMessage"));
				}
				newProject = false;
				id = input.Project.Id;
				recordId = id.Value;
				Project project1 = new Project();
				Mapper.Map<ProjectEditDto, Project>(input.Project, project1);
				project1.Tasks = null;
				project1.Products = null;
				project1.AdhocProducts = null;
				project1.Adjustments = null;
				await this._projectRepository.UpdateAsync(project1);
			}
			IRepository<ProjectTask, long> repository = this._projectTaskRepository;
			List<ProjectTask> allListAsync = await repository.GetAllListAsync((ProjectTask x) => x.ProjectId == recordId);
			List<ProjectTask> projectTasks = allListAsync;
			if (input.Project.Tasks.Any<ProjectTask>())
			{
				if (!newProject)
				{
					if (!projectTasks.Any<ProjectTask>())
					{
						foreach (ProjectTask task in input.Project.Tasks)
						{
							task.ProjectId = recordId;
							await this._projectTaskRepository.InsertAsync(task);
						}
					}
					else
					{
						List<ProjectTask> projectTasks1 = projectTasks;
						List<long> list = (
							from x in projectTasks1
							select x.Id).ToList<long>();
						foreach (ProjectTask projectTask in input.Project.Tasks)
						{
							if (projectTask.Id <= (long)0)
							{
								projectTask.ProjectId = recordId;
								await this._projectTaskRepository.InsertAndGetIdAsync(projectTask);
							}
							else
							{
								ProjectTask async = await this._projectTaskRepository.GetAsync(projectTask.Id);
								async.IsComplete = projectTask.IsComplete;
								async.Cost = projectTask.Cost;
								async.Comment = projectTask.Comment;
								async.Retail = projectTask.Retail;
								async.Name = projectTask.Name;
								async.Discount = projectTask.Discount;
								await this._projectTaskRepository.UpdateAsync(async);
								list.Remove(async.Id);
								async = null;
							}
						}
						if (list.Any<long>())
						{
							IRepository<ProjectTask, long> repository1 = this._projectTaskRepository;
							await repository1.DeleteAsync((ProjectTask x) => x.ProjectId == recordId && list.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Project.Tasks.Any<ProjectTask>() && projectTasks.Any<ProjectTask>())
			{
				List<ProjectTask> projectTasks2 = projectTasks;
				List<long> nums = (
					from x in projectTasks2
					select x.Id).ToList<long>();
				IRepository<ProjectTask, long> repository2 = this._projectTaskRepository;
				await repository2.DeleteAsync((ProjectTask x) => x.ProjectId == recordId && nums.Contains(x.Id));
			}
			IRepository<ProjectAdhocProduct, long> repository3 = this._projectAdhocProductRepository;
			List<ProjectAdhocProduct> projectAdhocProducts = await repository3.GetAllListAsync((ProjectAdhocProduct x) => x.ProjectId == recordId);
			List<ProjectAdhocProduct> projectAdhocProducts1 = projectAdhocProducts;
			if (input.Project.AdhocProducts.Any<ProjectAdhocProduct>())
			{
				if (!newProject)
				{
					if (!projectAdhocProducts1.Any<ProjectAdhocProduct>())
					{
						foreach (ProjectAdhocProduct adhocProduct in input.Project.AdhocProducts)
						{
							adhocProduct.ProjectId = recordId;
							await this._projectAdhocProductRepository.InsertAsync(adhocProduct);
						}
					}
					else
					{
						List<ProjectAdhocProduct> projectAdhocProducts2 = projectAdhocProducts1;
						List<long> list1 = (
							from x in projectAdhocProducts2
							select x.Id).ToList<long>();
						foreach (ProjectAdhocProduct projectAdhocProduct in input.Project.AdhocProducts)
						{
							if (projectAdhocProduct.Id <= (long)0)
							{
								projectAdhocProduct.ProjectId = recordId;
								await this._projectAdhocProductRepository.InsertAndGetIdAsync(projectAdhocProduct);
							}
							else
							{
								ProjectAdhocProduct isTaxable = await this._projectAdhocProductRepository.GetAsync(projectAdhocProduct.Id);
								isTaxable.IsTaxable = projectAdhocProduct.IsTaxable;
								isTaxable.Cost = projectAdhocProduct.Cost;
								isTaxable.Description = projectAdhocProduct.Description;
								isTaxable.RetailCost = projectAdhocProduct.RetailCost;
								isTaxable.Name = projectAdhocProduct.Name;
								isTaxable.BaseCost = projectAdhocProduct.BaseCost;
								await this._projectAdhocProductRepository.UpdateAsync(isTaxable);
								list1.Remove(isTaxable.Id);
								isTaxable = null;
							}
						}
						if (list1.Any<long>())
						{
							IRepository<ProjectAdhocProduct, long> repository4 = this._projectAdhocProductRepository;
							await repository4.DeleteAsync((ProjectAdhocProduct x) => x.ProjectId == recordId && list1.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Project.AdhocProducts.Any<ProjectAdhocProduct>() && projectAdhocProducts1.Any<ProjectAdhocProduct>())
			{
				List<ProjectAdhocProduct> projectAdhocProducts3 = projectAdhocProducts1;
				List<long> nums1 = (
					from x in projectAdhocProducts3
					select x.Id).ToList<long>();
				IRepository<ProjectAdhocProduct, long> repository5 = this._projectAdhocProductRepository;
				await repository5.DeleteAsync((ProjectAdhocProduct x) => x.ProjectId == recordId && nums1.Contains(x.Id));
			}
			IRepository<ProjectAdjustment, long> repository6 = this._projectAdjustmentRepository;
			List<ProjectAdjustment> projectAdjustments = await repository6.GetAllListAsync((ProjectAdjustment x) => x.ProjectId == recordId);
			List<ProjectAdjustment> projectAdjustments1 = projectAdjustments;
			if (input.Project.Adjustments.Any<ProjectAdjustment>())
			{
				if (!newProject)
				{
					if (!projectAdjustments1.Any<ProjectAdjustment>())
					{
						foreach (ProjectAdjustment adjustment in input.Project.Adjustments)
						{
							adjustment.ProjectId = recordId;
							await this._projectAdjustmentRepository.InsertAsync(adjustment);
						}
					}
					else
					{
						List<ProjectAdjustment> projectAdjustments2 = projectAdjustments1;
						List<long> list2 = (
							from x in projectAdjustments2
							select x.Id).ToList<long>();
						foreach (ProjectAdjustment projectAdjustment in input.Project.Adjustments)
						{
							if (projectAdjustment.Id <= (long)0)
							{
								projectAdjustment.ProjectId = recordId;
								await this._projectAdjustmentRepository.InsertAndGetIdAsync(projectAdjustment);
							}
							else
							{
								ProjectAdjustment cost = await this._projectAdjustmentRepository.GetAsync(projectAdjustment.Id);
								cost.IsTaxable = projectAdjustment.IsTaxable;
								cost.Cost = projectAdjustment.Cost;
								cost.Description = projectAdjustment.Description;
								cost.RetailCost = projectAdjustment.RetailCost;
								cost.Name = projectAdjustment.Name;
								await this._projectAdjustmentRepository.UpdateAsync(cost);
								list2.Remove(cost.Id);
								cost = null;
							}
						}
						if (list2.Any<long>())
						{
							IRepository<ProjectAdjustment, long> repository7 = this._projectAdjustmentRepository;
							await repository7.DeleteAsync((ProjectAdjustment x) => x.ProjectId == recordId && list2.Contains(x.Id));
						}
					}
				}
			}
			else if (!input.Project.Adjustments.Any<ProjectAdjustment>() && projectAdjustments1.Any<ProjectAdjustment>())
			{
				List<ProjectAdjustment> projectAdjustments3 = projectAdjustments1;
				List<long> nums2 = (
					from x in projectAdjustments3
					select x.Id).ToList<long>();
				IRepository<ProjectAdjustment, long> repository8 = this._projectAdjustmentRepository;
				await repository8.DeleteAsync((ProjectAdjustment x) => x.ProjectId == recordId && nums2.Contains(x.Id));
			}
			IRepository<ProjectProduct, long> repository9 = this._projectProductRepository;
			List<ProjectProduct> projectProducts = await repository9.GetAllListAsync((ProjectProduct w) => (long?)w.ProjectId == input.Project.Id);
			List<ProjectProduct> projectProducts1 = projectProducts;
			if (projectProducts1.Any<ProjectProduct>())
			{
				List<ProjectProduct> projectProducts2 = projectProducts1;
				List<long> list3 = (
					from s in projectProducts2
					select s.Id).ToList<long>();
				foreach (ProjectProduct projectProduct in projectProducts1)
				{
					if ((
						from m in input.Project.Products
						where m.ProductId == projectProduct.ProductId
						select m).Any<ProjectProductDto>())
					{
						IRepository<ProjectProductLineItem, long> repository10 = this._projectProductLineItemRepository;
						List<ProjectProductLineItem> projectProductLineItems = await repository10.GetAllListAsync((ProjectProductLineItem x) => x.ProductId == projectProduct.ProductId && x.ProjectId == recordId);
						List<ProjectProductLineItem> projectProductLineItems1 = projectProductLineItems;
						if (projectProductLineItems1.Any<ProjectProductLineItem>())
						{
							List<ProjectProductLineItem> projectProductLineItems2 = projectProductLineItems1;
							List<long> nums3 = (
								from s in projectProductLineItems2
								select s.Id).ToList<long>();
							IRepository<ProjectProductLineItemOption, long> repository11 = this._projectProductLineItemOptionRepository;
							List<ProjectProductLineItemOption> projectProductLineItemOptions = await repository11.GetAllListAsync((ProjectProductLineItemOption x) => nums3.Contains(x.ProductLineItemId));
							List<ProjectProductLineItemOption> projectProductLineItemOptions1 = projectProductLineItemOptions;
							if (projectProductLineItemOptions1.Any<ProjectProductLineItemOption>())
							{
								List<ProjectProductLineItemOption> projectProductLineItemOptions2 = projectProductLineItemOptions1;
								List<long> list4 = (
									from s in projectProductLineItemOptions2
									select s.Id).ToList<long>();
								IRepository<ProjectProductLineItemOption, long> repository12 = this._projectProductLineItemOptionRepository;
								await repository12.DeleteAsync((ProjectProductLineItemOption x) => list4.Contains(x.Id));
							}
							IRepository<ProjectProductLineItem, long> repository13 = this._projectProductLineItemRepository;
							await repository13.DeleteAsync((ProjectProductLineItem x) => nums3.Contains(x.Id));
						}
						IEnumerable<ProjectProductDto> products =
							from m in input.Project.Products
							where m.ProductId == projectProduct.ProductId
							select m;
						ProjectProductLineItem projectProductLineItem = new ProjectProductLineItem()
						{
							ProjectId = recordId,
							Cost = products.First<ProjectProductDto>().LineItem.Cost,
							Quantity = products.First<ProjectProductDto>().LineItem.Quantity,
							ProductId = projectProduct.ProductId,
							Options = new List<ProjectProductLineItemOption>()
						};
						long num = await this._projectProductLineItemRepository.InsertAndGetIdAsync(projectProductLineItem);
						if (products.First<ProjectProductDto>().LineItem.Options.Any<ProjectProductLineItemOptionDto>())
						{
							foreach (ProjectProductLineItemOptionDto option in products.First<ProjectProductDto>().LineItem.Options)
							{
								ProjectProductLineItemOption projectProductLineItemOption = new ProjectProductLineItemOption()
								{
									ProductLineItemId = num,
									ProductOptionId = option.ProductOptionId
								};
								await this._projectProductLineItemOptionRepository.InsertAsync(projectProductLineItemOption);
							}
						}
						input.Project.Products.Remove((
							from m in input.Project.Products
							where m.ProductId == projectProduct.ProductId
							select m).First<ProjectProductDto>());
						list3.RemoveAll((long x) => x == projectProduct.Id);
						products = null;
					}
				}
				if (list3.Any<long>())
				{
					IRepository<ProjectProduct, long> repository14 = this._projectProductRepository;
					List<ProjectProduct> allListAsync1 = await repository14.GetAllListAsync((ProjectProduct x) => list3.Contains(x.Id));
					foreach (ProjectProduct projectProduct1 in allListAsync1)
					{
						if (projectProduct1.LineItemId.HasValue)
						{
							IRepository<ProjectProductLineItem, long> repository15 = this._projectProductLineItemRepository;
							id = projectProduct1.LineItemId;
							ProjectProductLineItem async1 = await repository15.GetAsync(id.Value);
							if (async1 != null && async1.Id > (long)0)
							{
								IRepository<ProjectProductLineItemOption, long> repository16 = this._projectProductLineItemOptionRepository;
								await repository16.DeleteAsync((ProjectProductLineItemOption x) => x.ProductLineItemId == async1.Id);
							}
							await this._projectProductLineItemRepository.DeleteAsync(async1);
						}
						await this._projectProductRepository.DeleteAsync(projectProduct1);
					}
				}
			}
			ICollection<ProjectProductDto> projectProductDtos = input.Project.Products;
			List<long> nums4 = (
				from s in projectProductDtos
				select s.ProductId).ToList<long>();
			if (!newProject && nums4.Any<long>())
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
					foreach (ProjectProductDto product in input.Project.Products)
					{
						if (list5.Contains(product.ProductId))
						{
							ProjectProduct projectProduct2 = new ProjectProduct()
							{
								ProjectId = recordId,
								ProductId = product.ProductId,
								IsActive = true
							};
							await this._projectProductRepository.InsertAndGetIdAsync(projectProduct2);
							ProjectProductLineItem projectProductLineItem1 = new ProjectProductLineItem()
							{
								ProjectId = recordId,
								Cost = product.LineItem.Cost,
								Quantity = product.LineItem.Quantity,
								ProductId = product.ProductId,
								Options = new List<ProjectProductLineItemOption>()
							};
							long num1 = await this._projectProductLineItemRepository.InsertAndGetIdAsync(projectProductLineItem1);
							if (product.LineItem.Options.Any<ProjectProductLineItemOptionDto>())
							{
								foreach (ProjectProductLineItemOptionDto projectProductLineItemOptionDto in product.LineItem.Options)
								{
									ProjectProductLineItemOption projectProductLineItemOption1 = new ProjectProductLineItemOption()
									{
										ProductLineItemId = num1,
										ProductOptionId = projectProductLineItemOptionDto.ProductOptionId
									};
									await this._projectProductLineItemOptionRepository.InsertAsync(projectProductLineItemOption1);
								}
							}
							num1 = (long)0;
						}
					}
					list5 = null;
				}
			}
			return recordId;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects.Create", "Pages.Tenant.Projects.Edit" })]
		public async Task CreateOrUpdateProjectTeamMembers(CreateOrUpdateProjectTeamMemberInput input)
		{
			IRepository<ProjectTeamMember, long> repository = this._projectTeamMemberRepository;
			List<ProjectTeamMember> allListAsync = await repository.GetAllListAsync((ProjectTeamMember w) => (long?)w.ProjectId == input.ProjectId);
			List<ProjectTeamMember> projectTeamMembers = allListAsync;
			if (projectTeamMembers.Any<ProjectTeamMember>())
			{
				List<ProjectTeamMember> projectTeamMembers1 = projectTeamMembers;
				List<long> list = (
					from s in projectTeamMembers1
					select s.Id).ToList<long>();
				foreach (ProjectTeamMember projectTeamMember in projectTeamMembers)
				{
					if (!input.ProjectTeamMembers.Where<ProjectTeamMemberEditDto>((ProjectTeamMemberEditDto m) => {
						long? id = m.Id;
						long teamMemberId = projectTeamMember.TeamMemberId;
						if (id.GetValueOrDefault() != teamMemberId)
						{
							return false;
						}
						return id.HasValue;
					}).Any<ProjectTeamMemberEditDto>())
					{
						continue;
					}
					input.ProjectTeamMembers.RemoveAll((ProjectTeamMemberEditDto x) => {
						long? id = x.Id;
						long teamMemberId = projectTeamMember.TeamMemberId;
						if (id.GetValueOrDefault() != teamMemberId)
						{
							return false;
						}
						return id.HasValue;
					});
					list.RemoveAll((long x) => x == projectTeamMember.Id);
				}
				if (list.Any<long>())
				{
					IRepository<ProjectTeamMember, long> repository1 = this._projectTeamMemberRepository;
					await repository1.DeleteAsync((ProjectTeamMember m) => list.Contains(m.Id));
				}
			}
			List<ProjectTeamMemberEditDto> projectTeamMemberEditDtos = input.ProjectTeamMembers;
			List<long> nums = (
				from s in projectTeamMemberEditDtos
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
					foreach (ProjectTeamMemberEditDto projectTeamMemberEditDto in input.ProjectTeamMembers)
					{
						if (!list1.Contains(projectTeamMemberEditDto.TeamMemberId))
						{
							continue;
						}
						ProjectTeamMember projectTeamMember1 = new ProjectTeamMember()
						{
							ProjectId = input.ProjectId.Value,
							TeamMemberId = projectTeamMemberEditDto.TeamMemberId,
							IsActive = true
						};
						await this._projectTeamMemberRepository.InsertAsync(projectTeamMember1);
					}
					list1 = null;
				}
			}
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects.Delete" })]
		public async Task DeleteProject(IdInput<long> input)
		{
			await this._projectRepository.DeleteAsync(input.Id);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects.Delete" })]
		public async Task DeleteProjectResource(IdInput<long> input)
		{
			ProjectResource async = await this._projectResourceRepository.GetAsync(input.Id);
			if (async != null)
			{
				await this._projectResourceRepository.DeleteAsync(async.Id);
			}
		}

		private string GetCategoryFromFileExtension(string fileExt)
		{
			string empty = string.Empty;
			empty = (fileExt == ".pdf" || fileExt == ".xlsx" || fileExt == ".xls" || fileExt == ".docx" || fileExt == ".doc" || fileExt == ".zip" ? this.L("ResourceCategoryAttachment") : this.L("ResourceCategoryImage"));
			return empty;
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects" })]
		public async Task<Project> GetProject(long projectId)
		{
			return await this._projectRepository.GetAsync(projectId);
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects.Create" })]
		public async Task<GetProjectForEditOutput> GetProjectForEdit(NullableIdInput<long> input)
		{
			ProjectEditDto projectEditDto;
			if (!input.Id.HasValue)
			{
				projectEditDto = new ProjectEditDto()
				{
					Tasks = new List<ProjectTask>(),
					Products = new List<ProjectProductDto>(),
					AdhocProducts = new List<ProjectAdhocProduct>(),
					Adjustments = new List<ProjectAdjustment>()
				};
			}
			else
			{
				IRepository<Project, long> repository = this._projectRepository;
				long? id = input.Id;
				Project async = await repository.GetAsync(id.Value);
				projectEditDto = async.MapTo<ProjectEditDto>();
				ProjectEditDto projectEditDto1 = projectEditDto;
				IRepository<ProjectTask, long> repository1 = this._projectTaskRepository;
				List<ProjectTask> allListAsync = await repository1.GetAllListAsync((ProjectTask x) => x.ProjectId == async.Id);
				projectEditDto1.Tasks = allListAsync;
				projectEditDto1 = null;
				IRepository<ProjectProduct, long> repository2 = this._projectProductRepository;
				List<ProjectProduct> projectProducts = await repository2.GetAllListAsync((ProjectProduct x) => x.ProjectId == async.Id);
				projectEditDto.Products = projectProducts.MapTo<List<ProjectProductDto>>();
				if (projectEditDto.Products.Any<ProjectProductDto>())
				{
					foreach (ProjectProductDto product in projectEditDto.Products)
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
				projectEditDto1 = projectEditDto;
				IRepository<ProjectAdhocProduct, long> repository5 = this._projectAdhocProductRepository;
				List<ProjectAdhocProduct> projectAdhocProducts = await repository5.GetAllListAsync((ProjectAdhocProduct x) => x.ProjectId == async.Id);
				projectEditDto1.AdhocProducts = projectAdhocProducts;
				projectEditDto1 = null;
				projectEditDto1 = projectEditDto;
				IRepository<ProjectAdjustment, long> repository6 = this._projectAdjustmentRepository;
				List<ProjectAdjustment> projectAdjustments = await repository6.GetAllListAsync((ProjectAdjustment x) => x.ProjectId == async.Id);
				projectEditDto1.Adjustments = projectAdjustments;
				projectEditDto1 = null;
				projectEditDto1 = projectEditDto;
				Customer customer = await this._customerRepository.GetAsync(async.CustomerId);
				projectEditDto1.Customer = customer.MapTo<CustomerDto>();
				projectEditDto1 = null;
				if (!async.CustomerAddressId.HasValue)
				{
					projectEditDto.CustomerAddress = new AddressDto()
					{
						CountryRegion = new CountryRegionDto()
					};
				}
				else
				{
					projectEditDto1 = projectEditDto;
					IRepository<Address, long> repository7 = this._addressRepository;
					id = async.CustomerAddressId;
					Address address = await repository7.GetAsync(id.Value);
					projectEditDto1.CustomerAddress = address.MapTo<AddressDto>();
					projectEditDto1 = null;
					if (projectEditDto.CustomerAddress.CountryRegionId.HasValue)
					{
						AddressDto customerAddress = projectEditDto.CustomerAddress;
						IRepository<CountryRegion> repository8 = this._countryRegionRepository;
						int? countryRegionId = async.CustomerAddress.CountryRegionId;
						CountryRegion countryRegion = await repository8.GetAsync(countryRegionId.Value);
						customerAddress.CountryRegion = countryRegion.MapTo<CountryRegionDto>();
						customerAddress = null;
					}
				}
			}
			return new GetProjectForEditOutput()
			{
				Project = projectEditDto
			};
		}

		public async Task<ProjectResourceEditDto> GetProjectResourceDetailsByBinaryObjectId(Guid resourceId)
		{
			IRepository<ProjectResource, long> repository = this._projectResourceRepository;
			ProjectResource projectResource = await repository.FirstOrDefaultAsync((ProjectResource m) => m.BinaryObjectId == resourceId);
			ProjectResource projectResource1 = projectResource;
			if (projectResource1 == null)
			{
				throw new Exception("Project Resource was not found in GetProjectResourceDetailsByBinaryObjectId();");
			}
			return projectResource1.MapTo<ProjectResourceEditDto>();
		}

		public async Task<GetProjectResourceForEditOutput> GetProjectResourcesForEdit(NullableIdInput<long> input)
		{
			List<ProjectResourceEditDto> projectResourceEditDtos = new List<ProjectResourceEditDto>();
			if (input.Id.HasValue)
			{
				IRepository<ProjectResource, long> repository = this._projectResourceRepository;
				List<ProjectResource> allListAsync = await repository.GetAllListAsync((ProjectResource m) => m.ProjectId == input.Id.Value);
				List<ProjectResource> projectResources = allListAsync;
				if (projectResources.Any<ProjectResource>())
				{
					projectResourceEditDtos = projectResources.MapTo<List<ProjectResourceEditDto>>();
				}
			}
			return new GetProjectResourceForEditOutput()
			{
				ProjectResources = projectResourceEditDtos
			};
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects" })]
		public async Task<PagedResultOutput<ProjectListDto>> GetProjects(GetProjectsInput input)
		{
			//ProjectAppService.<>c__DisplayClass21_2 variable = null;
			//ProjectAppService.<>c__DisplayClass21_1 variable1 = null;
			//ProjectAppService.<>c__DisplayClass21_0 variable2 = null;
			int idToGet;
			IQueryable<Project> all = this._projectRepository.GetAll();
            var projects = all.Where(p => p.TenantId == AbpSession.TenantId && p.Id == 0);
			var listAsync = await projects.Select(s => new { ProjectId = s.Id }).ToListAsync();
			List<long> foundProjectIdsFromInputFilter = (
				from s in listAsync
				select s.ProjectId).ToList<long>();
			bool foundFromIdFilter = false;
			if (input.Filter.ToLower().StartsWith("id:"))
			{
				try
				{
					string lower = input.Filter.ToLower();
					char[] chrArray = new char[] { ':' };
					int.TryParse(lower.Split(chrArray)[1].ToString(), out idToGet);
					IQueryable<Project> all1 = _projectRepository.GetAll();
                    var projects1 = all1.Where(p => p.TenantId == AbpSession.TenantId && p.Id == idToGet);
					var listAsync1 = await projects1.Select(s => new { ProjectId = s.Id }).ToListAsync();
					foundFromIdFilter = true;
					foundProjectIdsFromInputFilter = foundProjectIdsFromInputFilter.Union(
						from s in listAsync1
						select s.ProjectId).ToList();
				}
				catch (Exception)
				{
				}
			}
			if (!foundFromIdFilter)
			{
				IQueryable<Project> all2 = this._projectRepository.GetAll();
                var projects2 = all2.WhereIf(!input.Filter.IsNullOrEmpty(), p =>
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
					p.Estimate.CurrentStatus.Contains(input.Filter) ||
					p.Estimate.Description.Contains(input.Filter) ||
					p.Estimate.Label.Contains(input.Filter) ||
					p.Estimate.Number.Contains(input.Filter) ||
					p.Estimate.PONumber.Contains(input.Filter) ||
					p.Estimate.Terms.Contains(input.Filter) ||
					p.Estimate.TimeEntryLog.Contains(input.Filter)
				);
				var listAsync2 = await projects2.Select(s => new { ProjectId = s.Id }).ToListAsync();
				foundProjectIdsFromInputFilter = foundProjectIdsFromInputFilter.Union(
					from s in listAsync2
					select s.ProjectId).ToList();
			}
			IQueryable<Project> all3 = this._projectRepository.GetAll();
            var projects3 = all3.Where(m => foundProjectIdsFromInputFilter.Contains(m.Id));
			int resultCount = await projects3.CountAsync();
			if (input.Sorting.Contains("customer"))
			{
				input.Sorting = input.Sorting.Replace("customer", "customerid");
			}
			List<Project> pagedAndSortedProjects = await projects3.OrderBy(input.Sorting, new object[0]).PageBy(input).ToListAsync();
			foreach (Project project in pagedAndSortedProjects)
			{
                int taskTotal = await _projectTaskRepository.CountAsync(m => m.ProjectId == project.Id);
				project.TaskTotal = taskTotal;
			}
			return new PagedResultOutput<ProjectListDto>(resultCount, pagedAndSortedProjects.MapTo<List<ProjectListDto>>());
		}

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects.ExportData" })]
		public async Task<FileDto> GetProjectsToExcel()
		{
			List<Project> allListAsync = await this._projectRepository.GetAllListAsync();
			List<ProjectListDto> projectListDtos = allListAsync.MapTo<List<ProjectListDto>>();
			return this._projectListExcelExporter.ExportToFile(projectListDtos);
		}

		public async Task<GetProjectTeamMembersForEditOutput> GetProjectTeamMembersForEdit(NullableIdInput<long> input)
		{
			List<ProjectTeamMemberEditDto> projectTeamMemberEditDtos = new List<ProjectTeamMemberEditDto>();
			if (input.Id.HasValue)
			{
				IRepository<ProjectTeamMember, long> repository = this._projectTeamMemberRepository;
				List<ProjectTeamMember> allListAsync = await repository.GetAllListAsync((ProjectTeamMember m) => m.ProjectId == input.Id.Value);
				List<ProjectTeamMember> projectTeamMembers = allListAsync;
				if (projectTeamMembers.Any<ProjectTeamMember>())
				{
					projectTeamMemberEditDtos = projectTeamMembers.MapTo<List<ProjectTeamMemberEditDto>>();
				}
			}
			return new GetProjectTeamMembersForEditOutput()
			{
				ProjectTeamMembers = projectTeamMemberEditDtos
			};
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

		[AbpAuthorize(new string[] { "Pages.Tenant.Projects.Create", "Pages.Tenant.Projects.Edit" })]
		public async Task SaveProjectResourceAsync(UpdateProjectResourceInput input)
		{
			Guid? resourceId;
			if (input.Id <= (long)0)
			{
				ProjectResource projectResource = new ProjectResource()
				{
					Id = (long)0
				};
				resourceId = input.ResourceId;
				projectResource.BinaryObjectId = Guid.Parse(resourceId.ToString());
				projectResource.ProjectId = input.ProjectId;
				projectResource.Name = input.FileName;
				projectResource.FileName = input.FileName;
				projectResource.FileExtension = input.FileExtension;
				projectResource.FileSize = input.FileSize;
				projectResource.Category = this.GetCategoryFromFileExtension(input.FileExtension);
				projectResource.IsActive = input.IsActive;
				await this._projectResourceRepository.InsertAsync(projectResource);
			}
			else
			{
				ProjectResource async = await this._projectResourceRepository.GetAsync(input.Id);
				if (async != null)
				{
					resourceId = input.ResourceId;
					async.BinaryObjectId = Guid.Parse(resourceId.ToString());
					async.ProjectId = input.ProjectId;
					async.FileExtension = input.FileExtension;
					async.FileName = input.FileName;
					async.FileSize = input.FileSize;
					async.Category = this.GetCategoryFromFileExtension(input.FileExtension);
					async.IsActive = input.IsActive;
				}
				await this._projectResourceRepository.UpdateAsync(async);
			}
		}

		public async Task SaveProjectResourceDetails(long id, string name, string description, bool isActive)
		{
			ProjectResource async = await this._projectResourceRepository.GetAsync(id);
			if (async != null)
			{
				async.Description = description;
			}
			await this._projectResourceRepository.UpdateAsync(async);
		}
	}
}