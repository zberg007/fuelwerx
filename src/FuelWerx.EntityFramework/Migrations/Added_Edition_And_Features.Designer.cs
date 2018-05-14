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
	public sealed class Added_Edition_And_Features : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Added_Edition_And_Features));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201510060756191_Added_Edition_And_Features";
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

		public Added_Edition_And_Features()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.AbpTenants", "EditionId", "dbo.AbpEditions", null);
			base.DropForeignKey("dbo.AbpFeatures", "EditionId", "dbo.AbpEditions", null);
			base.DropIndex("dbo.AbpTenants", new string[] { "EditionId" }, null);
			base.DropIndex("dbo.AbpFeatures", new string[] { "EditionId" }, null);
			base.DropColumn("dbo.AbpTenants", "EditionId", null);
			base.DropColumn("dbo.AbpAuditLogs", "CustomData", null);
			base.DropColumn("dbo.AbpAuditLogs", "ImpersonatorTenantId", null);
			base.DropColumn("dbo.AbpAuditLogs", "ImpersonatorUserId", null);
			base.DropTable("dbo.AbpEditions", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.AbpFeatures");
		}

		public override void Up()
		{
			base.CreateTable("dbo.AbpFeatures", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel1 = c.String(new bool?(false), new int?(128), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(2000), nullable3, nullable1, null, null, null, null, null);
				ColumnModel columnModel3 = c.DateTime(new bool?(false), null, null, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				int? nullable4 = null;
				ColumnModel columnModel5 = c.Int(nullable1, false, nullable4, null, null, null, null);
				nullable1 = null;
				nullable4 = null;
				ColumnModel columnModel6 = c.Int(nullable1, false, nullable4, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				return new { Id = columnModel, Name = columnModel1, Value = columnModel2, CreationTime = columnModel3, CreatorUserId = columnModel4, EditionId = columnModel5, TenantId = columnModel6, Discriminator = c.String(new bool?(false), new int?(128), nullable5, nullable1, null, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpEditions", (t) => t.EditionId, true, null, null).Index((t) => t.EditionId, null, false, false, null);
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.AbpEditions", (ColumnBuilder c) => {
				ColumnModel columnModel = c.Int(new bool?(false), true, null, null, null, null, null);
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				ColumnModel columnModel1 = c.String(new bool?(false), new int?(32), nullable1, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(64), nullable2, nullable, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				long? nullable3 = null;
				ColumnModel columnModel4 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel5 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel6 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable3 = null;
				ColumnModel columnModel7 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable = null;
				nullable3 = null;
				return new { Id = columnModel, Name = columnModel1, DisplayName = columnModel2, IsDeleted = columnModel3, DeleterUserId = columnModel4, DeletionTime = columnModel5, LastModificationTime = columnModel6, LastModifierUserId = columnModel7, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable, false, nullable3, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
			base.AddColumn("dbo.AbpAuditLogs", "ImpersonatorUserId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.AbpAuditLogs", "ImpersonatorTenantId", (ColumnBuilder c) => c.Int(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.AbpAuditLogs", "CustomData", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AddColumn("dbo.AbpTenants", "EditionId", (ColumnBuilder c) => c.Int(null, false, null, null, null, null, null), null);
			base.CreateIndex("dbo.AbpTenants", "EditionId", false, null, false, null);
			base.AddForeignKey("dbo.AbpTenants", "EditionId", "dbo.AbpEditions", "Id", false, null, null);
		}
	}
}