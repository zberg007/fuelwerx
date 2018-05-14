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
	public sealed class _201607180532201_FuelWerxMigration_2098 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(_201607180532201_FuelWerxMigration_2098));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201607190513235_201607180532201_FuelWerxMigration_2098";
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

		public _201607180532201_FuelWerxMigration_2098()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxInvoices", "IncludeEmergencyDeliveryFee", null);
			base.DropColumn("dbo.FuelWerxInvoices", "EmergencyDeliveryFee", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxInvoices", "EmergencyDeliveryFee", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxInvoices", "IncludeEmergencyDeliveryFee", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.Boolean(nullable1, nullable, null, null, null, null);
			}, null);
		}
	}
}