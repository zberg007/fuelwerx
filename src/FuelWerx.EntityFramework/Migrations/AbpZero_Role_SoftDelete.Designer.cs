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
	public sealed class AbpZero_Role_SoftDelete : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(AbpZero_Role_SoftDelete));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201504051650417_AbpZero_Role_SoftDelete";
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

		public AbpZero_Role_SoftDelete()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers", null);
			base.DropIndex("dbo.AbpRoles", new string[] { "DeleterUserId" }, null);
			base.AlterColumn("dbo.AbpSettings", "Value", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpSettings", "Name", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.DropColumn("dbo.AbpRoles", "DeletionTime", null);
			base.DropColumn("dbo.AbpRoles", "DeleterUserId", null);
			base.DropColumn("dbo.AbpRoles", "IsDeleted", null);
			base.DropColumn("dbo.AbpRoles", "IsDefault", null);
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Role_SoftDelete", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.AbpRoles", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(32), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(64), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				long? nullable4 = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable4, null, null, null, null);
				nullable1 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable4 = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable4, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable1 = null;
				nullable4 = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, DisplayName = columnModel3, IsStatic = columnModel4, IsDefault = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable4, null, null, null, null) };
			}, strs, null);
		}

		public override void Up()
		{
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Role_SoftDelete", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.AbpRoles", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(32), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(64), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				long? nullable4 = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable4, null, null, null, null);
				nullable1 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable4 = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable4, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable1 = null;
				nullable4 = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, DisplayName = columnModel3, IsStatic = columnModel4, IsDefault = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable4, null, null, null, null) };
			}, strs, null);
			base.AddColumn("dbo.AbpRoles", "IsDefault", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.AddColumn("dbo.AbpRoles", "IsDeleted", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.AddColumn("dbo.AbpRoles", "DeleterUserId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.AbpRoles", "DeletionTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AlterColumn("dbo.AbpSettings", "Name", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(256), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpSettings", "Value", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(2000), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.CreateIndex("dbo.AbpRoles", "DeleterUserId", false, null, false, null);
			base.AddForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers", "Id", false, null, null);
		}
	}
}