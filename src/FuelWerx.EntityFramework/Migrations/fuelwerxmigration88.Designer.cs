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
	public sealed class fuelwerxmigration88 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration88));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602290411582_fuelwerxmigration88";
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

		public fuelwerxmigration88()
		{
		}

		public override void Down()
		{
			base.AlterColumn("dbo.FuelWerxTenantHours", "SundayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "SundayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "SaturdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "SaturdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "FridayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "FridayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "ThursdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "ThursdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "WednesdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "WednesdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "TuesdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "TuesdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "MondayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "MondayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				int? nullable2 = null;
				nullable = null;
				bool? nullable3 = nullable;
				nullable = null;
				return c.String(nullable1, nullable2, nullable3, nullable, null, null, null, null, null);
			}, null);
		}

		public override void Up()
		{
			base.AlterColumn("dbo.FuelWerxTenantHours", "MondayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "MondayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "TuesdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "TuesdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "WednesdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "WednesdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "ThursdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "ThursdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "FridayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "FridayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "SaturdayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "SaturdayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "SundayOpen", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
			base.AlterColumn("dbo.FuelWerxTenantHours", "SundayClose", (ColumnBuilder c) => {
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				return c.String(nullable1, new int?(15), nullable2, nullable, null, null, null, null, null);
			}, null);
		}
	}
}