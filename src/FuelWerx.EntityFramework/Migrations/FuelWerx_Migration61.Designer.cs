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
	public sealed class FuelWerx_Migration61 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration61));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602170316072_FuelWerx_Migration61";
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

		public FuelWerx_Migration61()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxEstimateResources", "EstimateId", "dbo.FuelWerxEstimates", null);
			base.DropForeignKey("dbo.FuelWerxEstimateTasks", "EstimateId", "dbo.FuelWerxEstimates", null);
			base.DropForeignKey("dbo.FuelWerxEstimates", "CustomerId", "dbo.FuelWerxCustomers", null);
			base.DropForeignKey("dbo.FuelWerxEstimateResources", "BinaryObjectId", "dbo.AppBinaryObjects", null);
			base.DropIndex("dbo.FuelWerxEstimateTasks", new string[] { "EstimateId" }, null);
			base.DropIndex("dbo.FuelWerxEstimates", new string[] { "CustomerId" }, null);
			base.DropIndex("dbo.FuelWerxEstimateResources", new string[] { "BinaryObjectId" }, null);
			base.DropIndex("dbo.FuelWerxEstimateResources", new string[] { "EstimateId" }, null);
			base.DropTable("dbo.FuelWerxEstimateTasks", new Dictionary<string, object>()
			{
				{ "DynamicFilter_EstimateTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EstimateTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxEstimates", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Estimate_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Estimate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxEstimateResources", new Dictionary<string, object>()
			{
				{ "DynamicFilter_EstimateResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EstimateResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_EstimateResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EstimateResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxEstimateResources", (ColumnBuilder c) => {
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
				return new { Id = columnModel, TenantId = columnModel1, Category = columnModel2, EstimateId = columnModel3, BinaryObjectId = columnModel4, Name = columnModel5, Description = columnModel6, FileName = columnModel7, FileExtension = columnModel8, FileSize = columnModel9, IsActive = columnModel10, IsDeleted = columnModel11, DeleterUserId = columnModel12, DeletionTime = columnModel13, LastModificationTime = columnModel14, LastModifierUserId = columnModel15, CreationTime = c.DateTime(new bool?(false), nullable10, nullable11, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AppBinaryObjects", (t) => t.BinaryObjectId, true, null, null).ForeignKey("dbo.FuelWerxEstimates", (t) => t.EstimateId, true, null, null).Index((t) => t.EstimateId, null, false, false, null).Index((t) => t.BinaryObjectId, null, false, false, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Estimate_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Estimate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxEstimates", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable4, new int?(38), nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel5 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable8, new int?(99), nullable9, nullable2, null, null, null, null, null);
				decimal? nullable10 = null;
				ColumnModel columnModel7 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel8 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel9 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel10 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel11 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel12 = c.String(nullable11, nullable1, nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.String(nullable13, new int?(50), nullable14, nullable2, null, null, null, null, null);
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
				nullable1 = null;
				nullable2 = null;
				bool? nullable19 = nullable2;
				nullable2 = null;
				ColumnModel columnModel16 = c.String(nullable18, nullable1, nullable19, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel17 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel18 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel19 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel20 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel21 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel22 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, CustomerId = columnModel2, Label = columnModel3, Number = columnModel4, Date = columnModel5, PONumber = columnModel6, Discount = columnModel7, Rate = columnModel8, Hours = columnModel9, Tax = columnModel10, LineTotal = columnModel11, Terms = columnModel12, CurrentStatus = columnModel13, LogDataAndTasksVisibleToCustomer = columnModel14, TimeEntryLog = columnModel15, Description = columnModel16, IsActive = columnModel17, IsDeleted = columnModel18, DeleterUserId = columnModel19, DeletionTime = columnModel20, LastModificationTime = columnModel21, LastModifierUserId = columnModel22, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCustomers", (t) => t.CustomerId, true, null, null).Index((t) => t.CustomerId, null, false, false, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_EstimateTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EstimateTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxEstimateTasks", (ColumnBuilder c) => {
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
				return new { Id = columnModel, TenantId = columnModel1, EstimateId = columnModel2, Name = columnModel3, Cost = columnModel4, Discount = columnModel5, Comment = columnModel6, IsActive = columnModel7, IsDeleted = columnModel8, DeleterUserId = columnModel9, DeletionTime = columnModel10, LastModificationTime = columnModel11, LastModifierUserId = columnModel12, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxEstimates", (t) => t.EstimateId, true, null, null).Index((t) => t.EstimateId, null, false, false, null);
		}
	}
}