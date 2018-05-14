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
	public sealed class Renamed_ApplicationLanguageTexts_Table : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Renamed_ApplicationLanguageTexts_Table));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201511031848424_Renamed_ApplicationLanguageTexts_Table";
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

		public Renamed_ApplicationLanguageTexts_Table()
		{
		}

		public override void Down()
		{
			base.AlterColumn("dbo.AbpLanguageTexts", "Key", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(128), nullable1, nullable, null, null, null, null, null);
			}, null);
			base.RenameTable("dbo.AbpLanguageTexts", "ApplicationLanguageTexts", null);
		}

		public override void Up()
		{
			base.RenameTable("dbo.ApplicationLanguageTexts", "AbpLanguageTexts", null);
			base.AlterColumn("dbo.AbpLanguageTexts", "Key", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				return c.String(new bool?(false), new int?(256), nullable1, nullable, null, null, null, null, null);
			}, null);
		}
	}
}