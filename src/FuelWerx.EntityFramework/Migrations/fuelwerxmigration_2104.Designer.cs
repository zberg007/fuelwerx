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
	public sealed class fuelwerxmigration_2104 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_2104));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201604241923556_fuelwerxmigration_2104";
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

		public fuelwerxmigration_2104()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProjectAdhocProducts", "ProjectId", "dbo.FuelWerxProjects", null);
			base.DropIndex("dbo.FuelWerxProjectAdhocProducts", new string[] { "ProjectId" }, null);
			base.DropTable("dbo.FuelWerxProjectAdhocProducts", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectAdhocProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectAdhocProduct_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjectAdhocProducts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				decimal? nullable3 = null;
				ColumnModel columnModel3 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				nullable3 = null;
				ColumnModel columnModel4 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				nullable3 = null;
				ColumnModel columnModel5 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel6 = c.String(nullable4, new int?(1200), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel10 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel11 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel12 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ProjectId = columnModel1, Name = columnModel2, Cost = columnModel3, BaseCost = columnModel4, RetailCost = columnModel5, Description = columnModel6, IsActive = columnModel7, IsDeleted = columnModel8, DeleterUserId = columnModel9, DeletionTime = columnModel10, LastModificationTime = columnModel11, LastModifierUserId = columnModel12, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProjects", (t) => t.ProjectId, true, null, null).Index((t) => t.ProjectId, null, false, false, null);
		}
	}
}