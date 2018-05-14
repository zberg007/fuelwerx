using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class fuelwerxmigration98 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration98));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602290937205_fuelwerxmigration98";
			}
		}

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Source
		{
			get
			{
				return null;
			}
		}

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Target
		{
			get
			{
				return this.Resources.GetString("Target");
			}
		}

		public fuelwerxmigration98()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTenantPaymentSettings", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantPaymentSettings_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantPaymentSettings_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantPaymentSettings_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantPaymentSettings_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantPaymentSettings", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.Boolean(nullable3, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.Boolean(nullable4, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.Boolean(nullable5, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.Boolean(nullable6, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.Boolean(nullable7, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.Boolean(nullable8, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.Boolean(nullable9, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.Boolean(nullable10, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable2 = null;
				ColumnModel columnModel10 = c.Boolean(nullable11, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel11 = c.Boolean(nullable12, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				ColumnModel columnModel12 = c.Boolean(nullable13, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.Boolean(nullable14, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable2 = null;
				ColumnModel columnModel14 = c.Boolean(nullable15, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable2 = null;
				ColumnModel columnModel15 = c.String(nullable16, nullable1, nullable17, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable2 = null;
				ColumnModel columnModel16 = c.Boolean(nullable18, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable19 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable20 = nullable2;
				nullable2 = null;
				ColumnModel columnModel17 = c.String(nullable19, nullable1, nullable20, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable21 = nullable2;
				nullable2 = null;
				ColumnModel columnModel18 = c.Boolean(nullable21, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable22 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable23 = nullable2;
				nullable2 = null;
				ColumnModel columnModel19 = c.String(nullable22, nullable1, nullable23, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable24 = nullable2;
				nullable2 = null;
				ColumnModel columnModel20 = c.Boolean(nullable24, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable25 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable26 = nullable2;
				nullable2 = null;
				ColumnModel columnModel21 = c.String(nullable25, nullable1, nullable26, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable27 = nullable2;
				nullable2 = null;
				ColumnModel columnModel22 = c.Boolean(nullable27, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable28 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable29 = nullable2;
				nullable2 = null;
				ColumnModel columnModel23 = c.String(nullable28, nullable1, nullable29, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel24 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel25 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable30 = null;
				DateTime? nullable31 = null;
				ColumnModel columnModel26 = c.DateTime(nullable2, nullable30, nullable31, null, null, null, null);
				nullable2 = null;
				nullable30 = null;
				nullable31 = null;
				ColumnModel columnModel27 = c.DateTime(nullable2, nullable30, nullable31, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel28 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable30 = null;
				nullable31 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, EnableDeliveryReceiptEmailed = columnModel2, EnableInvoiceEmailed = columnModel3, EnableStatementEmailed = columnModel4, AllowOnlinePayments = columnModel5, EnablePrintedDeliveryReceipt = columnModel6, EnablePrintedInvoice = columnModel7, EnablePrintedStatement = columnModel8, EnablePrePurchase = columnModel9, EnableInstallmentBilling = columnModel10, EnableBudgetBilling = columnModel11, EnableCapping = columnModel12, EnableAutoPay = columnModel13, EnableReminderOne = columnModel14, EnableReminderOneMessage = columnModel15, EnableReminderTwo = columnModel16, EnableReminderTwoMessage = columnModel17, EnableReminderThree = columnModel18, EnableReminderThreeMessage = columnModel19, EnableCODWarning = columnModel20, EnableCODWarningMessage = columnModel21, EnableCOD = columnModel22, EnableCODMessage = columnModel23, IsDeleted = columnModel24, DeleterUserId = columnModel25, DeletionTime = columnModel26, LastModificationTime = columnModel27, LastModifierUserId = columnModel28, CreationTime = c.DateTime(new bool?(false), nullable30, nullable31, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}