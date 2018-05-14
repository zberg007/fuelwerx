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
	[GeneratedCode("EntityFramework.Migrations", "6.1.2-31219")]
	public sealed class Tenant_Changed_To_FullAuditedEntity_From_AuditedEntity : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Tenant_Changed_To_FullAuditedEntity_From_AuditedEntity));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201503122019249_Tenant_Changed_To_FullAuditedEntity_From_AuditedEntity";
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

		public Tenant_Changed_To_FullAuditedEntity_From_AuditedEntity()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers", null);
			base.DropIndex("dbo.AbpTenants", new string[] { "DeleterUserId" }, null);
			base.DropColumn("dbo.AbpTenants", "DeletionTime", null);
			base.DropColumn("dbo.AbpTenants", "DeleterUserId", null);
			base.DropColumn("dbo.AbpTenants", "IsDeleted", null);
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "Abp_SoftDelete", new AnnotationValues("True", null) }
			};
			base.AlterTableAnnotations("dbo.AbpTenants", (ColumnBuilder c) => {
				ColumnModel columnModel = c.Int(new bool?(false), true, null, null, null, null, null);
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				ColumnModel columnModel1 = c.String(new bool?(false), new int?(64), nullable1, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(128), nullable2, nullable, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				long? nullable3 = null;
				ColumnModel columnModel5 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel6 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel7 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable3 = null;
				ColumnModel columnModel8 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable = null;
				nullable3 = null;
				return new { Id = columnModel, TenancyName = columnModel1, Name = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable, false, nullable3, null, null, null, null) };
			}, strs, null);
		}

		public override void Up()
		{
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "Abp_SoftDelete", new AnnotationValues(null, "True") }
			};
			base.AlterTableAnnotations("dbo.AbpTenants", (ColumnBuilder c) => {
				ColumnModel columnModel = c.Int(new bool?(false), true, null, null, null, null, null);
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				ColumnModel columnModel1 = c.String(new bool?(false), new int?(64), nullable1, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(128), nullable2, nullable, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				long? nullable3 = null;
				ColumnModel columnModel5 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel6 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel7 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable3 = null;
				ColumnModel columnModel8 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable = null;
				nullable3 = null;
				return new { Id = columnModel, TenancyName = columnModel1, Name = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable, false, nullable3, null, null, null, null) };
			}, strs, null);
			base.AddColumn("dbo.AbpTenants", "IsDeleted", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.AddColumn("dbo.AbpTenants", "DeleterUserId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.AbpTenants", "DeletionTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.CreateIndex("dbo.AbpTenants", "DeleterUserId", false, null, false, null);
			base.AddForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers", "Id", false, null, null);
		}
	}
}