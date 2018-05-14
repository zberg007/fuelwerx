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
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Added_ShouldChangePasswordOnNextLogin_To_AbpUser : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Added_ShouldChangePasswordOnNextLogin_To_AbpUser));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201503220907591_Added_ShouldChangePasswordOnNextLogin_To_AbpUser";
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

		public Added_ShouldChangePasswordOnNextLogin_To_AbpUser()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.AbpUsers", "ShouldChangePasswordOnNextLogin", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.AbpUsers", "ShouldChangePasswordOnNextLogin", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
		}
	}
}