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
	public sealed class fuelwerxmigration_21011 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_21011));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201604240411034_fuelwerxmigration_21011";
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

		public fuelwerxmigration_21011()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProjectProducts", "ProjectId", "dbo.FuelWerxProjects", null);
			base.DropForeignKey("dbo.FuelWerxProjectProducts", "ProductId", "dbo.FuelWerxProducts", null);
			base.DropIndex("dbo.FuelWerxProjectProducts", new string[] { "ProductId" }, null);
			base.DropIndex("dbo.FuelWerxProjectProducts", new string[] { "ProjectId" }, null);
			base.DropTable("dbo.FuelWerxProjectProducts", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjectProducts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel5 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ProjectId = columnModel1, ProductId = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProducts", (t) => t.ProductId, true, null, null).ForeignKey("dbo.FuelWerxProjects", (t) => t.ProjectId, true, null, null).Index((t) => t.ProjectId, null, false, false, null).Index((t) => t.ProductId, null, false, false, null);
		}
	}
}