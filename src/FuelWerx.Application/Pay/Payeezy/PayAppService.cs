using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using FuelWerx;
using FuelWerx.Configuration.Tenants;
using FuelWerx.Customers;
using FuelWerx.Emailing;
using FuelWerx.Estimates;
using FuelWerx.Generic;
using FuelWerx.Invoices;
using FuelWerx.MultiTenancy;
using FuelWerx.Products;
using FuelWerx.Projects;
using FuelWerx.Web;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FuelWerx.Pay.Payeezy
{
	public class PayAppService : FuelWerxAppServiceBase, IPayAppService, IApplicationService, ITransientDependency
	{
		private readonly TenantSettingsAppService _tenantSettingsAppService;

		private readonly FuelWerx.MultiTenancy.TenantManager _tenantManager;

		private readonly IWebUrlService _webUrlService;

		private readonly IEmailTemplateProvider _emailTemplateProvider;

		private readonly IEmailSender _emailSender;

		private readonly IRepository<Estimate, long> _estimateRepository;

		private readonly IRepository<EstimateTask, long> _estimateTaskRepository;

		private readonly IRepository<EstimateAdhocProduct, long> _estimateAdhocProductRepository;

		private readonly IRepository<EstimateProduct, long> _estimateProductRepository;

		private readonly IRepository<ProductOption, long> _productOptionRepository;

		private readonly IRepository<EstimateAdjustment, long> _estimateAdjustmentRepository;

		private readonly IRepository<EstimateProductLineItem, long> _estimateProductLineItemRepository;

		private readonly IRepository<Project, long> _projectRepository;

		private readonly IRepository<ProjectTask, long> _projectTaskRepository;

		private readonly IRepository<ProjectTeamMember, long> _projectTeamMemberRepository;

		private readonly IRepository<ProjectAdhocProduct, long> _projectAdhocProductRepository;

		private readonly IRepository<ProjectProduct, long> _projectProductRepository;

		private readonly IRepository<ProjectAdjustment, long> _projectAdjustmentRepository;

		private readonly IRepository<ProjectProductLineItem, long> _projectProductLineItemRepository;

		private readonly IRepository<Invoice, long> _invoiceRepository;

		private readonly IRepository<InvoiceTask, long> _invoiceTaskRepository;

		private readonly IRepository<InvoiceTeamMember, long> _invoiceTeamMemberRepository;

		private readonly IRepository<InvoiceAdhocProduct, long> _invoiceAdhocProductRepository;

		private readonly IRepository<InvoiceProduct, long> _invoiceProductRepository;

		private readonly IRepository<InvoiceAdjustment, long> _invoiceAdjustmentRepository;

		private readonly IRepository<InvoiceProductLineItem, long> _invoiceProductLineItemRepository;

		private readonly IRepository<Customer, long> _customerRepository;

		private readonly IRepository<Address, long> _addressRepository;

		private readonly IRepository<CountryRegion> _countryRegionRepository;

		public PayAppService(TenantSettingsAppService tenantSettingsAppService, FuelWerx.MultiTenancy.TenantManager tenantManager, IWebUrlService webUrlService, IRepository<Estimate, long> estimateRepository, IRepository<EstimateTask, long> estimateTaskRepository, IRepository<EstimateAdhocProduct, long> estimateAdhocProductRepository, IRepository<EstimateProduct, long> estimateProductRepository, IRepository<EstimateAdjustment, long> estimateAdjustmentRepository, IRepository<EstimateProductLineItem, long> estimateProductLineItemRepository, IRepository<ProductOption, long> productOptionRepository, IRepository<Project, long> projectRepository, IRepository<ProjectTeamMember, long> projectTeamMemberRepository, IRepository<ProjectTask, long> projectTaskRepository, IRepository<ProjectAdhocProduct, long> projectAdhocProductRepository, IRepository<ProjectProduct, long> projectProductRepository, IRepository<ProjectAdjustment, long> projectAdjustmentRepository, IRepository<ProjectProductLineItem, long> projectProductLineItemRepository, IRepository<Invoice, long> invoiceRepository, IRepository<InvoiceTeamMember, long> invoiceTeamMemberRepository, IRepository<InvoiceTask, long> invoiceTaskRepository, IRepository<InvoiceAdhocProduct, long> invoiceAdhocProductRepository, IRepository<InvoiceProduct, long> invoiceProductRepository, IRepository<InvoiceAdjustment, long> invoiceAdjustmentRepository, IRepository<InvoiceProductLineItem, long> invoiceProductLineItemRepository, IRepository<Customer, long> customerRepository, IRepository<Address, long> addressRepository, IRepository<CountryRegion> countryRegionRepository, IEmailTemplateProvider emailTemplateProvider, IEmailSender emailSender)
		{
			this._tenantSettingsAppService = tenantSettingsAppService;
			this._tenantManager = tenantManager;
			this._webUrlService = webUrlService;
			this._estimateRepository = estimateRepository;
			this._estimateTaskRepository = estimateTaskRepository;
			this._estimateAdhocProductRepository = estimateAdhocProductRepository;
			this._estimateProductRepository = estimateProductRepository;
			this._estimateAdjustmentRepository = estimateAdjustmentRepository;
			this._estimateProductLineItemRepository = estimateProductLineItemRepository;
			this._productOptionRepository = productOptionRepository;
			this._projectRepository = projectRepository;
			this._projectTaskRepository = projectTaskRepository;
			this._projectTeamMemberRepository = projectTeamMemberRepository;
			this._projectAdhocProductRepository = projectAdhocProductRepository;
			this._projectProductRepository = projectProductRepository;
			this._projectAdjustmentRepository = projectAdjustmentRepository;
			this._projectProductLineItemRepository = projectProductLineItemRepository;
			this._invoiceRepository = invoiceRepository;
			this._invoiceTaskRepository = invoiceTaskRepository;
			this._invoiceTeamMemberRepository = invoiceTeamMemberRepository;
			this._invoiceAdhocProductRepository = invoiceAdhocProductRepository;
			this._invoiceProductRepository = invoiceProductRepository;
			this._invoiceAdjustmentRepository = invoiceAdjustmentRepository;
			this._invoiceProductLineItemRepository = invoiceProductLineItemRepository;
			this._customerRepository = customerRepository;
			this._addressRepository = addressRepository;
			this._countryRegionRepository = countryRegionRepository;
			this._emailTemplateProvider = emailTemplateProvider;
			this._emailSender = emailSender;
		}

		public async Task<bool> EndPayment(long input)
		{
			return true;
		}

		public async Task<bool> StartPayment(long input)
		{
			return true;
		}
	}
}