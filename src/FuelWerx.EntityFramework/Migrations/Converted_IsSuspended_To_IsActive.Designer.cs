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
	public sealed class Converted_IsSuspended_To_IsActive : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Converted_IsSuspended_To_IsActive));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201503070820583_Converted_IsSuspended_To_IsActive";
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

		public Converted_IsSuspended_To_IsActive()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.AbpTenants", "IsSuspended", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.AddColumn("dbo.AbpUsers", "IsSuspended", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.DropColumn("dbo.AbpTenants", "IsActive", null);
			base.DropColumn("dbo.AbpUsers", "IsActive", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.AbpUsers", "IsActive", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(true), null, null, null, null), null);
			base.AddColumn("dbo.AbpTenants", "IsActive", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(true), null, null, null, null), null);
			base.DropColumn("dbo.AbpUsers", "IsSuspended", null);
			base.DropColumn("dbo.AbpTenants", "IsSuspended", null);
		}
	}
}