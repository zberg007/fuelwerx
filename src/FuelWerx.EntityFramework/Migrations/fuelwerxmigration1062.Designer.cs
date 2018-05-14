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
	public sealed class fuelwerxmigration1062 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1062));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603220319591_fuelwerxmigration1062";
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

		public fuelwerxmigration1062()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.FuelWerxProductPrices", "TaxRuleId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.DropForeignKey("dbo.FuelWerxTaxRuleRules", "TaxRuleId", "dbo.FuelWerxTaxRules", null);
			base.DropForeignKey("dbo.FuelWerxTaxRuleRules", "TaxId", "dbo.FuelWerxTaxes", null);
			base.DropForeignKey("dbo.FuelWerxTaxRuleRules", "CountryRegionId", "dbo.FuelWerxCountryRegions", null);
			base.DropForeignKey("dbo.FuelWerxTaxRuleRules", "CountryId", "dbo.FuelWerxCountries", null);
			base.DropForeignKey("dbo.FuelWerxProductPriceTaxRules", "TaxRuleId", "dbo.FuelWerxTaxRules", null);
			base.DropForeignKey("dbo.FuelWerxProductPriceTaxRules", "ProductPriceId", "dbo.FuelWerxProductPrices", null);
			base.DropIndex("dbo.FuelWerxTaxRuleRules", new string[] { "TaxId" }, null);
			base.DropIndex("dbo.FuelWerxTaxRuleRules", new string[] { "CountryRegionId" }, null);
			base.DropIndex("dbo.FuelWerxTaxRuleRules", new string[] { "CountryId" }, null);
			base.DropIndex("dbo.FuelWerxTaxRuleRules", new string[] { "TaxRuleId" }, null);
			base.DropIndex("dbo.FuelWerxProductPriceTaxRules", new string[] { "TaxRuleId" }, null);
			base.DropIndex("dbo.FuelWerxProductPriceTaxRules", new string[] { "ProductPriceId" }, null);
			base.DropTable("dbo.FuelWerxTaxRuleRules", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TaxRuleRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProductPriceTaxRules", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductPriceTaxRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxFIPs", new Dictionary<string, object>()
			{
				{ "DynamicFilter_FIP_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.CreateIndex("dbo.FuelWerxProductPrices", "TaxRuleId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxProductPrices", "TaxRuleId", "dbo.FuelWerxTaxRules", "Id", false, null, null);
		}

		public override void Up()
		{
			base.DropForeignKey("dbo.FuelWerxProductPrices", "TaxRuleId", "dbo.FuelWerxTaxRules", null);
			base.DropIndex("dbo.FuelWerxProductPrices", new string[] { "TaxRuleId" }, null);
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_FIP_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxFIPs", (ColumnBuilder c) => {
				ColumnModel columnModel = c.Int(new bool?(false), true, null, null, null, null, null);
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				ColumnModel columnModel1 = c.String(nullable1, new int?(110), nullable2, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				bool? nullable4 = nullable;
				nullable = null;
				ColumnModel columnModel2 = c.String(nullable3, new int?(110), nullable4, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable5 = nullable;
				nullable = null;
				bool? nullable6 = nullable;
				nullable = null;
				ColumnModel columnModel3 = c.String(nullable5, new int?(4), nullable6, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable7 = nullable;
				nullable = null;
				bool? nullable8 = nullable;
				nullable = null;
				ColumnModel columnModel4 = c.String(nullable7, new int?(6), nullable8, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable9 = nullable;
				nullable = null;
				bool? nullable10 = nullable;
				nullable = null;
				ColumnModel columnModel5 = c.String(nullable9, new int?(12), nullable10, nullable, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				long? nullable11 = null;
				ColumnModel columnModel7 = c.Long(nullable, false, nullable11, null, null, null, null);
				nullable = null;
				byte? nullable12 = null;
				DateTime? nullable13 = null;
				ColumnModel columnModel8 = c.DateTime(nullable, nullable12, nullable13, null, null, null, null);
				nullable = null;
				nullable12 = null;
				nullable13 = null;
				ColumnModel columnModel9 = c.DateTime(nullable, nullable12, nullable13, null, null, null, null);
				nullable = null;
				nullable11 = null;
				ColumnModel columnModel10 = c.Long(nullable, false, nullable11, null, null, null, null);
				nullable12 = null;
				nullable13 = null;
				nullable = null;
				nullable11 = null;
				return new { Id = columnModel, StateName = columnModel1, CountyName = columnModel2, FIPsState = columnModel3, FIPsCounty = columnModel4, FIPsStateCounty = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable12, nullable13, null, null, null, null), CreatorUserId = c.Long(nullable, false, nullable11, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProductPriceTaxRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProductPriceTaxRules", (ColumnBuilder c) => {
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
				return new { Id = columnModel, ProductPriceId = columnModel1, TaxRuleId = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProductPrices", (t) => t.ProductPriceId, true, null, null).ForeignKey("dbo.FuelWerxTaxRules", (t) => t.TaxRuleId, true, null, null).Index((t) => t.ProductPriceId, null, false, false, null).Index((t) => t.TaxRuleId, null, false, false, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TaxRuleRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTaxRuleRules", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel2 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				nullable1 = null;
				ColumnModel columnModel3 = c.Int(nullable2, false, nullable1, null, null, null, null);
				nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable3, new int?(15), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(40), nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(new bool?(false), new int?(255), nullable6, nullable2, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel7 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel11 = c.DateTime(nullable2, nullable7, nullable8, null, null, null, null);
				nullable2 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel12 = c.DateTime(nullable2, nullable7, nullable8, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel13 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TaxRuleId = columnModel1, CountryId = columnModel2, CountryRegionId = columnModel3, PostalCodeRange = columnModel4, Behavior = columnModel5, Caption = columnModel6, TaxId = columnModel7, IsActive = columnModel8, IsDeleted = columnModel9, DeleterUserId = columnModel10, DeletionTime = columnModel11, LastModificationTime = columnModel12, LastModifierUserId = columnModel13, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCountries", (t) => t.CountryId, true, null, null).ForeignKey("dbo.FuelWerxCountryRegions", (t) => t.CountryRegionId, false, null, null).ForeignKey("dbo.FuelWerxTaxes", (t) => t.TaxId, true, null, null).ForeignKey("dbo.FuelWerxTaxRules", (t) => t.TaxRuleId, true, null, null).Index((t) => t.TaxRuleId, null, false, false, null).Index((t) => t.CountryId, null, false, false, null).Index((t) => t.CountryRegionId, null, false, false, null).Index((t) => t.TaxId, null, false, false, null);
			base.DropColumn("dbo.FuelWerxProductPrices", "TaxRuleId", null);
		}
	}
}