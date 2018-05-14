using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using System;

namespace FuelWerx.Authorization
{
	public class AppAuthorizationProvider : AuthorizationProvider
	{
		public AppAuthorizationProvider()
		{
		}

		private static ILocalizableString L(string name)
		{
			return new LocalizableString(name, "FuelWerx");
		}

		public override void SetPermissions(IPermissionDefinitionContext context)
		{
			Permission permissionOrNull = context.GetPermissionOrNull("Rights") ?? context.CreatePermission("Rights", AppAuthorizationProvider.L("Rights"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission = permissionOrNull.CreateChildPermission("Rights.Pricing", AppAuthorizationProvider.L("Pricing"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission.CreateChildPermission("Rights.Pricing.CanModifyEstimatePrice", AppAuthorizationProvider.L("CanModifyEstimatePrice"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission.CreateChildPermission("Rights.Pricing.CanModifyEstimateProductPrice", AppAuthorizationProvider.L("CanModifyEstimateProductPrice"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission.CreateChildPermission("Rights.Pricing.CanModifyProjectPrice", AppAuthorizationProvider.L("CanModifyProjectPrice"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission.CreateChildPermission("Rights.Pricing.CanModifyProjectProductPrice", AppAuthorizationProvider.L("CanModifyProjectProductPrice"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission.CreateChildPermission("Rights.Pricing.CanModifyInvoicePrice", AppAuthorizationProvider.L("CanModifyInvoicePrice"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission.CreateChildPermission("Rights.Pricing.CanModifyInvoiceProductPrice", AppAuthorizationProvider.L("CanModifyInvoiceProductPrice"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission1 = permissionOrNull.CreateChildPermission("Rights.Discounting", AppAuthorizationProvider.L("Discounts"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission1.CreateChildPermission("Rights.Discounting.CanModifyEstimateDiscount", AppAuthorizationProvider.L("CanModifyEstimateDiscount"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission1.CreateChildPermission("Rights.Discounting.CanModifyProjectDiscount", AppAuthorizationProvider.L("CanModifyProjectDiscount"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission1.CreateChildPermission("Rights.Discounting.CanModifyInvoiceDiscount", AppAuthorizationProvider.L("CanModifyInvoiceDiscount"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permissionOrNull.CreateChildPermission("Rights.Assets", AppAuthorizationProvider.L("Assets"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null).CreateChildPermission("Rights.Assets.CanModifyFillLotTankCapacities", AppAuthorizationProvider.L("CanModifyFillLotTankCapacities"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission2 = permissionOrNull.CreateChildPermission("Rights.Customer", AppAuthorizationProvider.L("Customer"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanAddAddresses", AppAuthorizationProvider.L("CanAddAddresses"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanEditAddresses", AppAuthorizationProvider.L("CanEditAddresses"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanDeleteAddresses", AppAuthorizationProvider.L("CanDeleteAddresses"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanAddPayments", AppAuthorizationProvider.L("CanAddPayments"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanViewPayments", AppAuthorizationProvider.L("CanViewPayments"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanDeletePayments", AppAuthorizationProvider.L("CanDeletePayments"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanAddPhones", AppAuthorizationProvider.L("CanAddPhones"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanEditPhones", AppAuthorizationProvider.L("CanEditPhones"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanDeletePhones", AppAuthorizationProvider.L("CanDeletePhones"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanAddServices", AppAuthorizationProvider.L("CanAddServices"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanEditServices", AppAuthorizationProvider.L("CanEditServices"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanDeleteServices", AppAuthorizationProvider.L("CanDeleteServices"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission2.CreateChildPermission("Rights.Customer.CanModifyServiceTankCapacities", AppAuthorizationProvider.L("CanModifyServicesTankCapacity"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permissionOrNull1 = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", AppAuthorizationProvider.L("Pages"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission3 = permissionOrNull1.CreateChildPermission("Pages.Administration", AppAuthorizationProvider.L("Administration"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission4 = permission3.CreateChildPermission("Pages.Administration.Roles", AppAuthorizationProvider.L("Roles"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission4.CreateChildPermission("Pages.Administration.Roles.Create", AppAuthorizationProvider.L("CreatingNewRole"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission4.CreateChildPermission("Pages.Administration.Roles.Edit", AppAuthorizationProvider.L("EditingRole"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission4.CreateChildPermission("Pages.Administration.Roles.Delete", AppAuthorizationProvider.L("DeletingRole"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission5 = permission3.CreateChildPermission("Pages.Administration.Contacts", AppAuthorizationProvider.L("Contacts"), false, null, MultiTenancySides.Tenant, null);
			permission5.CreateChildPermission("Pages.Administration.Contacts.Create", AppAuthorizationProvider.L("CreatingNewContact"), false, null, MultiTenancySides.Tenant, null);
			permission5.CreateChildPermission("Pages.Administration.Contacts.Edit", AppAuthorizationProvider.L("EditingContact"), false, null, MultiTenancySides.Tenant, null);
			permission5.CreateChildPermission("Pages.Administration.Contacts.Delete", AppAuthorizationProvider.L("DeletingContact"), false, null, MultiTenancySides.Tenant, null);
			permission5.CreateChildPermission("Pages.Administration.Contacts.ExportData", AppAuthorizationProvider.L("ExportingContacts"), false, null, MultiTenancySides.Tenant, null);
			Permission permission6 = permission3.CreateChildPermission("Pages.Administration.Titles", AppAuthorizationProvider.L("Titles"), false, null, MultiTenancySides.Tenant, null);
			permission6.CreateChildPermission("Pages.Administration.Titles.Create", AppAuthorizationProvider.L("CreatingNewTitle"), false, null, MultiTenancySides.Tenant, null);
			permission6.CreateChildPermission("Pages.Administration.Titles.Edit", AppAuthorizationProvider.L("EditingTitle"), false, null, MultiTenancySides.Tenant, null);
			permission6.CreateChildPermission("Pages.Administration.Titles.Delete", AppAuthorizationProvider.L("DeletingTitle"), false, null, MultiTenancySides.Tenant, null);
			permission6.CreateChildPermission("Pages.Administration.Titles.ExportData", AppAuthorizationProvider.L("ExportingTitles"), false, null, MultiTenancySides.Tenant, null);
			Permission permission7 = permission3.CreateChildPermission("Pages.Administration.Taxes", AppAuthorizationProvider.L("Taxes"), false, null, MultiTenancySides.Tenant, null);
			permission7.CreateChildPermission("Pages.Administration.Taxes.Create", AppAuthorizationProvider.L("CreatingNewTax"), false, null, MultiTenancySides.Tenant, null);
			permission7.CreateChildPermission("Pages.Administration.Taxes.Edit", AppAuthorizationProvider.L("EditingTax"), false, null, MultiTenancySides.Tenant, null);
			permission7.CreateChildPermission("Pages.Administration.Taxes.Delete", AppAuthorizationProvider.L("DeletingTax"), false, null, MultiTenancySides.Tenant, null);
			permission7.CreateChildPermission("Pages.Administration.Taxes.ExportData", AppAuthorizationProvider.L("ExportingTaxes"), false, null, MultiTenancySides.Tenant, null);
			Permission permission8 = permission3.CreateChildPermission("Pages.Administration.TaxRules", AppAuthorizationProvider.L("TaxRules"), false, null, MultiTenancySides.Tenant, null);
			permission8.CreateChildPermission("Pages.Administration.TaxRules.Create", AppAuthorizationProvider.L("CreatingNewTaxRule"), false, null, MultiTenancySides.Tenant, null);
			permission8.CreateChildPermission("Pages.Administration.TaxRules.Edit", AppAuthorizationProvider.L("EditingTaxRule"), false, null, MultiTenancySides.Tenant, null);
			permission8.CreateChildPermission("Pages.Administration.TaxRules.Delete", AppAuthorizationProvider.L("DeletingTaxRule"), false, null, MultiTenancySides.Tenant, null);
			permission8.CreateChildPermission("Pages.Administration.TaxRules.ExportData", AppAuthorizationProvider.L("ExportingTaxRules"), false, null, MultiTenancySides.Tenant, null);
			Permission permission9 = permission3.CreateChildPermission("Pages.Administration.EmergencyDeliveryFees", AppAuthorizationProvider.L("EmergencyDeliveryFees"), false, null, MultiTenancySides.Tenant, null);
			permission9.CreateChildPermission("Pages.Administration.EmergencyDeliveryFees.Create", AppAuthorizationProvider.L("CreatingNewEmergencyDeliveryFee"), false, null, MultiTenancySides.Tenant, null);
			permission9.CreateChildPermission("Pages.Administration.EmergencyDeliveryFees.Edit", AppAuthorizationProvider.L("EditingEmergencyDeliveryFee"), false, null, MultiTenancySides.Tenant, null);
			permission9.CreateChildPermission("Pages.Administration.EmergencyDeliveryFees.Delete", AppAuthorizationProvider.L("DeletingEmergencyDeliveryFee"), false, null, MultiTenancySides.Tenant, null);
			permission9.CreateChildPermission("Pages.Administration.EmergencyDeliveryFees.ExportData", AppAuthorizationProvider.L("ExportingEmergencyDeliveryFees"), false, null, MultiTenancySides.Tenant, null);
			Permission permission10 = permission3.CreateChildPermission("Pages.Administration.Zones", AppAuthorizationProvider.L("Zones"), false, null, MultiTenancySides.Tenant, null);
			permission10.CreateChildPermission("Pages.Administration.Zones.Create", AppAuthorizationProvider.L("CreatingNewZone"), false, null, MultiTenancySides.Tenant, null);
			permission10.CreateChildPermission("Pages.Administration.Zones.Edit", AppAuthorizationProvider.L("EditingZone"), false, null, MultiTenancySides.Tenant, null);
			permission10.CreateChildPermission("Pages.Administration.Zones.Delete", AppAuthorizationProvider.L("DeletingZone"), false, null, MultiTenancySides.Tenant, null);
			permission10.CreateChildPermission("Pages.Administration.Zones.ExportData", AppAuthorizationProvider.L("ExportingZones"), false, null, MultiTenancySides.Tenant, null);
			Permission permission11 = permission3.CreateChildPermission("Pages.Administration.Users", AppAuthorizationProvider.L("Users"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission11.CreateChildPermission("Pages.Administration.Users.Create", AppAuthorizationProvider.L("CreatingNewUser"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission11.CreateChildPermission("Pages.Administration.Users.Edit", AppAuthorizationProvider.L("EditingUser"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission11.CreateChildPermission("Pages.Administration.Users.Delete", AppAuthorizationProvider.L("DeletingUser"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission11.CreateChildPermission("Pages.Administration.Users.ChangePermissions", AppAuthorizationProvider.L("ChangingPermissions"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission11.CreateChildPermission("Pages.Administration.Users.Impersonation", AppAuthorizationProvider.L("LoginForUsers"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission12 = permission3.CreateChildPermission("Pages.Administration.Languages", AppAuthorizationProvider.L("Languages"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission12.CreateChildPermission("Pages.Administration.Languages.Create", AppAuthorizationProvider.L("CreatingNewLanguage"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission12.CreateChildPermission("Pages.Administration.Languages.Edit", AppAuthorizationProvider.L("EditingLanguage"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission12.CreateChildPermission("Pages.Administration.Languages.Delete", AppAuthorizationProvider.L("DeletingLanguages"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission12.CreateChildPermission("Pages.Administration.Languages.ChangeTexts", AppAuthorizationProvider.L("ChangingTexts"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission3.CreateChildPermission("Pages.Administration.AuditLogs", AppAuthorizationProvider.L("AuditLogs"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			Permission permission13 = permission3.CreateChildPermission("Pages.Administration.OrganizationUnits", AppAuthorizationProvider.L("OrganizationUnits"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission13.CreateChildPermission("Pages.Administration.OrganizationUnits.ManageOrganizationTree", AppAuthorizationProvider.L("ManagingOrganizationTree"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission13.CreateChildPermission("Pages.Administration.OrganizationUnits.ManageMembers", AppAuthorizationProvider.L("ManagingMembers"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permission13.CreateChildPermission("Pages.Administration.OrganizationUnits.ManageProperties", AppAuthorizationProvider.L("ManagingOrganizationUnitProperties"), false, null, MultiTenancySides.Tenant | MultiTenancySides.Host, null);
			permissionOrNull1.CreateChildPermission("Pages.Tenant.Dashboard", AppAuthorizationProvider.L("Dashboard"), false, null, MultiTenancySides.Tenant, null);
			Permission permission14 = permissionOrNull1.CreateChildPermission("Pages.Tenant.FuelCast", AppAuthorizationProvider.L("FuelCast"), false, null, MultiTenancySides.Tenant, null);
			Permission permission15 = permission14.CreateChildPermission("Pages.Tenant.Trucks", AppAuthorizationProvider.L("Trucks"), false, null, MultiTenancySides.Tenant, null);
			permission15.CreateChildPermission("Pages.Tenant.Trucks.Create", AppAuthorizationProvider.L("CreatingNewTruck"), false, null, MultiTenancySides.Tenant, null);
			permission15.CreateChildPermission("Pages.Tenant.Trucks.Edit", AppAuthorizationProvider.L("EditingTruck"), false, null, MultiTenancySides.Tenant, null);
			permission15.CreateChildPermission("Pages.Tenant.Trucks.Delete", AppAuthorizationProvider.L("DeletingTrucks"), false, null, MultiTenancySides.Tenant, null);
			permission15.CreateChildPermission("Pages.Tenant.Trucks.ExportData", AppAuthorizationProvider.L("ExportingTrucks"), false, null, MultiTenancySides.Tenant, null);
			Permission permission16 = permission14.CreateChildPermission("Pages.Tenant.FillLots", AppAuthorizationProvider.L("FuelLots"), false, null, MultiTenancySides.Tenant, null);
			permission16.CreateChildPermission("Pages.Tenant.FillLots.Create", AppAuthorizationProvider.L("CreatingNewFuelLot"), false, null, MultiTenancySides.Tenant, null);
			permission16.CreateChildPermission("Pages.Tenant.FillLots.Edit", AppAuthorizationProvider.L("EditingFuelLot"), false, null, MultiTenancySides.Tenant, null);
			permission16.CreateChildPermission("Pages.Tenant.FillLots.Delete", AppAuthorizationProvider.L("DeletingFuelLots"), false, null, MultiTenancySides.Tenant, null);
			permission16.CreateChildPermission("Pages.Tenant.FillLots.ExportData", AppAuthorizationProvider.L("ExportingFuelLots"), false, null, MultiTenancySides.Tenant, null);
			permission14.CreateChildPermission("Pages.Tenant.FuelCastSettings", AppAuthorizationProvider.L("FuelCastSettings"), false, null, MultiTenancySides.Tenant, null).CreateChildPermission("Pages.Tenant.FuelCastSettings.Edit", AppAuthorizationProvider.L("EditingFuelCastSettings"), false, null, MultiTenancySides.Tenant, null);
			Permission permission17 = permissionOrNull1.CreateChildPermission("Pages.Tenant.Invoices", AppAuthorizationProvider.L("Invoices"), false, null, MultiTenancySides.Tenant, null);
			permission17.CreateChildPermission("Pages.Tenant.Invoices.Create", AppAuthorizationProvider.L("CreatingNewInvoice"), false, null, MultiTenancySides.Tenant, null);
			permission17.CreateChildPermission("Pages.Tenant.Invoices.Edit", AppAuthorizationProvider.L("EditingInvoice"), false, null, MultiTenancySides.Tenant, null);
			permission17.CreateChildPermission("Pages.Tenant.Invoices.Delete", AppAuthorizationProvider.L("DeletingInvoices"), false, null, MultiTenancySides.Tenant, null);
			permission17.CreateChildPermission("Pages.Tenant.Invoices.ExportData", AppAuthorizationProvider.L("ExportingInvoices"), false, null, MultiTenancySides.Tenant, null);
			Permission permission18 = permission17.CreateChildPermission("Pages.Tenant.InvoicePayments", AppAuthorizationProvider.L("InvoicePayments"), false, null, MultiTenancySides.Tenant, null);
			permission18.CreateChildPermission("Pages.Tenant.InvoicePayments.Create", AppAuthorizationProvider.L("CreatingNewInvoicePayment"), false, null, MultiTenancySides.Tenant, null);
			permission18.CreateChildPermission("Pages.Tenant.InvoicePayments.ExportData", AppAuthorizationProvider.L("ExportingInvoicePayments"), false, null, MultiTenancySides.Tenant, null);
			Permission permission19 = permissionOrNull1.CreateChildPermission("Pages.Tenant.Suppliers", AppAuthorizationProvider.L("Suppliers"), false, null, MultiTenancySides.Tenant, null);
			permission19.CreateChildPermission("Pages.Tenant.Suppliers.Create", AppAuthorizationProvider.L("CreatingNewSupplier"), false, null, MultiTenancySides.Tenant, null);
			permission19.CreateChildPermission("Pages.Tenant.Suppliers.Edit", AppAuthorizationProvider.L("EditingSupplier"), false, null, MultiTenancySides.Tenant, null);
			permission19.CreateChildPermission("Pages.Tenant.Suppliers.Delete", AppAuthorizationProvider.L("DeletingSuppliers"), false, null, MultiTenancySides.Tenant, null);
			permission19.CreateChildPermission("Pages.Tenant.Suppliers.ExportData", AppAuthorizationProvider.L("ExportingSuppliers"), false, null, MultiTenancySides.Tenant, null);
			Permission permission20 = permissionOrNull1.CreateChildPermission("Pages.Tenant.Customers", AppAuthorizationProvider.L("Customers"), false, null, MultiTenancySides.Tenant, null);
			permission20.CreateChildPermission("Pages.Tenant.Customers.Create", AppAuthorizationProvider.L("CreatingNewCustomer"), false, null, MultiTenancySides.Tenant, null);
			permission20.CreateChildPermission("Pages.Tenant.Customers.Edit", AppAuthorizationProvider.L("EditingCustomer"), false, null, MultiTenancySides.Tenant, null);
			permission20.CreateChildPermission("Pages.Tenant.Customers.Delete", AppAuthorizationProvider.L("DeletingCustomers"), false, null, MultiTenancySides.Tenant, null);
			permission20.CreateChildPermission("Pages.Tenant.Customers.ExportData", AppAuthorizationProvider.L("ExportingCustomersApplications"), false, null, MultiTenancySides.Tenant, null);
			Permission permission21 = permissionOrNull1.CreateChildPermission("Pages.Tenant.Products", AppAuthorizationProvider.L("Products"), false, null, MultiTenancySides.Tenant, null);
			permission21.CreateChildPermission("Pages.Tenant.Products.Create", AppAuthorizationProvider.L("CreatingNewProduct"), false, null, MultiTenancySides.Tenant, null);
			permission21.CreateChildPermission("Pages.Tenant.Products.Edit", AppAuthorizationProvider.L("EditingProduct"), false, null, MultiTenancySides.Tenant, null);
			permission21.CreateChildPermission("Pages.Tenant.Products.Delete", AppAuthorizationProvider.L("DeletingProducts"), false, null, MultiTenancySides.Tenant, null);
			permission21.CreateChildPermission("Pages.Tenant.Products.ExportData", AppAuthorizationProvider.L("ExportingProducts"), false, null, MultiTenancySides.Tenant, null);
			Permission permission22 = permissionOrNull1.CreateChildPermission("Pages.Tenant.ProjectsAndEstimates", AppAuthorizationProvider.L("ProjectsAndEstimates"), false, null, MultiTenancySides.Tenant, null);
			Permission permission23 = permission22.CreateChildPermission("Pages.Tenant.Estimates", AppAuthorizationProvider.L("Estimates"), false, null, MultiTenancySides.Tenant, null);
			permission23.CreateChildPermission("Pages.Tenant.Estimates.Create", AppAuthorizationProvider.L("CreatingNewEstimate"), false, null, MultiTenancySides.Tenant, null);
			permission23.CreateChildPermission("Pages.Tenant.Estimates.Edit", AppAuthorizationProvider.L("EditingEstimate"), false, null, MultiTenancySides.Tenant, null);
			permission23.CreateChildPermission("Pages.Tenant.Estimates.Delete", AppAuthorizationProvider.L("DeletingEstimates"), false, null, MultiTenancySides.Tenant, null);
			permission23.CreateChildPermission("Pages.Tenant.Estimates.ExportData", AppAuthorizationProvider.L("ExportingEstimates"), false, null, MultiTenancySides.Tenant, null);
			Permission permission24 = permission22.CreateChildPermission("Pages.Tenant.Projects", AppAuthorizationProvider.L("Projects"), false, null, MultiTenancySides.Tenant, null);
			permission24.CreateChildPermission("Pages.Tenant.Projects.Create", AppAuthorizationProvider.L("CreatingNewProject"), false, null, MultiTenancySides.Tenant, null);
			permission24.CreateChildPermission("Pages.Tenant.Projects.Edit", AppAuthorizationProvider.L("EditingProject"), false, null, MultiTenancySides.Tenant, null);
			permission24.CreateChildPermission("Pages.Tenant.Projects.Delete", AppAuthorizationProvider.L("DeletingProjects"), false, null, MultiTenancySides.Tenant, null);
			permission24.CreateChildPermission("Pages.Tenant.Projects.ExportData", AppAuthorizationProvider.L("ExportingProjects"), false, null, MultiTenancySides.Tenant, null);
			permission3.CreateChildPermission("Pages.Administration.Tenant.Settings", AppAuthorizationProvider.L("Settings"), false, null, MultiTenancySides.Tenant, null);
			Permission permission25 = permissionOrNull1.CreateChildPermission("Pages.Editions", AppAuthorizationProvider.L("Editions"), false, null, MultiTenancySides.Host, null);
			permission25.CreateChildPermission("Pages.Editions.Create", AppAuthorizationProvider.L("CreatingNewEdition"), false, null, MultiTenancySides.Host, null);
			permission25.CreateChildPermission("Pages.Editions.Edit", AppAuthorizationProvider.L("EditingEdition"), false, null, MultiTenancySides.Host, null);
			permission25.CreateChildPermission("Pages.Editions.Delete", AppAuthorizationProvider.L("DeletingEdition"), false, null, MultiTenancySides.Host, null);
			Permission permission26 = permissionOrNull1.CreateChildPermission("Pages.Tenants", AppAuthorizationProvider.L("Tenants"), false, null, MultiTenancySides.Host, null);
			permission26.CreateChildPermission("Pages.Tenants.Create", AppAuthorizationProvider.L("CreatingNewTenant"), false, null, MultiTenancySides.Host, null);
			permission26.CreateChildPermission("Pages.Tenants.Edit", AppAuthorizationProvider.L("EditingTenant"), false, null, MultiTenancySides.Host, null);
			permission26.CreateChildPermission("Pages.Tenants.ChangeFeatures", AppAuthorizationProvider.L("ChangingFeatures"), false, null, MultiTenancySides.Host, null);
			permission26.CreateChildPermission("Pages.Tenants.Delete", AppAuthorizationProvider.L("DeletingTenant"), false, null, MultiTenancySides.Host, null);
			permission26.CreateChildPermission("Pages.Tenants.Impersonation", AppAuthorizationProvider.L("LoginForTenants"), false, null, MultiTenancySides.Host, null);
			permission3.CreateChildPermission("Pages.Administration.Host.Settings", AppAuthorizationProvider.L("Settings"), false, null, MultiTenancySides.Host, null);
		}
	}
}