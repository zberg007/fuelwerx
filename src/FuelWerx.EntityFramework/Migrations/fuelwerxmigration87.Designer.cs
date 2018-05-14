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
	public sealed class fuelwerxmigration87 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration87));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602290327377_fuelwerxmigration87";
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

		public fuelwerxmigration87()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.FuelWerxTenantHours", "SundayOutForLunch", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "SaturdayOutForLunch", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "FridayOutForLunch", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "ThursdayOutForLunch", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "WednesdayOutForLunch", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "TuesdayOutForLunch", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "MondayOutForLunch", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.DropColumn("dbo.FuelWerxTenantHours", "SundayLunchObserved", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "SaturdayLunchObserved", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "FridayLunchObserved", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "ThursdayLunchObserved", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "WednesdayLunchObserved", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "TuesdayLunchObserved", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "MondayLunchObserved", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxTenantHours", "MondayLunchObserved", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "TuesdayLunchObserved", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "WednesdayLunchObserved", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "ThursdayLunchObserved", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "FridayLunchObserved", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "SaturdayLunchObserved", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxTenantHours", "SundayLunchObserved", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.DropColumn("dbo.FuelWerxTenantHours", "MondayOutForLunch", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "TuesdayOutForLunch", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "WednesdayOutForLunch", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "ThursdayOutForLunch", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "FridayOutForLunch", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "SaturdayOutForLunch", null);
			base.DropColumn("dbo.FuelWerxTenantHours", "SundayOutForLunch", null);
		}
	}
}