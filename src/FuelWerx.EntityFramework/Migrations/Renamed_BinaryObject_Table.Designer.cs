using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Renamed_BinaryObject_Table : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Renamed_BinaryObject_Table));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201505162037045_Renamed_BinaryObject_Table";
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

		public Renamed_BinaryObject_Table()
		{
		}

		public override void Down()
		{
			base.RenameTable("dbo.AppBinaryObjects", "AbpBinaryObjects", null);
		}

		public override void Up()
		{
			base.RenameTable("dbo.AbpBinaryObjects", "AppBinaryObjects", null);
		}
	}
}