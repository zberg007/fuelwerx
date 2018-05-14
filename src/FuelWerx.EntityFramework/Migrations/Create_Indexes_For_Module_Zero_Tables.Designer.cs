using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Create_Indexes_For_Module_Zero_Tables : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Create_Indexes_For_Module_Zero_Tables));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201505011844467_Create_Indexes_For_Module_Zero_Tables";
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

		public Create_Indexes_For_Module_Zero_Tables()
		{
		}

		public override void Down()
		{
			base.DropIndex("AbpAuditLogs", new string[] { "TenantId", "ExecutionTime" }, null);
			base.DropIndex("AbpAuditLogs", new string[] { "UserId", "ExecutionTime" }, null);
			base.DropIndex("AbpPermissions", new string[] { "UserId", "Name" }, null);
			base.DropIndex("AbpPermissions", new string[] { "RoleId", "Name" }, null);
			base.DropIndex("AbpRoles", new string[] { "TenantId", "Name" }, null);
			base.DropIndex("AbpRoles", new string[] { "IsDeleted", "TenantId", "Name" }, null);
			base.DropIndex("AbpSettings", new string[] { "TenantId", "Name" }, null);
			base.DropIndex("AbpSettings", new string[] { "UserId", "Name" }, null);
			base.DropIndex("AbpTenants", new string[] { "TenancyName" }, null);
			base.DropIndex("AbpTenants", new string[] { "IsDeleted", "TenancyName" }, null);
			base.DropIndex("AbpUserLogins", new string[] { "UserId", "LoginProvider" }, null);
			base.DropIndex("AbpUserRoles", new string[] { "UserId", "RoleId" }, null);
			base.DropIndex("AbpUserRoles", new string[] { "RoleId" }, null);
			base.DropIndex("AbpUsers", new string[] { "TenantId", "UserName" }, null);
			base.DropIndex("AbpUsers", new string[] { "TenantId", "EmailAddress" }, null);
			base.DropIndex("AbpUsers", new string[] { "IsDeleted", "TenantId", "UserName" }, null);
			base.DropIndex("AbpUsers", new string[] { "IsDeleted", "TenantId", "EmailAddress" }, null);
			base.CreateIndex("AbpPermissions", new string[] { "UserId" }, false, null, false, null);
			base.CreateIndex("AbpPermissions", new string[] { "RoleId" }, false, null, false, null);
			base.CreateIndex("AbpRoles", new string[] { "TenantId" }, false, null, false, null);
			base.CreateIndex("AbpSettings", new string[] { "TenantId" }, false, null, false, null);
			base.CreateIndex("AbpSettings", new string[] { "UserId" }, false, null, false, null);
			base.CreateIndex("AbpUserLogins", new string[] { "UserId" }, false, null, false, null);
			base.CreateIndex("AbpUserRoles", new string[] { "UserId" }, false, null, false, null);
			base.CreateIndex("AbpUsers", new string[] { "TenantId" }, false, null, false, null);
		}

		public override void Up()
		{
			base.DropIndex("AbpPermissions", new string[] { "UserId" }, null);
			base.DropIndex("AbpPermissions", new string[] { "RoleId" }, null);
			base.DropIndex("AbpRoles", new string[] { "TenantId" }, null);
			base.DropIndex("AbpSettings", new string[] { "TenantId" }, null);
			base.DropIndex("AbpSettings", new string[] { "UserId" }, null);
			base.DropIndex("AbpUserLogins", new string[] { "UserId" }, null);
			base.DropIndex("AbpUserRoles", new string[] { "UserId" }, null);
			base.DropIndex("AbpUsers", new string[] { "TenantId" }, null);
			base.CreateIndex("AbpAuditLogs", new string[] { "TenantId", "ExecutionTime" }, false, null, false, null);
			base.CreateIndex("AbpAuditLogs", new string[] { "UserId", "ExecutionTime" }, false, null, false, null);
			base.CreateIndex("AbpPermissions", new string[] { "UserId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpPermissions", new string[] { "RoleId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpRoles", new string[] { "TenantId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpRoles", new string[] { "IsDeleted", "TenantId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpSettings", new string[] { "TenantId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpSettings", new string[] { "UserId", "Name" }, false, null, false, null);
			base.CreateIndex("AbpTenants", new string[] { "TenancyName" }, false, null, false, null);
			base.CreateIndex("AbpTenants", new string[] { "IsDeleted", "TenancyName" }, false, null, false, null);
			base.CreateIndex("AbpUserLogins", new string[] { "UserId", "LoginProvider" }, false, null, false, null);
			base.CreateIndex("AbpUserRoles", new string[] { "UserId", "RoleId" }, false, null, false, null);
			base.CreateIndex("AbpUserRoles", new string[] { "RoleId" }, false, null, false, null);
			base.CreateIndex("AbpUsers", new string[] { "TenantId", "UserName" }, false, null, false, null);
			base.CreateIndex("AbpUsers", new string[] { "TenantId", "EmailAddress" }, false, null, false, null);
			base.CreateIndex("AbpUsers", new string[] { "IsDeleted", "TenantId", "UserName" }, false, null, false, null);
			base.CreateIndex("AbpUsers", new string[] { "IsDeleted", "TenantId", "EmailAddress" }, false, null, false, null);
		}
	}
}