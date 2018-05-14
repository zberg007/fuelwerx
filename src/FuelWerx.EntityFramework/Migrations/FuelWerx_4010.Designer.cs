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
	public sealed class FuelWerx_4010 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_4010));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201609121645040_FuelWerx_4010";
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

		public FuelWerx_4010()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxServiceTanks", "BaseTemperature", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxServiceTanks", "BaseTemperature", (ColumnBuilder c) => c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null), null);
		}
	}
}