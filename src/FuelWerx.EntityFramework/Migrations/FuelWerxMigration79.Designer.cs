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
	public sealed class FuelWerxMigration79 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration79));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602260534074_FuelWerxMigration79";
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

		public FuelWerxMigration79()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxInvoiceResources", "InvoiceId", "dbo.FuelWerxInvoices", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceTasks", "InvoiceId", "dbo.FuelWerxInvoices", null);
			base.DropForeignKey("dbo.FuelWerxInvoices", "CustomerId", "dbo.FuelWerxCustomers", null);
			base.DropForeignKey("dbo.FuelWerxInvoiceResources", "BinaryObjectId", "dbo.AppBinaryObjects", null);
			base.DropIndex("dbo.FuelWerxInvoiceTasks", new string[] { "InvoiceId" }, null);
			base.DropIndex("dbo.FuelWerxInvoices", new string[] { "CustomerId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceResources", new string[] { "BinaryObjectId" }, null);
			base.DropIndex("dbo.FuelWerxInvoiceResources", new string[] { "InvoiceId" }, null);
			base.DropTable("dbo.FuelWerxInvoiceTasks", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_InvoiceTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxInvoices", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Invoice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Invoice_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxInvoiceResources", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_InvoiceResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_InvoiceResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceResources", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), nullable1, nullable3, nullable2, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				ColumnModel columnModel4 = c.Guid(new bool?(false), false, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(255), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable5, new int?(1200), nullable6, nullable2, null, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), nullable1, nullable7, nullable2, null, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(new bool?(false), nullable1, nullable8, nullable2, null, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.String(new bool?(false), nullable1, nullable9, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel10 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel11 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel12 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable10 = null;
				DateTime? nullable11 = null;
				ColumnModel columnModel13 = c.DateTime(nullable2, nullable10, nullable11, null, null, null, null);
				nullable2 = null;
				nullable10 = null;
				nullable11 = null;
				ColumnModel columnModel14 = c.DateTime(nullable2, nullable10, nullable11, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel15 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable10 = null;
				nullable11 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Category = columnModel2, InvoiceId = columnModel3, BinaryObjectId = columnModel4, Name = columnModel5, Description = columnModel6, FileName = columnModel7, FileExtension = columnModel8, FileSize = columnModel9, IsActive = columnModel10, IsDeleted = columnModel11, DeleterUserId = columnModel12, DeletionTime = columnModel13, LastModificationTime = columnModel14, LastModifierUserId = columnModel15, CreationTime = c.DateTime(new bool?(false), nullable10, nullable11, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AppBinaryObjects", (t) => t.BinaryObjectId, true, null, null).ForeignKey("dbo.FuelWerxInvoices", (t) => t.InvoiceId, true, null, null).Index((t) => t.InvoiceId, null, false, false, null).Index((t) => t.BinaryObjectId, null, false, false, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Invoice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Invoice_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoices", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(38), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel4 = c.DateTime(nullable2, nullable4, nullable5, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable6, new int?(99), nullable7, nullable2, null, null, null, null, null);
				decimal? nullable8 = null;
				ColumnModel columnModel6 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable8 = null;
				ColumnModel columnModel7 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable8 = null;
				ColumnModel columnModel8 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable8 = null;
				ColumnModel columnModel9 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable8 = null;
				ColumnModel columnModel10 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable8 = null;
				ColumnModel columnModel11 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel12 = c.String(nullable9, nullable1, nullable10, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.String(nullable11, new int?(50), nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				ColumnModel columnModel14 = c.Boolean(nullable13, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel15 = c.Boolean(nullable14, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable2 = null;
				ColumnModel columnModel16 = c.String(nullable15, nullable1, nullable16, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable2 = null;
				ColumnModel columnModel17 = c.String(nullable17, nullable1, nullable18, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel18 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel19 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel20 = c.DateTime(nullable2, nullable4, nullable5, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel21 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel22 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel23 = c.DateTime(nullable2, nullable4, nullable5, null, null, null, null);
				nullable2 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel24 = c.DateTime(nullable2, nullable4, nullable5, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel25 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, CustomerId = columnModel2, Number = columnModel3, Date = columnModel4, PONumber = columnModel5, Discount = columnModel6, Rate = columnModel7, Quantity = columnModel8, Hours = columnModel9, Tax = columnModel10, LineTotal = columnModel11, Terms = columnModel12, CurrentStatus = columnModel13, TimeEntryVisibleToCustomer = columnModel14, NotesVisibleToCustomer = columnModel15, TimeEntryLog = columnModel16, Description = columnModel17, IsActive = columnModel18, ProjectId = columnModel19, ProjectAcceptedTime = columnModel20, IsDeleted = columnModel21, DeleterUserId = columnModel22, DeletionTime = columnModel23, LastModificationTime = columnModel24, LastModifierUserId = columnModel25, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCustomers", (t) => t.CustomerId, true, null, null).Index((t) => t.CustomerId, null, false, false, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoiceTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_InvoiceTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoiceTasks", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				decimal? nullable3 = null;
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
				return new { Id = columnModel, TenantId = columnModel1, InvoiceId = columnModel2, Name = columnModel3, Cost = columnModel4, Discount = columnModel5, Comment = columnModel6, IsComplete = columnModel7, IsActive = columnModel8, IsDeleted = columnModel9, DeleterUserId = columnModel10, DeletionTime = columnModel11, LastModificationTime = columnModel12, LastModifierUserId = columnModel13, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxInvoices", (t) => t.InvoiceId, true, null, null).Index((t) => t.InvoiceId, null, false, false, null);
		}
	}
}