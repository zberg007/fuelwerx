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
	public sealed class FuelWerx_4005 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_4005));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201609111811117_FuelWerx_4005";
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

		public FuelWerx_4005()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxCustomers", "PaymentType", null);
			base.DropColumn("dbo.FuelWerxCustomers", "DeliveryType", null);
			base.DropColumn("dbo.FuelWerxCustomers", "DoNotDeliver", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxCustomers", "DoNotDeliver", (ColumnBuilder c) => c.Boolean(new bool?(false), new bool?(false), null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxCustomers", "DeliveryType", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(40), nullable1, nullable, "Automatic", null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxCustomers", "PaymentType", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(40), nullable1, nullable, "Prepay", null, null, null, null);
			}, null);
		}
	}
}