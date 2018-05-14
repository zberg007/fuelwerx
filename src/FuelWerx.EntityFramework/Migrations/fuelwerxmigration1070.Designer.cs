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
	public sealed class fuelwerxmigration1070 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1070));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603240503521_fuelwerxmigration1070";
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

		public fuelwerxmigration1070()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxTenantDetails", "Latitude", null);
			base.DropColumn("dbo.FuelWerxTenantDetails", "Longitude", null);
			base.DropColumn("dbo.FuelWerxTenantDetails", "Location", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxTenantDetails", "Location", (ColumnBuilder c) => c.Geography(new bool?(true), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantDetails", "Longitude", (ColumnBuilder c) => c.Double(new bool?(true), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantDetails", "Latitude", (ColumnBuilder c) => c.Double(new bool?(true), null, null, null, null, null), null);
		}
	}
}