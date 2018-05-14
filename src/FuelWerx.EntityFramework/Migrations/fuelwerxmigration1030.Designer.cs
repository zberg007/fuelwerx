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
	public sealed class fuelwerxmigration1030 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1030));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603121952370_fuelwerxmigration1030";
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

		public fuelwerxmigration1030()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTenantDateTimeSettings", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantDateTimeSettings_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantDateTimeSettings_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantDateTimeSettings_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantDateTimeSettings_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantDateTimeSettings", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(1200), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable3 = null;
				DateTime? nullable4 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, TimezoneJson = columnModel2, IsDeleted = columnModel3, DeleterUserId = columnModel4, DeletionTime = columnModel5, LastModificationTime = columnModel6, LastModifierUserId = columnModel7, CreationTime = c.DateTime(new bool?(false), nullable3, nullable4, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}