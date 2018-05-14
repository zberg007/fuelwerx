using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Create_Indexes_For_OrganizationUnit_Tables : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Create_Indexes_For_OrganizationUnit_Tables));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201512211835309_Create_Indexes_For_OrganizationUnit_Tables";
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

		public Create_Indexes_For_OrganizationUnit_Tables()
		{
		}

		public override void Down()
		{
			base.DropIndex("AbpUserOrganizationUnits", new string[] { "OrganizationUnitId" }, null);
			base.DropIndex("AbpUserOrganizationUnits", new string[] { "UserId" }, null);
			base.DropIndex("AbpUserOrganizationUnits", new string[] { "TenantId", "OrganizationUnitId" }, null);
			base.DropIndex("AbpUserOrganizationUnits", new string[] { "TenantId", "UserId" }, null);
			base.DropIndex("AbpOrganizationUnits", new string[] { "TenantId", "Code" }, null);
			base.DropIndex("AbpOrganizationUnits", new string[] { "TenantId", "ParentId" }, null);
		}

		public override void Up()
		{
			base.CreateIndex("AbpOrganizationUnits", new string[] { "TenantId", "ParentId" }, false, null, false, null);
			base.CreateIndex("AbpOrganizationUnits", new string[] { "TenantId", "Code" }, false, null, false, null);
			base.CreateIndex("AbpUserOrganizationUnits", new string[] { "TenantId", "UserId" }, false, null, false, null);
			base.CreateIndex("AbpUserOrganizationUnits", new string[] { "TenantId", "OrganizationUnitId" }, false, null, false, null);
			base.CreateIndex("AbpUserOrganizationUnits", new string[] { "UserId" }, false, null, false, null);
			base.CreateIndex("AbpUserOrganizationUnits", new string[] { "OrganizationUnitId" }, false, null, false, null);
		}
	}
}