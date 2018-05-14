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
	public sealed class fuelwerxmigration85 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration85));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602290012186_fuelwerxmigration85";
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

		public fuelwerxmigration85()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxTenantDetails", "MailCountryRegionId", "dbo.FuelWerxCountryRegions", null);
			base.DropForeignKey("dbo.FuelWerxTenantDetails", "MailCountryId", "dbo.FuelWerxCountries", null);
			base.DropForeignKey("dbo.FuelWerxTenantDetails", "CountryRegionId", "dbo.FuelWerxCountryRegions", null);
			base.DropForeignKey("dbo.FuelWerxTenantDetails", "CountryId", "dbo.FuelWerxCountries", null);
			base.DropIndex("dbo.FuelWerxTenantDetails", new string[] { "MailCountryRegionId" }, null);
			base.DropIndex("dbo.FuelWerxTenantDetails", new string[] { "MailCountryId" }, null);
			base.DropIndex("dbo.FuelWerxTenantDetails", new string[] { "CountryRegionId" }, null);
			base.DropIndex("dbo.FuelWerxTenantDetails", new string[] { "CountryId" }, null);
			base.DropTable("dbo.FuelWerxTenantDetails", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantDetail_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantDetail_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantDetails", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				ColumnModel columnModel2 = c.Guid(nullable2, false, null, null, null, null, null);
				nullable2 = null;
				bool? nullable3 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(nullable3, nullable1, nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable5, new int?(16), nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable7, new int?(16), nullable8, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable9, new int?(16), nullable10, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), new int?(255), nullable11, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(nullable12, new int?(255), nullable13, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.String(new bool?(false), new int?(255), nullable14, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable2 = null;
				ColumnModel columnModel10 = c.String(nullable15, new int?(255), nullable16, nullable2, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel11 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable2 = null;
				nullable1 = null;
				ColumnModel columnModel12 = c.Int(nullable2, false, nullable1, null, null, null, null);
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.String(new bool?(false), new int?(255), nullable17, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable2 = null;
				bool? nullable19 = nullable2;
				nullable2 = null;
				ColumnModel columnModel14 = c.String(nullable18, new int?(255), nullable19, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable20 = nullable2;
				nullable2 = null;
				ColumnModel columnModel15 = c.String(new bool?(false), new int?(255), nullable20, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable21 = nullable2;
				nullable2 = null;
				bool? nullable22 = nullable2;
				nullable2 = null;
				ColumnModel columnModel16 = c.String(nullable21, new int?(255), nullable22, nullable2, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel17 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable2 = null;
				nullable1 = null;
				ColumnModel columnModel18 = c.Int(nullable2, false, nullable1, null, null, null, null);
				nullable2 = null;
				bool? nullable23 = nullable2;
				nullable2 = null;
				bool? nullable24 = nullable2;
				nullable2 = null;
				ColumnModel columnModel19 = c.String(nullable23, new int?(255), nullable24, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel20 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel21 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel22 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable25 = null;
				DateTime? nullable26 = null;
				ColumnModel columnModel23 = c.DateTime(nullable2, nullable25, nullable26, null, null, null, null);
				nullable2 = null;
				nullable25 = null;
				nullable26 = null;
				ColumnModel columnModel24 = c.DateTime(nullable2, nullable25, nullable26, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel25 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable25 = null;
				nullable26 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, PictureId = columnModel2, Note = columnModel3, PhoneNumber = columnModel4, PhoneNumberSecondary = columnModel5, FaxNumber = columnModel6, Address = columnModel7, SecondaryAddress = columnModel8, City = columnModel9, PostalCode = columnModel10, CountryId = columnModel11, CountryRegionId = columnModel12, MailAddress = columnModel13, MailSecondaryAddress = columnModel14, MailCity = columnModel15, MailPostalCode = columnModel16, MailCountryId = columnModel17, MailCountryRegionId = columnModel18, Email = columnModel19, IsActive = columnModel20, IsDeleted = columnModel21, DeleterUserId = columnModel22, DeletionTime = columnModel23, LastModificationTime = columnModel24, LastModifierUserId = columnModel25, CreationTime = c.DateTime(new bool?(false), nullable25, nullable26, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCountries", (t) => t.CountryId, false, null, null).ForeignKey("dbo.FuelWerxCountryRegions", (t) => t.CountryRegionId, false, null, null).ForeignKey("dbo.FuelWerxCountries", (t) => t.MailCountryId, false, null, null).ForeignKey("dbo.FuelWerxCountryRegions", (t) => t.MailCountryRegionId, false, null, null).Index((t) => t.CountryId, null, false, false, null).Index((t) => t.CountryRegionId, null, false, false, null).Index((t) => t.MailCountryId, null, false, false, null).Index((t) => t.MailCountryRegionId, null, false, false, null);
		}
	}
}