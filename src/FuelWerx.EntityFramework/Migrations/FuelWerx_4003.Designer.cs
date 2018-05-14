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
	public sealed class FuelWerx_4003 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_4003));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201609041913055_FuelWerx_4003";
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

		public FuelWerx_4003()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxFillLots", "AddressId", "dbo.FuelWerxAddresses", null);
			base.DropIndex("dbo.FuelWerxFillLots", new string[] { "AddressId" }, null);
			base.AlterColumn("dbo.FuelWerxFillLots", "AddressId", (ColumnBuilder c) => c.Long(new bool?(false), false, null, null, null, null, null), null);
			base.CreateIndex("dbo.FuelWerxFillLots", "AddressId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxFillLots", "AddressId", "dbo.FuelWerxAddresses", "Id", true, null, null);
		}

		public override void Up()
		{
			base.DropForeignKey("dbo.FuelWerxFillLots", "AddressId", "dbo.FuelWerxAddresses", null);
			base.DropIndex("dbo.FuelWerxFillLots", new string[] { "AddressId" }, null);
			base.AlterColumn("dbo.FuelWerxFillLots", "AddressId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.CreateIndex("dbo.FuelWerxFillLots", "AddressId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxFillLots", "AddressId", "dbo.FuelWerxAddresses", "Id", false, null, null);
		}
	}
}