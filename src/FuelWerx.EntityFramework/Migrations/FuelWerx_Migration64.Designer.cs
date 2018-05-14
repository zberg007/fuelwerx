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
	public sealed class FuelWerx_Migration64 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration64));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602190311219_FuelWerx_Migration64";
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

		public FuelWerx_Migration64()
		{
		}

		public override void Down()
		{
			base.AlterColumn("dbo.FuelWerxEstimates", "Number", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(38), nullable2, nullable, null, null, null, null, null);
			}, null);
		}

		public override void Up()
		{
			base.AlterColumn("dbo.FuelWerxEstimates", "Number", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(38), nullable1, nullable, null, null, null, null, null);
			}, null);
		}
	}
}