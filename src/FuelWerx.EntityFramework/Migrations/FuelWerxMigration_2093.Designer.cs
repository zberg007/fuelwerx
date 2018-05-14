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
	public sealed class FuelWerxMigration_2093 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration_2093));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201607070236437_FuelWerxMigration_2093";
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

		public FuelWerxMigration_2093()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProjectProductLineItemOptions", "ProductLineItemId", "dbo.FuelWerxProjectProductLineItems", null);
			base.DropForeignKey("dbo.FuelWerxProjectProductLineItems", "ProjectId", "dbo.FuelWerxProjects", null);
			base.DropIndex("dbo.FuelWerxProjectProductLineItems", new string[] { "ProjectId" }, null);
			base.DropIndex("dbo.FuelWerxProjectProductLineItemOptions", new string[] { "ProductLineItemId" }, null);
			base.DropTable("dbo.FuelWerxProjectProductLineItems", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectProductLineItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProjectProductLineItemOptions", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectProductLineItemOption_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectProductLineItemOption_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjectProductLineItemOptions", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ProductLineItemId = columnModel1, ProductOptionId = columnModel2, IsDeleted = columnModel3, DeleterUserId = columnModel4, DeletionTime = columnModel5, LastModificationTime = columnModel6, LastModifierUserId = columnModel7, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProjectProductLineItems", (t) => t.ProductLineItemId, true, null, null).Index((t) => t.ProductLineItemId, null, false, false, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectProductLineItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjectProductLineItems", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				ColumnModel columnModel3 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null);
				ColumnModel columnModel4 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ProjectId = columnModel1, ProductId = columnModel2, Cost = columnModel3, Quantity = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProjects", (t) => t.ProjectId, true, null, null).Index((t) => t.ProjectId, null, false, false, null);
		}
	}
}