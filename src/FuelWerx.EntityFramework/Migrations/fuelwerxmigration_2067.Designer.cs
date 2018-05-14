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
	public sealed class fuelwerxmigration_2067 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_2067));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201605080532034_fuelwerxmigration_2067";
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

		public fuelwerxmigration_2067()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxEstimates", "DueDateDiscountTotal", null);
			base.DropColumn("dbo.FuelWerxEstimates", "DueDateDiscountExpirationDate", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxEstimates", "DueDateDiscountExpirationDate", (ColumnBuilder c) => c.DateTime(null, null, null, null, null, null, null), null);
			base.AddColumn("dbo.FuelWerxEstimates", "DueDateDiscountTotal", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
		}
	}
}