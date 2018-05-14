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
	public sealed class fuelwerxmigration_2163 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_2163));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201604302314007_fuelwerxmigration_2163";
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

		public fuelwerxmigration_2163()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxTenantPaymentSettings", "FlatProjectAmount", null);
			base.DropColumn("dbo.FuelWerxTenantPaymentSettings", "HourlyProjectRate", null);
			base.DropColumn("dbo.FuelWerxTenantPaymentSettings", "HourlyTaskRate", null);
			base.DropColumn("dbo.FuelWerxTenantPaymentSettings", "HourlyStaffRate", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxTenantPaymentSettings", "HourlyStaffRate", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxTenantPaymentSettings", "HourlyTaskRate", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxTenantPaymentSettings", "HourlyProjectRate", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxTenantPaymentSettings", "FlatProjectAmount", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
		}
	}
}