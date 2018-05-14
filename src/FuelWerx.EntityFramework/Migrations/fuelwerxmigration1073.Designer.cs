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
	public sealed class fuelwerxmigration1073 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1073));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603250454243_fuelwerxmigration1073";
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

		public fuelwerxmigration1073()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxCustomers", "TitleId", "dbo.FuelWerxTitles", null);
			base.DropIndex("dbo.FuelWerxCustomers", new string[] { "TitleId" }, null);
			base.DropColumn("dbo.FuelWerxCustomers", "TitleId", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxCustomers", "TitleId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.CreateIndex("dbo.FuelWerxCustomers", "TitleId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxCustomers", "TitleId", "dbo.FuelWerxTitles", "Id", false, null, null);
		}
	}
}