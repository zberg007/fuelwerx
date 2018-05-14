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
	public sealed class ModuleZero_Upgrade_For_ISuspendable : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(ModuleZero_Upgrade_For_ISuspendable));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201503011513570_ModuleZero_Upgrade_For_ISuspendable";
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

		public ModuleZero_Upgrade_For_ISuspendable()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.AbpTenants", "IsSuspended", null);
			base.DropColumn("dbo.AbpUsers", "IsSuspended", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.AbpUsers", "IsSuspended", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.AddColumn("dbo.AbpTenants", "IsSuspended", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
		}
	}
}