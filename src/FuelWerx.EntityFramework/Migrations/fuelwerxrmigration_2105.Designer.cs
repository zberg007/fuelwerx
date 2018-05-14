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
	public sealed class fuelwerxrmigration_2105 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxrmigration_2105));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201604181155572_fuelwerxrmigration_2105";
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

		public fuelwerxrmigration_2105()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxProductSpecificPrices", "IsActive", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxProductSpecificPrices", "IsActive", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
		}
	}
}