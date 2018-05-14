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
	public sealed class FuelWerx_Migration_36 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_36));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602070343203_FuelWerx_Migration_36";
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

		public FuelWerx_Migration_36()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxProducts", "Description", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxProducts", "Description", (ColumnBuilder c) => {
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