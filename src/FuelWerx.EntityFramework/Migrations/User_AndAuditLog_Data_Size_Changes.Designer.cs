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
	public sealed class User_AndAuditLog_Data_Size_Changes : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(User_AndAuditLog_Data_Size_Changes));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201504021549075_User_AndAuditLog_Data_Size_Changes";
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

		public User_AndAuditLog_Data_Size_Changes()
		{
		}

		public override void Down()
		{
			base.AlterColumn("dbo.AbpUsers", "PasswordResetCode", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(32), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "EmailConfirmationCode", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(16), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpAuditLogs", "Exception", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(2048), nullable2, nullable, null, null, null, null, null);
			}, null);
		}

		public override void Up()
		{
			base.AlterColumn("dbo.AbpAuditLogs", "Exception", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(2000), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "EmailConfirmationCode", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(128), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.AbpUsers", "PasswordResetCode", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(128), nullable2, nullable, null, null, null, null, null);
			}, null);
		}
	}
}