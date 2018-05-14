using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class FuelWerx_Migration_10 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_10));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602040426542_FuelWerx_Migration_10";
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

		public FuelWerx_Migration_10()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxOrganizationUnitProperties", "OrganizationUnitId", "dbo.AbpOrganizationUnits", null);
			base.DropPrimaryKey("dbo.FuelWerxOrganizationUnitProperties", null);
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "Id", (ColumnBuilder c) => c.Long(new bool?(false), false, null, null, null, null, null), null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "CreatorUserId", null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "CreationTime", null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "LastModifierUserId", null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "LastModificationTime", null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "DeletionTime", null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "DeleterUserId", null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "IsDeleted", null);
			base.DropColumn("dbo.FuelWerxOrganizationUnitProperties", "TenantId", null);
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_OrganizationUnitProperties_MayHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") },
				{ "DynamicFilter_OrganizationUnitProperties_MustHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.FuelWerxOrganizationUnitProperties", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				decimal? nullable1 = null;
				ColumnModel columnModel3 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				bool? nullable2 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable3 = null;
				DateTime? nullable4 = null;
				ColumnModel columnModel8 = c.DateTime(nullable2, nullable3, nullable4, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel9 = c.DateTime(nullable2, nullable3, nullable4, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, OrganizationUnitId = columnModel2, Discount = columnModel3, Upcharge = columnModel4, ShowPrice = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable3, nullable4, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null);
			base.AddPrimaryKey("dbo.FuelWerxOrganizationUnitProperties", "Id", null, true, null);
			base.CreateIndex("dbo.FuelWerxOrganizationUnitProperties", "Id", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxOrganizationUnitProperties", "OrganizationUnitId", "dbo.AbpOrganizationUnits", "Id", false, null, null);
			base.AddForeignKey("dbo.FuelWerxOrganizationUnitProperties", "Id", "dbo.AbpOrganizationUnits", "Id", false, null, null);
		}

		public override void Up()
		{
			base.DropForeignKey("dbo.FuelWerxOrganizationUnitProperties", "Id", "dbo.AbpOrganizationUnits", null);
			base.DropForeignKey("dbo.FuelWerxOrganizationUnitProperties", "OrganizationUnitId", "dbo.AbpOrganizationUnits", null);
			base.DropIndex("dbo.FuelWerxOrganizationUnitProperties", new string[] { "Id" }, null);
			base.DropPrimaryKey("dbo.FuelWerxOrganizationUnitProperties", null);
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_OrganizationUnitProperties_MayHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) },
				{ "DynamicFilter_OrganizationUnitProperties_MustHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.FuelWerxOrganizationUnitProperties", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				decimal? nullable1 = null;
				ColumnModel columnModel3 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable1, null, null, null, false, null);
				bool? nullable2 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable3 = null;
				DateTime? nullable4 = null;
				ColumnModel columnModel8 = c.DateTime(nullable2, nullable3, nullable4, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel9 = c.DateTime(nullable2, nullable3, nullable4, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, OrganizationUnitId = columnModel2, Discount = columnModel3, Upcharge = columnModel4, ShowPrice = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable3, nullable4, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "TenantId", (ColumnBuilder c) => c.Int(new bool?(false), false, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "IsDeleted", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "DeleterUserId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "DeletionTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "LastModificationTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "LastModifierUserId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "CreationTime", (ColumnBuilder c) => c.DateTime(new bool?(false), null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxOrganizationUnitProperties", "CreatorUserId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "Id", (ColumnBuilder c) => c.Long(new bool?(false), true, null, null, null, null, null), null);
			base.AddPrimaryKey("dbo.FuelWerxOrganizationUnitProperties", "Id", null, true, null);
			base.AddForeignKey("dbo.FuelWerxOrganizationUnitProperties", "OrganizationUnitId", "dbo.AbpOrganizationUnits", "Id", true, null, null);
		}
	}
}