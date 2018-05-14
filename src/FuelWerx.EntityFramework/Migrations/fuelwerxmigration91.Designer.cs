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
	public sealed class fuelwerxmigration91 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration91));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602290420562_fuelwerxmigration91";
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

		public fuelwerxmigration91()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxCustomerServices", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantCustomerService_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantCustomerService_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantCustomerService_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantCustomerService_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxCustomerServices", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.String(nullable3, new int?(255), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(nullable5, new int?(16), nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable7, new int?(16), nullable8, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable9, new int?(255), nullable10, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable11, nullable1, nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(nullable13, new int?(15), nullable14, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(nullable15, new int?(15), nullable16, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable2 = null;
				ColumnModel columnModel10 = c.String(nullable17, new int?(15), nullable18, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable19 = nullable2;
				nullable2 = null;
				bool? nullable20 = nullable2;
				nullable2 = null;
				ColumnModel columnModel11 = c.String(nullable19, new int?(15), nullable20, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel12 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable21 = nullable2;
				nullable2 = null;
				bool? nullable22 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.String(nullable21, new int?(15), nullable22, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable23 = nullable2;
				nullable2 = null;
				bool? nullable24 = nullable2;
				nullable2 = null;
				ColumnModel columnModel14 = c.String(nullable23, new int?(15), nullable24, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel15 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable25 = nullable2;
				nullable2 = null;
				bool? nullable26 = nullable2;
				nullable2 = null;
				ColumnModel columnModel16 = c.String(nullable25, new int?(15), nullable26, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable27 = nullable2;
				nullable2 = null;
				bool? nullable28 = nullable2;
				nullable2 = null;
				ColumnModel columnModel17 = c.String(nullable27, new int?(15), nullable28, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel18 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable29 = nullable2;
				nullable2 = null;
				bool? nullable30 = nullable2;
				nullable2 = null;
				ColumnModel columnModel19 = c.String(nullable29, new int?(15), nullable30, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable31 = nullable2;
				nullable2 = null;
				bool? nullable32 = nullable2;
				nullable2 = null;
				ColumnModel columnModel20 = c.String(nullable31, new int?(15), nullable32, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel21 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable33 = nullable2;
				nullable2 = null;
				bool? nullable34 = nullable2;
				nullable2 = null;
				ColumnModel columnModel22 = c.String(nullable33, new int?(15), nullable34, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable35 = nullable2;
				nullable2 = null;
				bool? nullable36 = nullable2;
				nullable2 = null;
				ColumnModel columnModel23 = c.String(nullable35, new int?(15), nullable36, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel24 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable37 = nullable2;
				nullable2 = null;
				bool? nullable38 = nullable2;
				nullable2 = null;
				ColumnModel columnModel25 = c.String(nullable37, new int?(15), nullable38, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable39 = nullable2;
				nullable2 = null;
				bool? nullable40 = nullable2;
				nullable2 = null;
				ColumnModel columnModel26 = c.String(nullable39, new int?(15), nullable40, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel27 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel28 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel29 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable41 = null;
				DateTime? nullable42 = null;
				ColumnModel columnModel30 = c.DateTime(nullable2, nullable41, nullable42, null, null, null, null);
				nullable2 = null;
				nullable41 = null;
				nullable42 = null;
				ColumnModel columnModel31 = c.DateTime(nullable2, nullable41, nullable42, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel32 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable41 = null;
				nullable42 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Label = columnModel2, PhoneNumber = columnModel3, PhoneNumberEmergency = columnModel4, Email = columnModel5, Note = columnModel6, MondayOpen = columnModel7, MondayClose = columnModel8, MondayLunchObserved = columnModel9, TuesdayOpen = columnModel10, TuesdayClose = columnModel11, TuesdayLunchObserved = columnModel12, WednesdayOpen = columnModel13, WednesdayClose = columnModel14, WednesdayLunchObserved = columnModel15, ThursdayOpen = columnModel16, ThursdayClose = columnModel17, ThursdayLunchObserved = columnModel18, FridayOpen = columnModel19, FridayClose = columnModel20, FridayLunchObserved = columnModel21, SaturdayOpen = columnModel22, SaturdayClose = columnModel23, SaturdayLunchObserved = columnModel24, SundayOpen = columnModel25, SundayClose = columnModel26, SundayLunchObserved = columnModel27, IsDeleted = columnModel28, DeleterUserId = columnModel29, DeletionTime = columnModel30, LastModificationTime = columnModel31, LastModifierUserId = columnModel32, CreationTime = c.DateTime(new bool?(false), nullable41, nullable42, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}