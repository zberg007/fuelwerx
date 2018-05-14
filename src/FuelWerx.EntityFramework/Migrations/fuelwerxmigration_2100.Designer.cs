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
	public sealed class fuelwerxmigration_2100 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_2100));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201604170754109_fuelwerxmigration_2100";
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

		public fuelwerxmigration_2100()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProductSpecificPrices", "ProductOptionId", "dbo.FuelWerxProductOptions", null);
			base.DropForeignKey("dbo.FuelWerxProductSpecificPrices", "ProductId", "dbo.FuelWerxProducts", null);
			base.DropIndex("dbo.FuelWerxProductSpecificPrices", new string[] { "ProductOptionId" }, null);
			base.DropIndex("dbo.FuelWerxProductSpecificPrices", new string[] { "ProductId" }, null);
			base.DropTable("dbo.FuelWerxProductSpecificPrices", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductSpecificPrice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductSpecificPrice_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductSpecificPrice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProductSpecificPrice_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProductSpecificPrices", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				decimal? nullable2 = null;
				ColumnModel columnModel3 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable2, null, null, null, false, null);
				bool? nullable3 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable3, null, null, null, null);
				nullable = null;
				ColumnModel columnModel5 = c.Long(new bool?(true), false, nullable, null, null, null, null);
				nullable3 = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel6 = c.DateTime(nullable3, nullable4, nullable5, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel7 = c.DateTime(nullable3, nullable4, nullable5, null, null, null, null);
				nullable3 = null;
				nullable1 = null;
				ColumnModel columnModel8 = c.Int(nullable3, false, nullable1, null, null, null, null);
				nullable3 = null;
				bool? nullable6 = nullable3;
				nullable1 = null;
				nullable3 = null;
				bool? nullable7 = nullable3;
				nullable3 = null;
				ColumnModel columnModel9 = c.String(nullable6, nullable1, nullable7, nullable3, null, null, null, null, null);
				nullable3 = null;
				nullable1 = null;
				ColumnModel columnModel10 = c.Int(nullable3, false, nullable1, null, null, null, null);
				nullable3 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable3, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable = null;
				ColumnModel columnModel12 = c.Long(nullable3, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable2 = null;
				ColumnModel columnModel13 = c.Decimal(nullable3, new byte?((byte)18), new byte?((byte)2), nullable2, null, null, null, false, null);
				nullable3 = null;
				bool? nullable8 = nullable3;
				nullable1 = null;
				nullable3 = null;
				bool? nullable9 = nullable3;
				nullable3 = null;
				ColumnModel columnModel14 = c.String(nullable8, nullable1, nullable9, nullable3, null, null, null, null, null);
				nullable3 = null;
				bool? nullable10 = nullable3;
				nullable3 = null;
				ColumnModel columnModel15 = c.Boolean(nullable10, nullable3, null, null, null, null);
				nullable3 = null;
				ColumnModel columnModel16 = c.Boolean(new bool?(false), nullable3, null, null, null, null);
				nullable3 = null;
				nullable = null;
				ColumnModel columnModel17 = c.Long(nullable3, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel18 = c.DateTime(nullable3, nullable4, nullable5, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel19 = c.DateTime(nullable3, nullable4, nullable5, null, null, null, null);
				nullable3 = null;
				nullable = null;
				ColumnModel columnModel20 = c.Long(nullable3, false, nullable, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable3 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ProductId = columnModel2, Cost = columnModel3, BaseCostOverride = columnModel4, ProductOptionId = columnModel5, AvailableFrom = columnModel6, AvailableTo = columnModel7, StartingAtQuantity = columnModel8, ForCurrency = columnModel9, ForCountryId = columnModel10, ForOrganizationalUnitId = columnModel11, ForCustomerId = columnModel12, Discount = columnModel13, DiscountType = columnModel14, DiscountIncludeTax = columnModel15, IsDeleted = columnModel16, DeleterUserId = columnModel17, DeletionTime = columnModel18, LastModificationTime = columnModel19, LastModifierUserId = columnModel20, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable3, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProducts", (t) => t.ProductId, true, null, null).ForeignKey("dbo.FuelWerxProductOptions", (t) => t.ProductOptionId, false, null, null).Index((t) => t.ProductId, null, false, false, null).Index((t) => t.ProductOptionId, null, false, false, null);
		}
	}
}