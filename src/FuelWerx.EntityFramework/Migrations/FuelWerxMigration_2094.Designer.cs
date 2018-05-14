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
	public sealed class FuelWerxMigration_2094 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration_2094));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201607070410089_FuelWerxMigration_2094";
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

		public FuelWerxMigration_2094()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.FuelWerxProjects", "IvoiceAcceptedTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.DropForeignKey("dbo.FuelWerxProjectProducts", "LineItemId", "dbo.FuelWerxProjectProductLineItems", null);
			base.DropIndex("dbo.FuelWerxProjectProducts", new string[] { "LineItemId" }, null);
			base.DropColumn("dbo.FuelWerxProjectProducts", "LineItemId", null);
			base.DropColumn("dbo.FuelWerxProjects", "InvoiceAcceptedTime", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxProjects", "InvoiceAcceptedTime", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxProjectProducts", "LineItemId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.CreateIndex("dbo.FuelWerxProjectProducts", "LineItemId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxProjectProducts", "LineItemId", "dbo.FuelWerxProjectProductLineItems", "Id", false, null, null);
			base.DropColumn("dbo.FuelWerxProjects", "IvoiceAcceptedTime", null);
		}
	}
}