using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.2-31219")]
	public sealed class Added_IsStatic_To_Role : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Added_IsStatic_To_Role));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201501131916251_Added_IsStatic_To_Role";
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

		public Added_IsStatic_To_Role()
		{
		}

		public override void Down()
		{
			base.AlterColumn("dbo.AbpUsers", "EmailAddress", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(100), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "Password", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(100), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "Surname", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(30), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "Name", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(30), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpRoles", "DisplayName", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpRoles", "Name", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.DropColumn("dbo.AbpRoles", "IsStatic", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.AbpRoles", "IsStatic", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.AlterColumn("dbo.AbpRoles", "Name", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(32), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpRoles", "DisplayName", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(64), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "Name", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(32), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "Surname", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(32), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "Password", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(128), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "EmailAddress", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(256), nullable1, nullable, null, null, null, null, null);
			}, null);
		}
	}
}