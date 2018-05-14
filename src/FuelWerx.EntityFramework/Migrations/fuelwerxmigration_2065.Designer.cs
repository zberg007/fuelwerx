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
	public sealed class fuelwerxmigration_2065 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration_2065));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201605051925355_fuelwerxmigration_2065";
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

		public fuelwerxmigration_2065()
		{
		}

		public override void Down()
		{
			base.DropColumn("dbo.FuelWerxProjects", "TermType", null);
			base.DropColumn("dbo.FuelWerxEstimates", "TermType", null);
		}

		public override void Up()
		{
			base.AddColumn("dbo.FuelWerxEstimates", "TermType", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(255), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AddColumn("dbo.FuelWerxProjects", "TermType", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(255), nullable2, nullable, null, null, null, null, null);
			}, null);
		}
	}
}