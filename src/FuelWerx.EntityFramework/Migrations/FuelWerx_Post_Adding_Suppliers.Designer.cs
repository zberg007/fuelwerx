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
	public sealed class FuelWerx_Post_Adding_Suppliers : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Post_Adding_Suppliers));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201601230720221_FuelWerx_Post_Adding_Suppliers";
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

		public FuelWerx_Post_Adding_Suppliers()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxSuppliers", "CountryRegionId", "dbo.FuelWerxCountryRegions", null);
			base.DropForeignKey("dbo.FuelWerxSuppliers", "CountryId", "dbo.FuelWerxCountries", null);
			base.DropIndex("dbo.FuelWerxSuppliers", new string[] { "CountryId" }, null);
			base.DropIndex("dbo.FuelWerxSuppliers", new string[] { "CountryRegionId" }, null);
			base.DropTable("dbo.FuelWerxSuppliers", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Supplier_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Supplier_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Supplier_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Supplier_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxSuppliers", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(nullable4, nullable1, nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(16), nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable7, new int?(16), nullable8, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(new bool?(false), new int?(255), nullable9, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(nullable10, new int?(255), nullable11, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(new bool?(false), new int?(255), nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.String(nullable13, new int?(255), nullable14, nullable2, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel10 = c.Int(new bool?(true), false, nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel11 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable2 = null;
				ColumnModel columnModel12 = c.String(nullable15, new int?(255), nullable16, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.String(nullable17, new int?(255), nullable18, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel14 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel15 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel16 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable19 = null;
				DateTime? nullable20 = null;
				ColumnModel columnModel17 = c.DateTime(nullable2, nullable19, nullable20, null, null, null, null);
				nullable2 = null;
				nullable19 = null;
				nullable20 = null;
				ColumnModel columnModel18 = c.DateTime(nullable2, nullable19, nullable20, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel19 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable19 = null;
				nullable20 = null;
				ColumnModel columnModel20 = c.DateTime(new bool?(false), nullable19, nullable20, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel21 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, Description = columnModel3, PhoneNumber = columnModel4, MobilePhoneNumber = columnModel5, Address = columnModel6, SecondaryAddress = columnModel7, City = columnModel8, PostalCode = columnModel9, CountryRegionId = columnModel10, CountryId = columnModel11, ContactName = columnModel12, ContactEmail = columnModel13, IsActive = columnModel14, IsDeleted = columnModel15, DeleterUserId = columnModel16, DeletionTime = columnModel17, LastModificationTime = columnModel18, LastModifierUserId = columnModel19, CreationTime = columnModel20, CreatorUserId = columnModel21, LogoId = c.Guid(nullable2, false, null, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCountries", (t) => t.CountryId, false, null, null).ForeignKey("dbo.FuelWerxCountryRegions", (t) => t.CountryRegionId, false, null, null).Index((t) => t.CountryRegionId, null, false, false, null).Index((t) => t.CountryId, null, false, false, null);
		}
	}
}