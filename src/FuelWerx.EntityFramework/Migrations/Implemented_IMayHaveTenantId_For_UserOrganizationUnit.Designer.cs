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
	public sealed class Implemented_IMayHaveTenantId_For_UserOrganizationUnit : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Implemented_IMayHaveTenantId_For_UserOrganizationUnit));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201512100815485_Implemented_IMayHaveTenantId_For_UserOrganizationUnit";
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

		public Implemented_IMayHaveTenantId_For_UserOrganizationUnit()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.AbpUserOrganizationUnits", "TenantId", null);
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_UserOrganizationUnit_MayHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.AbpUserOrganizationUnits", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, UserId = columnModel2, OrganizationUnitId = columnModel3, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null);
		}

		public override void Up()
		{
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_UserOrganizationUnit_MayHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.AbpUserOrganizationUnits", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, UserId = columnModel2, OrganizationUnitId = columnModel3, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null);
			base.AddColumn("dbo.AbpUserOrganizationUnits", "TenantId", (ColumnBuilder c) => c.Int(null, false, null, null, null, null, null), null);
		}
	}
}