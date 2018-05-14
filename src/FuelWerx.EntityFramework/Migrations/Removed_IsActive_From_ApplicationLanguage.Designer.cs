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
	public sealed class Removed_IsActive_From_ApplicationLanguage : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Removed_IsActive_From_ApplicationLanguage));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201511020629559_Removed_IsActive_From_ApplicationLanguage";
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

		public Removed_IsActive_From_ApplicationLanguage()
		{
		}

		public override void Down()
		{
			base.AddColumn("dbo.AbpLanguages", "IsActive", (ColumnBuilder c) => c.Boolean(new bool?(false), null, null, null, null, null), null);
		}

		public override void Up()
		{
			base.DropColumn("dbo.AbpLanguages", "IsActive", null);
		}
	}
}