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
	public sealed class FuelWerxMigration72 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration72));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602230432416_FuelWerxMigration72";
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

		public FuelWerxMigration72()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProjects", "EstimateId", "dbo.FuelWerxEstimates", null);
			base.DropIndex("dbo.FuelWerxProjects", new string[] { "EstimateId" }, null);
			base.DropColumn("dbo.FuelWerxProjects", "EstimateId", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxProjects", "EstimateId", (ColumnBuilder c) => c.Long(null, false, null, null, null, null, null), null);
			base.CreateIndex("dbo.FuelWerxProjects", "EstimateId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxProjects", "EstimateId", "dbo.FuelWerxEstimates", "Id", false, null, null);
		}
	}
}