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
	public sealed class fuelwerxmigration1065 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1065));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603231537536_fuelwerxmigration1065";
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

		public fuelwerxmigration1065()
		{
		}

		public override void Down()
		{
			base.AlterColumn("dbo.FuelWerxAddresses", "Latitude", (ColumnBuilder c) => c.Double(new bool?(false), null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxAddresses", "Longitude", (ColumnBuilder c) => c.Double(new bool?(false), null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxAddresses", "Location", (ColumnBuilder c) => c.Geography(new bool?(false), null, null, null, null, null), null);
		}

		public override void Up()
		{
			base.AlterColumn("dbo.FuelWerxAddresses", "Location", (ColumnBuilder c) => c.Geography(null, null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxAddresses", "Longitude", (ColumnBuilder c) => c.Double(null, null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxAddresses", "Latitude", (ColumnBuilder c) => c.Double(null, null, null, null, null, null), null);
		}
	}
}