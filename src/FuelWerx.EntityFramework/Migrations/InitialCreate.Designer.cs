using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.2-31219")]
	public sealed class InitialCreate : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(InitialCreate));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201501041229232_InitialCreate";
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

		public InitialCreate()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.AbpRoles", "TenantId", "dbo.AbpTenants", null);
			base.DropForeignKey("dbo.AbpPermissions", "RoleId", "dbo.AbpRoles", null);
			base.DropForeignKey("dbo.AbpRoles", "LastModifierUserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpRoles", "CreatorUserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpUsers", "TenantId", "dbo.AbpTenants", null);
			base.DropForeignKey("dbo.AbpSettings", "TenantId", "dbo.AbpTenants", null);
			base.DropForeignKey("dbo.AbpTenants", "LastModifierUserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpTenants", "CreatorUserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpSettings", "UserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpUserRoles", "UserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpPermissions", "UserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpUserLogins", "UserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpUsers", "LastModifierUserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpUsers", "DeleterUserId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.AbpUsers", "CreatorUserId", "dbo.AbpUsers", null);
			base.DropIndex("dbo.AbpTenants", new string[] { "CreatorUserId" }, null);
			base.DropIndex("dbo.AbpTenants", new string[] { "LastModifierUserId" }, null);
			base.DropIndex("dbo.AbpSettings", new string[] { "UserId" }, null);
			base.DropIndex("dbo.AbpSettings", new string[] { "TenantId" }, null);
			base.DropIndex("dbo.AbpUserRoles", new string[] { "UserId" }, null);
			base.DropIndex("dbo.AbpUserLogins", new string[] { "UserId" }, null);
			base.DropIndex("dbo.AbpUsers", new string[] { "CreatorUserId" }, null);
			base.DropIndex("dbo.AbpUsers", new string[] { "LastModifierUserId" }, null);
			base.DropIndex("dbo.AbpUsers", new string[] { "DeleterUserId" }, null);
			base.DropIndex("dbo.AbpUsers", new string[] { "TenantId" }, null);
			base.DropIndex("dbo.AbpRoles", new string[] { "CreatorUserId" }, null);
			base.DropIndex("dbo.AbpRoles", new string[] { "LastModifierUserId" }, null);
			base.DropIndex("dbo.AbpRoles", new string[] { "TenantId" }, null);
			base.DropIndex("dbo.AbpPermissions", new string[] { "UserId" }, null);
			base.DropIndex("dbo.AbpPermissions", new string[] { "RoleId" }, null);
			base.DropTable("dbo.AbpTenants");
			base.DropTable("dbo.AbpSettings");
			base.DropTable("dbo.AbpUserRoles");
			base.DropTable("dbo.AbpUserLogins");
			base.DropTable("dbo.AbpUsers", new Dictionary<string, object>()
			{
				{ "Abp_SoftDelete", "True" }
			}, null);
			base.DropTable("dbo.AbpRoles");
			base.DropTable("dbo.AbpPermissions");
		}

		public override void Up()
		{
			base.CreateTable("dbo.AbpPermissions", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel1 = c.String(new bool?(false), new int?(128), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel2 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				ColumnModel columnModel3 = c.DateTime(new bool?(false), null, null, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Int(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				return new { Id = columnModel, Name = columnModel1, IsGranted = columnModel2, CreationTime = columnModel3, CreatorUserId = columnModel4, RoleId = columnModel5, UserId = columnModel6, Discriminator = c.String(new bool?(false), new int?(128), nullable3, nullable1, null, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpUsers", (t) => t.UserId, true, null, null).ForeignKey("dbo.AbpRoles", (t) => t.RoleId, true, null, null).Index((t) => t.RoleId, null, false, false, null).Index((t) => t.UserId, null, false, false, null);
			base.CreateTable("dbo.AbpRoles", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(nullable2, nullable, nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable4, nullable, nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel4 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				long? nullable8 = null;
				ColumnModel columnModel5 = c.Long(nullable1, false, nullable8, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable1 = null;
				nullable8 = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, DisplayName = columnModel3, LastModificationTime = columnModel4, LastModifierUserId = columnModel5, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable8, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpUsers", (t) => t.CreatorUserId, false, null, null).ForeignKey("dbo.AbpUsers", (t) => t.LastModifierUserId, false, null, null).ForeignKey("dbo.AbpTenants", (t) => t.TenantId, false, null, null).Index((t) => t.TenantId, null, false, false, null).Index((t) => t.LastModifierUserId, null, false, false, null).Index((t) => t.CreatorUserId, null, false, false, null);
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "Abp_SoftDelete", "True" }
			};
			base.CreateTable("dbo.AbpUsers", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(30), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(30), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(32), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(100), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel6 = c.String(new bool?(false), new int?(100), nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				bool? nullable7 = nullable1;
				nullable1 = null;
				bool? nullable8 = nullable1;
				nullable1 = null;
				ColumnModel columnModel8 = c.String(nullable7, new int?(16), nullable8, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable9 = nullable1;
				nullable1 = null;
				bool? nullable10 = nullable1;
				nullable1 = null;
				ColumnModel columnModel9 = c.String(nullable9, new int?(32), nullable10, nullable1, null, null, null, null, null);
				nullable1 = null;
				byte? nullable11 = null;
				DateTime? nullable12 = null;
				ColumnModel columnModel10 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel11 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel12 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				nullable11 = null;
				nullable12 = null;
				ColumnModel columnModel13 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				nullable11 = null;
				nullable12 = null;
				ColumnModel columnModel14 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel15 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable11 = null;
				nullable12 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, Surname = columnModel3, UserName = columnModel4, Password = columnModel5, EmailAddress = columnModel6, IsEmailConfirmed = columnModel7, EmailConfirmationCode = columnModel8, PasswordResetCode = columnModel9, LastLoginTime = columnModel10, IsDeleted = columnModel11, DeleterUserId = columnModel12, DeletionTime = columnModel13, LastModificationTime = columnModel14, LastModifierUserId = columnModel15, CreationTime = c.DateTime(new bool?(false), nullable11, nullable12, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpUsers", (t) => t.CreatorUserId, false, null, null).ForeignKey("dbo.AbpUsers", (t) => t.DeleterUserId, false, null, null).ForeignKey("dbo.AbpUsers", (t) => t.LastModifierUserId, false, null, null).ForeignKey("dbo.AbpTenants", (t) => t.TenantId, false, null, null).Index((t) => t.TenantId, null, false, false, null).Index((t) => t.DeleterUserId, null, false, false, null).Index((t) => t.LastModifierUserId, null, false, false, null).Index((t) => t.CreatorUserId, null, false, false, null);
			base.CreateTable("dbo.AbpUserLogins", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				int? nullable3 = null;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(nullable2, nullable3, nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable3 = null;
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				return new { Id = columnModel, UserId = columnModel1, LoginProvider = columnModel2, ProviderKey = c.String(nullable5, nullable3, nullable6, nullable1, null, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpUsers", (t) => t.UserId, true, null, null).Index((t) => t.UserId, null, false, false, null);
			base.CreateTable("dbo.AbpUserRoles", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				ColumnModel columnModel2 = c.Int(new bool?(false), false, null, null, null, null, null);
				byte? nullable1 = null;
				DateTime? nullable2 = null;
				bool? nullable3 = null;
				nullable = null;
				return new { Id = columnModel, UserId = columnModel1, RoleId = columnModel2, CreationTime = c.DateTime(new bool?(false), nullable1, nullable2, null, null, null, null), CreatorUserId = c.Long(nullable3, false, nullable, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpUsers", (t) => t.UserId, true, null, null).Index((t) => t.UserId, null, false, false, null);
			base.CreateTable("dbo.AbpSettings", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				int? nullable2 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel2 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable2 = null;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable3, nullable2, nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable2 = null;
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable5, nullable2, nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, UserId = columnModel2, Name = columnModel3, Value = columnModel4, LastModificationTime = columnModel5, LastModifierUserId = columnModel6, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpUsers", (t) => t.UserId, false, null, null).ForeignKey("dbo.AbpTenants", (t) => t.TenantId, false, null, null).Index((t) => t.TenantId, null, false, false, null).Index((t) => t.UserId, null, false, false, null);
			base.CreateTable("dbo.AbpTenants", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel1 = c.String(nullable2, nullable, nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable = null;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(nullable4, nullable, nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel3 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				long? nullable8 = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable8, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable1 = null;
				nullable8 = null;
				return new { Id = columnModel, TenancyName = columnModel1, Name = columnModel2, LastModificationTime = columnModel3, LastModifierUserId = columnModel4, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable8, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AbpUsers", (t) => t.CreatorUserId, false, null, null).ForeignKey("dbo.AbpUsers", (t) => t.LastModifierUserId, false, null, null).Index((t) => t.LastModifierUserId, null, false, false, null).Index((t) => t.CreatorUserId, null, false, false, null);
		}
	}
}