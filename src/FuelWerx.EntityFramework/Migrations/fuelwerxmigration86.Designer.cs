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
	public sealed class fuelwerxmigration86 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration86));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602290252342_fuelwerxmigration86";
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

		public fuelwerxmigration86()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTenantHours", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantHour_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantHour_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantHour_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantHour_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantHours", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.String(nullable3, nullable1, nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(nullable5, nullable1, nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable7, nullable1, nullable8, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable9, nullable1, nullable10, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(nullable11, nullable1, nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.String(nullable13, nullable1, nullable14, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable2 = null;
				ColumnModel columnModel10 = c.String(nullable15, nullable1, nullable16, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel11 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable2 = null;
				ColumnModel columnModel12 = c.String(nullable17, nullable1, nullable18, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable19 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable20 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.String(nullable19, nullable1, nullable20, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel14 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable21 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable22 = nullable2;
				nullable2 = null;
				ColumnModel columnModel15 = c.String(nullable21, nullable1, nullable22, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable23 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable24 = nullable2;
				nullable2 = null;
				ColumnModel columnModel16 = c.String(nullable23, nullable1, nullable24, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel17 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable25 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable26 = nullable2;
				nullable2 = null;
				ColumnModel columnModel18 = c.String(nullable25, nullable1, nullable26, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable27 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable28 = nullable2;
				nullable2 = null;
				ColumnModel columnModel19 = c.String(nullable27, nullable1, nullable28, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel20 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable29 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable30 = nullable2;
				nullable2 = null;
				ColumnModel columnModel21 = c.String(nullable29, nullable1, nullable30, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable31 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable32 = nullable2;
				nullable2 = null;
				ColumnModel columnModel22 = c.String(nullable31, nullable1, nullable32, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel23 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel24 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel25 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable33 = null;
				DateTime? nullable34 = null;
				ColumnModel columnModel26 = c.DateTime(nullable2, nullable33, nullable34, null, null, null, null);
				nullable2 = null;
				nullable33 = null;
				nullable34 = null;
				ColumnModel columnModel27 = c.DateTime(nullable2, nullable33, nullable34, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel28 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable33 = null;
				nullable34 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Note = columnModel2, MondayOpen = columnModel3, MondayClose = columnModel4, MondayOutForLunch = columnModel5, TuesdayOpen = columnModel6, TuesdayClose = columnModel7, TuesdayOutForLunch = columnModel8, WednesdayOpen = columnModel9, WednesdayClose = columnModel10, WednesdayOutForLunch = columnModel11, ThursdayOpen = columnModel12, ThursdayClose = columnModel13, ThursdayOutForLunch = columnModel14, FridayOpen = columnModel15, FridayClose = columnModel16, FridayOutForLunch = columnModel17, SaturdayOpen = columnModel18, SaturdayClose = columnModel19, SaturdayOutForLunch = columnModel20, SundayOpen = columnModel21, SundayClose = columnModel22, SundayOutForLunch = columnModel23, IsDeleted = columnModel24, DeleterUserId = columnModel25, DeletionTime = columnModel26, LastModificationTime = columnModel27, LastModifierUserId = columnModel28, CreationTime = c.DateTime(new bool?(false), nullable33, nullable34, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}