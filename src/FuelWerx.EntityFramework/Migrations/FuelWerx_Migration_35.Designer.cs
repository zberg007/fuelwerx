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
	public sealed class FuelWerx_Migration_35 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_35));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602070305521_FuelWerx_Migration_35";
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

		public FuelWerx_Migration_35()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProductSuppliers", "SupplierId", "dbo.FuelWerxSuppliers", null);
			base.DropForeignKey("dbo.FuelWerxProductSuppliers", "ProductId", "dbo.FuelWerxProducts", null);
			base.DropForeignKey("dbo.FuelWerxProductResources", "ProductId", "dbo.FuelWerxProducts", null);
			base.DropForeignKey("dbo.FuelWerxProductResources", "BinaryObjectId", "dbo.AppBinaryObjects", null);
			base.DropForeignKey("dbo.FuelWerxProductPrices", "TaxRuleId", "dbo.FuelWerxTaxRules", null);
			base.DropForeignKey("dbo.FuelWerxProductPrices", "ProductId", "dbo.FuelWerxProducts", null);
			base.DropForeignKey("dbo.FuelWerxProductOptions", "ProductId", "dbo.FuelWerxProducts", null);
			base.DropIndex("dbo.FuelWerxProductSuppliers", new string[] { "SupplierId" }, null);
			base.DropIndex("dbo.FuelWerxProductSuppliers", new string[] { "ProductId" }, null);
			base.DropIndex("dbo.FuelWerxProductResources", new string[] { "BinaryObjectId" }, null);
			base.DropIndex("dbo.FuelWerxProductResources", new string[] { "ProductId" }, null);
			base.DropIndex("dbo.FuelWerxProductPrices", new string[] { "TaxRuleId" }, null);
			base.DropIndex("dbo.FuelWerxProductPrices", new string[] { "ProductId" }, null);
			base.DropIndex("dbo.FuelWerxProductOptions", new string[] { "ProductId" }, null);
			base.DropTable("dbo.FuelWerxProductSuppliers", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductSupplier_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductSupplier_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProductResources", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProductPrices", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductPrice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductPrice_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProducts", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Product_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Product_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProductOptions", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductOption_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductOption_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductOption_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductOption_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProductOptions", (ColumnBuilder c) => {
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
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable3, new int?(600), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(nullable5, new int?(1200), nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel10 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ProductId = columnModel2, Name = columnModel3, Value = columnModel4, Comment = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProducts", (t) => t.ProductId, true, null, null).Index((t) => t.ProductId, null, false, false, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Product_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Product_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProducts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				ColumnModel columnModel2 = c.Guid(nullable2, false, null, null, null, null, null);
				nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable4, new int?(255), nullable5, nullable2, null, null, null, null, null);
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
				nullable1 = null;
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(new bool?(false), nullable1, nullable9, nullable2, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel9 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
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
				return new { Id = columnModel, TenantId = columnModel1, ImageId = columnModel2, Name = columnModel3, Reference = columnModel4, Sku = columnModel5, BasePrice = columnModel6, FinalPrice = columnModel7, QuantitySoldIn = columnModel8, QuantityOnHand = columnModel9, IsActive = columnModel10, IsDeleted = columnModel11, DeleterUserId = columnModel12, DeletionTime = columnModel13, LastModificationTime = columnModel14, LastModifierUserId = columnModel15, CreationTime = c.DateTime(new bool?(false), nullable10, nullable11, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductPrice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductPrice_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProductPrices", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel3 = c.Long(nullable1, false, nullable, null, null, null, null);
				decimal? nullable2 = null;
				ColumnModel columnModel4 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable2, null, null, null, false, null);
				nullable2 = null;
				ColumnModel columnModel5 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable2, null, null, null, false, null);
				nullable1 = null;
				nullable2 = null;
				ColumnModel columnModel6 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable2, null, null, null, false, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				byte? nullable3 = null;
				DateTime? nullable4 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel10 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel12 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel13 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel14 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ProductId = columnModel2, TaxRuleId = columnModel3, Cost = columnModel4, UnitPrice = columnModel5, SpecialDeliveryFee = columnModel6, Historical = columnModel7, MarkedHistoricalAt = columnModel8, IsActive = columnModel9, IsDeleted = columnModel10, DeleterUserId = columnModel11, DeletionTime = columnModel12, LastModificationTime = columnModel13, LastModifierUserId = columnModel14, CreationTime = c.DateTime(new bool?(false), nullable3, nullable4, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProducts", (t) => t.ProductId, true, null, null).ForeignKey("dbo.FuelWerxTaxRules", (t) => t.TaxRuleId, false, null, null).Index((t) => t.ProductId, null, false, false, null).Index((t) => t.TaxRuleId, null, false, false, null);
			Dictionary<string, object> strs3 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProductResources", (ColumnBuilder c) => {
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
				return new { Id = columnModel, TenantId = columnModel1, Category = columnModel2, ProductId = columnModel3, BinaryObjectId = columnModel4, Name = columnModel5, Description = columnModel6, FileName = columnModel7, FileExtension = columnModel8, FileSize = columnModel9, IsActive = columnModel10, IsDeleted = columnModel11, DeleterUserId = columnModel12, DeletionTime = columnModel13, LastModificationTime = columnModel14, LastModifierUserId = columnModel15, CreationTime = c.DateTime(new bool?(false), nullable10, nullable11, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs3, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AppBinaryObjects", (t) => t.BinaryObjectId, true, null, null).ForeignKey("dbo.FuelWerxProducts", (t) => t.ProductId, true, null, null).Index((t) => t.ProductId, null, false, false, null).Index((t) => t.BinaryObjectId, null, false, false, null);
			Dictionary<string, object> strs4 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductSupplier_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductSupplier_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProductSuppliers", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
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
				return new { Id = columnModel, TenantId = columnModel1, ProductId = columnModel2, SupplierId = columnModel3, IsActive = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs4, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProducts", (t) => t.ProductId, true, null, null).ForeignKey("dbo.FuelWerxSuppliers", (t) => t.SupplierId, true, null, null).Index((t) => t.ProductId, null, false, false, null).Index((t) => t.SupplierId, null, false, false, null);
		}
	}
}