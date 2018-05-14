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
	public sealed class FuelWerxMigration_2099 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration_2099));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201607190754584_FuelWerxMigration_2099";
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

		public FuelWerxMigration_2099()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxTenantPaymentSettings", "InvoiceNumber_StartingNumber", null);
			base.DropColumn("dbo.FuelWerxTenantPaymentSettings", "InvoiceNumber_Prefix", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxTenantPaymentSettings", "InvoiceNumber_Prefix", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(10), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxTenantPaymentSettings", "InvoiceNumber_StartingNumber", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
		}
	}
}