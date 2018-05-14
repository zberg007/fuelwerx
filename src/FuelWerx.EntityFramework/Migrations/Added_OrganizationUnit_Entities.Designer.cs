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
	public sealed class Added_OrganizationUnit_Entities : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Added_OrganizationUnit_Entities));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201512100805111_Added_OrganizationUnit_Entities";
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

		public Added_OrganizationUnit_Entities()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.AbpOrganizationUnits", "ParentId", "dbo.AbpOrganizationUnits", null);
			base.DropIndex("dbo.AbpOrganizationUnits", new string[] { "ParentId" }, null);
			base.DropTable("dbo.AbpUserOrganizationUnits");
			base.DropTable("dbo.AbpOrganizationUnits", new Dictionary<string, object>()
			{
				{ "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.AbpOrganizationUnits", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel2 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(128), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(128), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable4, nullable5, null, null, null, null);
				nullable1 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable4, nullable5, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ParentId = columnModel2, Code = columnModel3, DisplayName = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpOrganizationUnits", (t) => t.ParentId, false, null, null).Index((t) => t.ParentId, null, false, false, null);
			base.CreateTable("dbo.AbpUserOrganizationUnits", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				byte? nullable1 = null;
				DateTime? nullable2 = null;
				bool? nullable3 = null;
				nullable = null;
				return new { Id = columnModel, UserId = columnModel1, OrganizationUnitId = columnModel2, CreationTime = c.DateTime(new bool?(false), nullable1, nullable2, null, null, null, null), CreatorUserId = c.Long(nullable3, false, nullable, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}