using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class fuelwerxmigration99 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration99));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602291205594_fuelwerxmigration99";
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

		public fuelwerxmigration99()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxQuickMenuItems");
		}

		public override void Up()
		{
			base.CreateTable("dbo.FuelWerxQuickMenuItems", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(nullable2, new int?(400), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable4, new int?(120), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				bool? nullable7 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable6, new int?(400), nullable7, nullable1, null, null, null, null, null);
				nullable1 = null;
				return new { Id = columnModel, OwnerId = columnModel1, FaviconUrl = columnModel2, Label = columnModel3, Url = columnModel4, LoadInNewWindow = c.Boolean(new bool?(false), nullable1, null, null, null, null) };
			}, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}