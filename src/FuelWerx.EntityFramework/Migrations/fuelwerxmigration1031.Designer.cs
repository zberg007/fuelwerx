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
	public sealed class fuelwerxmigration1031 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration1031));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201603121957276_fuelwerxmigration1031";
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

		public fuelwerxmigration1031()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.FuelWerxTenantDateTimeSettings", "TimezoneJson", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(1200), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.DropColumn("dbo.FuelWerxTenantDateTimeSettings", "TimezoneId", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxTenantDateTimeSettings", "TimezoneId", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(70), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.DropColumn("dbo.FuelWerxTenantDateTimeSettings", "TimezoneJson", null);
		}
	}
}