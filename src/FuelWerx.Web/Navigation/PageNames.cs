using System;

namespace FuelWerx.Web.Navigation
{
	public static class PageNames
	{
		public static class App
		{
			public static class Common
			{
				public const string Administration = "Administration";

				public const string Roles = "Administration.Roles";

				public const string Users = "Administration.Users";

				public const string AuditLogs = "Administration.AuditLogs";

				public const string OrganizationUnits = "Administration.OrganizationUnits";

				public const string Languages = "Administration.Languages";

				public const string Titles = "Administration.Titles";

				public const string Contacts = "Administration.Contacts";

				public const string Taxes = "Administration.Taxes";

				public const string TaxRules = "Administration.TaxRules";

				public const string EmergencyDeliveryFees = "Administration.EmergencyDeliveryFees";

				public const string EmergencyDeliveryFeeRules = "Administration.EmergencyDeliveryFeeRules";

				public const string Zones = "Administration.Zones";
			}

			public static class Host
			{
				public const string Tenants = "Tenants";

				public const string Editions = "Editions";

				public const string Settings = "Administration.Settings.Host";
			}

			public static class Tenant
			{
				public const string Dashboard = "Dashboard.Tenant";

				public const string DashboardCustomer = "Dashboard.Customer.Tenant";

				public const string DashboardDriver = "Dashboard.Driver.Tenant";

				public const string FuelCast = "FuelCast.Tenant";

				public const string FuelCastSettings = "FuelCastSettings.Tenant";

				public const string Trucks = "Trucks.Tenant";

				public const string FillLots = "FillLots.Tenant";

				public const string Invoices = "Invoices.Tenant";

				public const string InvoicePayments = "InvoicePayments.Tenant";

				public const string Suppliers = "Suppliers.Tenant";

				public const string Customers = "Customers.Tenant";

				public const string Products = "Products.Tenant";

				public const string ProjectAndEstimates = "ProjectsAndEstimates.Tenant";

				public const string Projects = "Projects.Tenant";

				public const string Estimates = "Estimates.Tenant";

				public const string Settings = "Administration.Settings.Tenant";
			}
		}

		public static class Frontend
		{
			public const string Home = "Frontend.Home";

			public const string About = "Frontend.About";

			public const string Map = "Frontend.Map";
		}
	}
}