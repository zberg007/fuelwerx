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
	public sealed class fuelwerxmigration1060 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1060));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603190507410_fuelwerxmigration1060";
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

		public fuelwerxmigration1060()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxProjectTasks", "Retail", null);
			base.DropColumn("dbo.FuelWerxEstimateTasks", "Retail", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxEstimateTasks", "Retail", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxProjectTasks", "Retail", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
		}
	}
}