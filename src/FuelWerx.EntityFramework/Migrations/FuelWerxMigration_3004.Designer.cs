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
	public sealed class FuelWerxMigration_3004 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration_3004));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201608220029549_FuelWerxMigration_3004";
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

		public FuelWerxMigration_3004()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTenantPaymentGatewaySettings", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantPaymentGatewaySettings_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantPaymentGatewaySettings_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantPaymentGatewaySettings_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantPaymentGatewaySettings_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantPaymentGatewaySettings", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), nullable1, nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel4 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel5 = c.DateTime(nullable2, nullable4, nullable5, null, null, null, null);
				nullable2 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel6 = c.DateTime(nullable2, nullable4, nullable5, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, GatewaySettings = columnModel2, IsDeleted = columnModel3, DeleterUserId = columnModel4, DeletionTime = columnModel5, LastModificationTime = columnModel6, LastModifierUserId = columnModel7, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}