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
	public sealed class fuelwerxmigration1001 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1001));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603051845512_fuelwerxmigration1001";
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

		public fuelwerxmigration1001()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxDriversData", new Dictionary<string, object>()
			{
				{ "DynamicFilter_DriversData_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_DriversData_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_DriversData_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_DriversData_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxDriversData", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(50), nullable2, nullable1, null, null, null, null, null);
				byte? nullable3 = null;
				DateTime? nullable4 = null;
				ColumnModel columnModel4 = c.DateTime(new bool?(false), nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(nullable5, nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel10 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, OwnerId = columnModel2, CDLNumber = columnModel3, CDLExpiration = columnModel4, HasHazmat = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable3, nullable4, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}