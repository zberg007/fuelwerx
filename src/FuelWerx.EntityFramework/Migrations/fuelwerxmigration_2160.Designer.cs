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
	public sealed class fuelwerxmigration_2160 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_2160));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201604280052188_fuelwerxmigration_2160";
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

		public fuelwerxmigration_2160()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxTenantPaymentSettings", "DefaultPaymentTerm", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxTenantPaymentSettings", "DefaultPaymentTerm", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
		}
	}
}