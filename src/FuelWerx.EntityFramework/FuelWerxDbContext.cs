using Abp.Zero.EntityFramework;
using FuelWerx.Administrative;
using FuelWerx.Authorization.Roles;
using FuelWerx.Authorization.Users;
using FuelWerx.Customers;
using FuelWerx.Estimates;
using FuelWerx.FillLots;
using FuelWerx.FillLotTanks;
using FuelWerx.FuelCastSettings;
using FuelWerx.Generic;
using FuelWerx.Invoices;
using FuelWerx.MultiTenancy;
using FuelWerx.Organizations;
using FuelWerx.Products;
using FuelWerx.Projects;
using FuelWerx.ServiceTanks;
using FuelWerx.Storage;
using FuelWerx.Suppliers;
using FuelWerx.Tenants;
using FuelWerx.Trucks;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace FuelWerx.EntityFramework
{
	public class FuelWerxDbContext : AbpZeroDbContext<Tenant, Role, User>
	{
		public virtual IDbSet<Address> Addresses
		{
			get;
			set;
		}

		public virtual IDbSet<Bank> Banks
		{
			get;
			set;
		}

		public virtual IDbSet<BinaryObject> BinaryObjects
		{
			get;
			set;
		}

		public virtual IDbSet<Contact> Contacts
		{
			get;
			set;
		}

		public virtual IDbSet<Country> Countries
		{
			get;
			set;
		}

		public virtual IDbSet<CountryRegion> CountryRegions
		{
			get;
			set;
		}

		public virtual IDbSet<Customer> Customers
		{
			get;
			set;
		}

		public virtual IDbSet<DriversData> DriversDatas
		{
			get;
			set;
		}

		public virtual IDbSet<EmergencyDeliveryFeeRule> EmergencyDeliveryFeeRules
		{
			get;
			set;
		}

		public virtual IDbSet<EmergencyDeliveryFee> EmergencyDeliveryFees
		{
			get;
			set;
		}

		public virtual IDbSet<EstimateAdhocProduct> EstimateAdhocProducts
		{
			get;
			set;
		}

		public virtual IDbSet<EstimateAdjustment> EstimateAdjustments
		{
			get;
			set;
		}

		public virtual IDbSet<EstimateProductLineItemOption> EstimateProductLineItemOptions
		{
			get;
			set;
		}

		public virtual IDbSet<EstimateProductLineItem> EstimateProductLineItems
		{
			get;
			set;
		}

		public virtual IDbSet<EstimateProduct> EstimateProducts
		{
			get;
			set;
		}

		public virtual IDbSet<EstimateResource> EstimateResources
		{
			get;
			set;
		}

		public virtual IDbSet<Estimate> Estimates
		{
			get;
			set;
		}

		public virtual IDbSet<EstimateTask> EstimateTasks
		{
			get;
			set;
		}

		public virtual IDbSet<FillLot> FillLots
		{
			get;
			set;
		}

		public virtual IDbSet<FillLotTank> FillLotTanks
		{
			get;
			set;
		}

		public virtual IDbSet<FIP> FIPs
		{
			get;
			set;
		}

		public virtual IDbSet<FuelCastSetting> FuelCastSettings
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceAdhocProduct> InvoiceAdhocProducts
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceAdjustment> InvoiceAdjustments
		{
			get;
			set;
		}

		public virtual IDbSet<InvoicePayment> InvoicePayments
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceProductLineItemOption> InvoiceProductLineItemOptions
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceProductLineItem> InvoiceProductLineItems
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceProduct> InvoiceProducts
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceResource> InvoiceResources
		{
			get;
			set;
		}

		public virtual IDbSet<Invoice> Invoices
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceTask> InvoiceTasks
		{
			get;
			set;
		}

		public virtual IDbSet<InvoiceTeamMember> InvoiceTeamMembers
		{
			get;
			set;
		}

		public virtual IDbSet<FuelWerx.Organizations.OrganizationUnitProperties> OrganizationUnitProperties
		{
			get;
			set;
		}

		public virtual IDbSet<PaymentSetting> PaymentSettings
		{
			get;
			set;
		}

		public virtual IDbSet<Phone> Phones
		{
			get;
			set;
		}

		public virtual IDbSet<ProductOption> ProductOptions
		{
			get;
			set;
		}

		public virtual IDbSet<ProductPrice> ProductPrices
		{
			get;
			set;
		}

		public virtual IDbSet<ProductPriceTaxRule> ProductPriceTaxRules
		{
			get;
			set;
		}

		public virtual IDbSet<ProductResource> ProductResources
		{
			get;
			set;
		}

		public virtual IDbSet<Product> Products
		{
			get;
			set;
		}

		public virtual IDbSet<ProductSpecificPrice> ProductSpecificPrices
		{
			get;
			set;
		}

		public virtual IDbSet<ProductSupplier> ProductSuppliers
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectAdhocProduct> ProjectAdhocProducts
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectAdjustment> ProjectAdjustments
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectProductLineItemOption> ProjectProductLineItemOptions
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectProductLineItem> ProjectProductLineItems
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectProduct> ProjectProducts
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectResource> ProjectResources
		{
			get;
			set;
		}

		public virtual IDbSet<Project> Projects
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectTask> ProjectTasks
		{
			get;
			set;
		}

		public virtual IDbSet<ProjectTeamMember> ProjectTeamMembers
		{
			get;
			set;
		}

		public virtual IDbSet<QuickMenuItem> QuickMenuItems
		{
			get;
			set;
		}

		public virtual IDbSet<Service> Services
		{
			get;
			set;
		}

		public virtual IDbSet<ServiceTank> ServiceTanks
		{
			get;
			set;
		}

		public virtual IDbSet<Supplier> Suppliers
		{
			get;
			set;
		}

		public virtual IDbSet<Tax> Taxes
		{
			get;
			set;
		}

		public virtual IDbSet<TaxRuleRule> TaxRuleRules
		{
			get;
			set;
		}

		public virtual IDbSet<TaxRule> TaxRules
		{
			get;
			set;
		}

		public virtual IDbSet<TenantCustomerService> TenantCustomerServices
		{
			get;
			set;
		}

		public virtual IDbSet<FuelWerx.Tenants.TenantDateTimeSettings> TenantDateTimeSettings
		{
			get;
			set;
		}

		public virtual IDbSet<TenantDetail> TenantDetails
		{
			get;
			set;
		}

		public virtual IDbSet<TenantHour> TenantHours
		{
			get;
			set;
		}

		public virtual IDbSet<FuelWerx.Tenants.TenantLogos> TenantLogos
		{
			get;
			set;
		}

		public virtual IDbSet<FuelWerx.Tenants.TenantNotifications> TenantNotifications
		{
			get;
			set;
		}

		public virtual IDbSet<FuelWerx.Tenants.TenantPaymentGatewaySettings> TenantPaymentGatewaySettings
		{
			get;
			set;
		}

		public virtual IDbSet<FuelWerx.Tenants.TenantPaymentSettings> TenantPaymentSettings
		{
			get;
			set;
		}

		public virtual IDbSet<Title> Titles
		{
			get;
			set;
		}

		public virtual IDbSet<Truck> Trucks
		{
			get;
			set;
		}

		public virtual IDbSet<UserSettingData> UserSettingDatas
		{
			get;
			set;
		}

		public virtual IDbSet<Zone> Zones
		{
			get;
			set;
		}

		public virtual IDbSet<ZoneTax> ZoneTaxes
		{
			get;
			set;
		}

		public FuelWerxDbContext() : base("Default")
		{
		}

		public FuelWerxDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
		{
			((IObjectContextAdapter)this).ObjectContext.CommandTimeout = new int?(360);
		}

		public FuelWerxDbContext(DbConnection dbConnection) : base(dbConnection, true)
		{
		}
	}
}