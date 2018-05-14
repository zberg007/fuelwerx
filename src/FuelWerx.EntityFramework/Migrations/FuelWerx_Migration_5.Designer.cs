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
	public sealed class FuelWerx_Migration_5 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_5));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602040205334_FuelWerx_Migration_5";
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

		public FuelWerx_Migration_5()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxOrganizationUnitProperties", "OrganizationUnitId", "dbo.AbpOrganizationUnits", null);
			base.DropForeignKey("dbo.FuelWerxOrganizationUnitProperties", "Id", "dbo.AbpOrganizationUnits", null);
			base.DropIndex("dbo.FuelWerxOrganizationUnitProperties", new string[] { "OrganizationUnitId" }, null);
			base.DropIndex("dbo.FuelWerxOrganizationUnitProperties", new string[] { "Id" }, null);
			base.DropTable("dbo.FuelWerxOrganizationUnitProperties", new Dictionary<string, object>()
			{
				{ "DynamicFilter_OrganizationUnitProperties_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_OrganizationUnitProperties_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_OrganizationUnitProperties_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_OrganizationUnitProperties_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxOrganizationUnitProperties", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				decimal? nullable1 = null;
				ColumnModel columnModel2 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				nullable1 = null;
				return new { Id = columnModel, OrganizationUnitId = columnModel1, Discount = columnModel2, Upcharge = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null), ShowPrice = c.Boolean(new bool?(false), null, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpOrganizationUnits", (t) => t.Id, false, null, null).ForeignKey("dbo.AbpOrganizationUnits", (t) => t.OrganizationUnitId, false, null, null).Index((t) => t.Id, null, false, false, null).Index((t) => t.OrganizationUnitId, null, false, false, null);
		}
	}
}