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
	public sealed class FuelWerx_4008 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_4008));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201609121038035_FuelWerx_4008";
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

		public FuelWerx_4008()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTenantFuelCastSettings", new Dictionary<string, object>()
			{
				{ "DynamicFilter_FuelCastSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_FuelCastSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_FuelCastSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_FuelCastSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantFuelCastSettings", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable, null, null, null, null);
				decimal? nullable1 = null;
				ColumnModel columnModel2 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				nullable = null;
				ColumnModel columnModel3 = c.Int(new bool?(false), false, nullable, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				nullable = null;
				ColumnModel columnModel5 = c.Int(new bool?(false), false, nullable, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				nullable = null;
				ColumnModel columnModel8 = c.Int(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable = null;
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.String(nullable3, nullable, nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel10 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel11 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel12 = c.DateTime(nullable2, nullable5, nullable6, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel13 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				long? nullable7 = null;
				ColumnModel columnModel14 = c.Long(nullable2, false, nullable7, null, null, null, null);
				nullable2 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel15 = c.DateTime(nullable2, nullable5, nullable6, null, null, null, null);
				nullable2 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel16 = c.DateTime(nullable2, nullable5, nullable6, null, null, null, null);
				nullable2 = null;
				nullable7 = null;
				ColumnModel columnModel17 = c.Long(nullable2, false, nullable7, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable2 = null;
				nullable7 = null;
				return new { Id = columnModel, TenantId = columnModel1, BaseTemperature = columnModel2, KFactorComparison_LastN = columnModel3, KFactorComparison_Range = columnModel4, KFactorAnomoly_LastN = columnModel5, KFactorAnomoly_Range = columnModel6, OptimalDeliveryRange = columnModel7, KFactorAnomoly_TrendingDown = columnModel8, Notes = columnModel9, IsActive = columnModel10, AllowAnomolyModification = columnModel11, AllowAnomolyModificationDateTime = columnModel12, IsDeleted = columnModel13, DeleterUserId = columnModel14, DeletionTime = columnModel15, LastModificationTime = columnModel16, LastModifierUserId = columnModel17, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable7, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}