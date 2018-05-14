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
	public sealed class FuelWerxMigration_2097 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration_2097));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201607180532201_FuelWerxMigration_2097";
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

		public FuelWerxMigration_2097()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.FuelWerxInvoices", "ProjectAcceptedTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "NotesVisibleToCustomer", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.Boolean(nullable1, nullable, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxInvoices", "TimeEntryVisibleToCustomer", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.Boolean(nullable1, nullable, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxInvoices", "Quantity", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.DropForeignKey("dbo.FuelWerxInvoiceTeamMembers", "TeamMemberId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceTeamMembers", "InvoiceId", "dbo.FuelWerxInvoices", null);
			base.DropForeignKey("dbo.FuelWerxInvoices", "ProjectId", "dbo.FuelWerxProjects", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceProducts", "ProductId", "dbo.FuelWerxProducts", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceProducts", "LineItemId", "dbo.FuelWerxInvoiceProductLineItems", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceProductLineItemOptions", "ProductLineItemId", "dbo.FuelWerxInvoiceProductLineItems", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceProductLineItems", "InvoiceId", "dbo.FuelWerxInvoices", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceProducts", "InvoiceId", "dbo.FuelWerxInvoices", null);
			base.DropForeignKey("dbo.FuelWerxInvoices", "CustomerAddressId", "dbo.FuelWerxAddresses", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceAdjustments", "InvoiceId", "dbo.FuelWerxInvoices", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceAdhocProducts", "InvoiceId", "dbo.FuelWerxInvoices", null);
			base.DropIndex("dbo.FuelWerxInvoiceTeamMembers", new string[] { "TeamMemberId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceTeamMembers", new string[] { "InvoiceId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceProductLineItemOptions", new string[] { "ProductLineItemId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceProductLineItems", new string[] { "InvoiceId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceProducts", new string[] { "LineItemId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceProducts", new string[] { "ProductId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceProducts", new string[] { "InvoiceId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceAdjustments", new string[] { "InvoiceId" }, null);
			base.DropIndex("dbo.FuelWerxInvoices", new string[] { "CustomerAddressId" }, null);
			base.DropIndex("dbo.FuelWerxInvoices", new string[] { "ProjectId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceAdhocProducts", new string[] { "InvoiceId" }, null);
			base.DropColumn("dbo.FuelWerxInvoiceTasks", "Retail", null);
			base.DropColumn("dbo.FuelWerxInvoices", "LogDataAndTasksVisibleToCustomer", null);
			base.DropColumn("dbo.FuelWerxInvoices", "TermType", null);
			base.DropColumn("dbo.FuelWerxInvoices", "HoursActual", null);
			base.DropColumn("dbo.FuelWerxInvoices", "GroupDiscount", null);
			base.DropColumn("dbo.FuelWerxInvoices", "Upcharge", null);
			base.DropColumn("dbo.FuelWerxInvoices", "DueDate", null);
			base.DropColumn("dbo.FuelWerxInvoices", "DueDateDiscountTotal", null);
			base.DropColumn("dbo.FuelWerxInvoices", "DueDateDiscountExpirationDate", null);
			base.DropColumn("dbo.FuelWerxInvoices", "BillingType", null);
			base.DropColumn("dbo.FuelWerxInvoices", "Label", null);
			base.DropColumn("dbo.FuelWerxInvoices", "CustomerAddressId", null);
			base.DropTable("dbo.FuelWerxInvoiceTeamMembers", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceTeamMember_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxInvoiceProductLineItemOptions", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceProductLineItemOption_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxInvoiceProductLineItems", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceProductLineItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxInvoiceProducts", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxInvoiceAdjustments", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceAdjustment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxInvoiceAdhocProducts", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceAdhocProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceAdhocProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceAdhocProducts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				decimal? nullable3 = null;
				ColumnModel columnModel3 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				nullable3 = null;
				ColumnModel columnModel4 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				nullable3 = null;
				ColumnModel columnModel5 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel6 = c.String(nullable4, new int?(1200), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel11 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel12 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel13 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, InvoiceId = columnModel1, Name = columnModel2, Cost = columnModel3, BaseCost = columnModel4, RetailCost = columnModel5, Description = columnModel6, IsTaxable = columnModel7, IsActive = columnModel8, IsDeleted = columnModel9, DeleterUserId = columnModel10, DeletionTime = columnModel11, LastModificationTime = columnModel12, LastModifierUserId = columnModel13, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxInvoices", (t) => t.InvoiceId, true, null, null).Index((t) => t.InvoiceId, null, false, false, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceAdjustment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceAdjustments", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				decimal? nullable3 = null;
				ColumnModel columnModel3 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				nullable3 = null;
				ColumnModel columnModel4 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(nullable4, new int?(1200), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel10 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel11 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel12 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, InvoiceId = columnModel1, Name = columnModel2, Cost = columnModel3, RetailCost = columnModel4, Description = columnModel5, IsTaxable = columnModel6, IsActive = columnModel7, IsDeleted = columnModel8, DeleterUserId = columnModel9, DeletionTime = columnModel10, LastModificationTime = columnModel11, LastModifierUserId = columnModel12, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxInvoices", (t) => t.InvoiceId, true, null, null).Index((t) => t.InvoiceId, null, false, false, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceProducts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel3 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, InvoiceId = columnModel1, ProductId = columnModel2, LineItemId = columnModel3, IsActive = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxInvoices", (t) => t.InvoiceId, true, null, null).ForeignKey("dbo.FuelWerxInvoiceProductLineItems", (t) => t.LineItemId, false, null, null).ForeignKey("dbo.FuelWerxProducts", (t) => t.ProductId, true, null, null).Index((t) => t.InvoiceId, null, false, false, null).Index((t) => t.ProductId, null, false, false, null).Index((t) => t.LineItemId, null, false, false, null);
			Dictionary<string, object> strs3 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceProductLineItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceProductLineItems", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				ColumnModel columnModel3 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null);
				ColumnModel columnModel4 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, InvoiceId = columnModel1, ProductId = columnModel2, Cost = columnModel3, Quantity = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs3, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxInvoices", (t) => t.InvoiceId, true, null, null).Index((t) => t.InvoiceId, null, false, false, null);
			Dictionary<string, object> strs4 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceProductLineItemOption_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceProductLineItemOptions", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ProductLineItemId = columnModel1, ProductOptionId = columnModel2, IsDeleted = columnModel3, DeleterUserId = columnModel4, DeletionTime = columnModel5, LastModificationTime = columnModel6, LastModifierUserId = columnModel7, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs4, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxInvoiceProductLineItems", (t) => t.ProductLineItemId, true, null, null).Index((t) => t.ProductLineItemId, null, false, false, null);
			Dictionary<string, object> strs5 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceTeamMember_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceTeamMembers", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel5 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, InvoiceId = columnModel1, TeamMemberId = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs5, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxInvoices", (t) => t.InvoiceId, true, null, null).ForeignKey("dbo.AbpUsers", (t) => t.TeamMemberId, true, null, null).Index((t) => t.InvoiceId, null, false, false, null).Index((t) => t.TeamMemberId, null, false, false, null);
			base.AddColumn("dbo.FuelWerxInvoices", "CustomerAddressId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "Label", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(255), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxInvoices", "BillingType", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(155), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxInvoices", "DueDateDiscountExpirationDate", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "DueDateDiscountTotal", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "DueDate", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "Upcharge", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "GroupDiscount", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "HoursActual", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "TermType", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(255), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxInvoices", "LogDataAndTasksVisibleToCustomer", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.Boolean(nullable1, nullable, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxInvoiceTasks", "Retail", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.CreateIndex("dbo.FuelWerxInvoices", "ProjectId", false, null, false, null);
			base.CreateIndex("dbo.FuelWerxInvoices", "CustomerAddressId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxInvoices", "CustomerAddressId", "dbo.FuelWerxAddresses", "Id", false, null, null);
			base.AddForeignKey("dbo.FuelWerxInvoices", "ProjectId", "dbo.FuelWerxProjects", "Id", false, null, null);
			base.DropColumn("dbo.FuelWerxInvoices", "Quantity", null);
			base.DropColumn("dbo.FuelWerxInvoices", "TimeEntryVisibleToCustomer", null);
			base.DropColumn("dbo.FuelWerxInvoices", "NotesVisibleToCustomer", null);
			base.DropColumn("dbo.FuelWerxInvoices", "ProjectAcceptedTime", null);
		}
	}
}