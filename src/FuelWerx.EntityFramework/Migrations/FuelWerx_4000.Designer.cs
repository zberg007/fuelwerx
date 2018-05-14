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
	public sealed class FuelWerx_4000 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_4000));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201609030647166_FuelWerx_4000";
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

		public FuelWerx_4000()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTrucks", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Truck_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Truck_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Truck_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Truck_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTrucks", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				ColumnModel columnModel2 = c.Guid(nullable2, false, null, null, null, null, null);
				nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable4, nullable1, nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable6, new int?(16), nullable7, nullable2, null, null, null, null, null);
				decimal? nullable8 = null;
				ColumnModel columnModel6 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable8 = null;
				ColumnModel columnModel7 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable8, null, null, null, false, null);
				nullable2 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable9 = null;
				DateTime? nullable10 = null;
				ColumnModel columnModel11 = c.DateTime(nullable2, nullable9, nullable10, null, null, null, null);
				nullable2 = null;
				nullable9 = null;
				nullable10 = null;
				ColumnModel columnModel12 = c.DateTime(nullable2, nullable9, nullable10, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel13 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable9 = null;
				nullable10 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ImageId = columnModel2, Name = columnModel3, Description = columnModel4, Number = columnModel5, Capacity = columnModel6, RemainingCapacity = columnModel7, IsActive = columnModel8, IsDeleted = columnModel9, DeleterUserId = columnModel10, DeletionTime = columnModel11, LastModificationTime = columnModel12, LastModifierUserId = columnModel13, CreationTime = c.DateTime(new bool?(false), nullable9, nullable10, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}