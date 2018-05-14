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
	public sealed class fuelwerxmigration_2090 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_2090));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201606031522155_fuelwerxmigration_2090";
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

		public fuelwerxmigration_2090()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxEstimates", "CustomerAddressId", "dbo.FuelWerxAddresses", null);
			base.DropIndex("dbo.FuelWerxEstimates", new string[] { "CustomerAddressId" }, null);
			base.DropColumn("dbo.FuelWerxEstimates", "CustomerAddressId", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxEstimates", "CustomerAddressId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.CreateIndex("dbo.FuelWerxEstimates", "CustomerAddressId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxEstimates", "CustomerAddressId", "dbo.FuelWerxAddresses", "Id", false, null, null);
		}
	}
}