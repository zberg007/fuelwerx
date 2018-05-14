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
	public sealed class fuelwerxmigration1064 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1064));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603231511413_fuelwerxmigration1064";
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

		public fuelwerxmigration1064()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxAddresses", "Latitude", null);
			base.DropColumn("dbo.FuelWerxAddresses", "Longitude", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxAddresses", "Longitude", (ColumnBuilder c) => c.Double(new bool?(true), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxAddresses", "Latitude", (ColumnBuilder c) => c.Double(new bool?(true), null, null, null, null, null), null);
		}
	}
}