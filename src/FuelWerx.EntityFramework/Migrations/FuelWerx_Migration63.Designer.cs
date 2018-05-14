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
	public sealed class FuelWerx_Migration63 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration63));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602181404127_FuelWerx_Migration63";
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

		public FuelWerx_Migration63()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.FuelWerxEstimates", "IsComplete", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.DropColumn("dbo.FuelWerxEstimateTasks", "IsComplete", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxEstimateTasks", "IsComplete", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.DropColumn("dbo.FuelWerxEstimates", "IsComplete", null);
		}
	}
}