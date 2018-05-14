using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Create_Indexes_For_Edition_Tables : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Create_Indexes_For_Edition_Tables));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201512211848081_Create_Indexes_For_Edition_Tables";
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

		public Create_Indexes_For_Edition_Tables()
		{
		}

		public override void Down()
		{
			base.DropIndex("AbpFeatures", new string[] { "TenantId", "Name" }, null);
			base.DropIndex("AbpFeatures", new string[] { "Discriminator", "EditionId", "Name" }, null);
			base.DropIndex("AbpFeatures", new string[] { "Discriminator", "TenantId", "Name" }, null);
			base.DropIndex("AbpEditions", new string[] { "Name" }, null);
		}

		public override void Up()
		{
			base.CreateIndex("AbpEditions", new string[] { "Name" }, false, null, false, null);
			base.CreateIndex("AbpFeatures", new string[] { "Discriminator", "TenantId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpFeatures", new string[] { "Discriminator", "EditionId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpFeatures", new string[] { "TenantId", "Name" }, false, null, false, null);
		}
	}
}