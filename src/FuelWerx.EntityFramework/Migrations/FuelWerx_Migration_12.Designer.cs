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
	public sealed class FuelWerx_Migration_12 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_12));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602050402566_FuelWerx_Migration_12";
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

		public FuelWerx_Migration_12()
		{
		}

		public override void Down()
		{
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "ShowPrice", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "Upcharge", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "Discount", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
		}

		public override void Up()
		{
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "Discount", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "Upcharge", (ColumnBuilder c) => c.Decimal(null, new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
			base.AlterColumn("dbo.FuelWerxOrganizationUnitProperties", "ShowPrice", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.Boolean(nullable1, nullable, null, null, null, null);
			}, null);
		}
	}
}