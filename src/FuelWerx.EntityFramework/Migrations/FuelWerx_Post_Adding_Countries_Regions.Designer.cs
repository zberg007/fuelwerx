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
	public sealed class FuelWerx_Post_Adding_Countries_Regions : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Post_Adding_Countries_Regions));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201601230641119_FuelWerx_Post_Adding_Countries_Regions";
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

		public FuelWerx_Post_Adding_Countries_Regions()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxCountryRegions", "CountryId", "dbo.FuelWerxCountries", null);
			base.DropIndex("dbo.FuelWerxCountryRegions", new string[] { "CountryId" }, null);
			base.DropTable("dbo.FuelWerxCountryRegions", new Dictionary<string, object>()
			{
				{ "DynamicFilter_CountryRegion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxCountries", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Country_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Country_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxCountries", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel1 = c.String(nullable2, nullable, nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(nullable4, nullable, nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				long? nullable6 = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable1 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable1 = null;
				nullable6 = null;
				return new { Id = columnModel, Code = columnModel1, Name = columnModel2, IsDeleted = columnModel3, DeleterUserId = columnModel4, DeletionTime = columnModel5, LastModificationTime = columnModel6, LastModifierUserId = columnModel7, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable6, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_CountryRegion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxCountryRegions", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel1 = c.String(nullable2, nullable, nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(nullable4, nullable, nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				long? nullable6 = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable1 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel8 = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, Code = columnModel1, Name = columnModel2, IsDeleted = columnModel3, DeleterUserId = columnModel4, DeletionTime = columnModel5, LastModificationTime = columnModel6, LastModifierUserId = columnModel7, CreationTime = columnModel8, CreatorUserId = columnModel9, CountryId = c.Int(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCountries", (t) => t.CountryId, false, null, null).Index((t) => t.CountryId, null, false, false, null);
		}
	}
}