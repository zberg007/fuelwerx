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
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class fuelwerxmigration97 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxmigration97));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602290724289_fuelwerxmigration97";
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

		public fuelwerxmigration97()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTenantNotifications", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantNotifications_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantNotifications_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantNotifications_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantNotifications_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantNotifications", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.String(nullable3, nullable1, nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.Boolean(nullable5, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable6, new int?(450), nullable7, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.Boolean(nullable8, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable9, new int?(450), nullable10, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.Boolean(nullable11, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(nullable12, new int?(450), nullable13, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.Boolean(nullable14, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable2 = null;
				ColumnModel columnModel10 = c.String(nullable15, new int?(450), nullable16, nullable2, null, null, null, null, null);
				ColumnModel columnModel11 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null);
				nullable2 = null;
				ColumnModel columnModel12 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel13 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable17 = null;
				DateTime? nullable18 = null;
				ColumnModel columnModel14 = c.DateTime(nullable2, nullable17, nullable18, null, null, null, null);
				nullable2 = null;
				nullable17 = null;
				nullable18 = null;
				ColumnModel columnModel15 = c.DateTime(nullable2, nullable17, nullable18, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel16 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable17 = null;
				nullable18 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Note = columnModel2, NewOrder = columnModel3, NewOrderEmails = columnModel4, NewCustomer = columnModel5, NewCustomerEmails = columnModel6, NewMessage = columnModel7, NewMessageEmails = columnModel8, LowPercentage = columnModel9, LowPercentageEmails = columnModel10, LowPercentageThreshold = columnModel11, IsDeleted = columnModel12, DeleterUserId = columnModel13, DeletionTime = columnModel14, LastModificationTime = columnModel15, LastModifierUserId = columnModel16, CreationTime = c.DateTime(new bool?(false), nullable17, nullable18, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}