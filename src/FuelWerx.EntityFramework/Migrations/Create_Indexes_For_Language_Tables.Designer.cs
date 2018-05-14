using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Create_Indexes_For_Language_Tables : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Create_Indexes_For_Language_Tables));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201512211844078_Create_Indexes_For_Language_Tables";
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

		public Create_Indexes_For_Language_Tables()
		{
		}

		public override void Down()
		{
			base.DropIndex("AbpLanguageTexts", new string[] { "TenantId", "LanguageName", "Source", "Key" }, null);
			base.DropIndex("AbpLanguages", new string[] { "TenantId", "Name" }, null);
		}

		public override void Up()
		{
			base.CreateIndex("AbpLanguages", new string[] { "TenantId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpLanguageTexts", new string[] { "TenantId", "LanguageName", "Source", "Key" }, false, null, false, null);
		}
	}
}