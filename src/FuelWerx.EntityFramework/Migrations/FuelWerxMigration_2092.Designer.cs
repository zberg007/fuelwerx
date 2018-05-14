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
	public sealed class FuelWerxMigration_2092 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration_2092));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201607060301532_FuelWerxMigration_2092";
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

		public FuelWerxMigration_2092()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProjects", "CustomerAddressId", "dbo.FuelWerxAddresses", null);
			base.DropIndex("dbo.FuelWerxProjects", new string[] { "CustomerAddressId" }, null);
			base.DropColumn("dbo.FuelWerxProjects", "IvoiceAcceptedTime", null);
			base.DropColumn("dbo.FuelWerxProjects", "InvoiceId", null);
			base.DropColumn("dbo.FuelWerxProjects", "GroupDiscount", null);
			base.DropColumn("dbo.FuelWerxProjects", "Upcharge", null);
			base.DropColumn("dbo.FuelWerxProjects", "DueDate", null);
			base.DropColumn("dbo.FuelWerxProjects", "DueDateDiscountTotal", null);
			base.DropColumn("dbo.FuelWerxProjects", "DueDateDiscountExpirationDate", null);
			base.DropColumn("dbo.FuelWerxProjects", "CustomerAddressId", null);
			base.DropColumn("dbo.FuelWerxProjectAdhocProducts", "IsTaxable", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxProjectAdhocProducts", "IsTaxable", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "CustomerAddressId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "DueDateDiscountExpirationDate", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "DueDateDiscountTotal", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "DueDate", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "Upcharge", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "GroupDiscount", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "InvoiceId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxProjects", "IvoiceAcceptedTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.CreateIndex("dbo.FuelWerxProjects", "CustomerAddressId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxProjects", "CustomerAddressId", "dbo.FuelWerxAddresses", "Id", false, null, null);
		}
	}
}