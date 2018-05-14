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
	public sealed class FuelWerxMigration80 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration80));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602271814458_FuelWerxMigration80";
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

		public FuelWerxMigration80()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTenantLogos", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantLogos_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantLogos_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TenantLogos_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TenantLogos_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTenantLogos", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				Guid? nullable2 = null;
				ColumnModel columnModel2 = c.Guid(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				ColumnModel columnModel3 = c.Guid(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				ColumnModel columnModel4 = c.Guid(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				ColumnModel columnModel5 = c.Guid(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable3 = null;
				DateTime? nullable4 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable3 = null;
				nullable4 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable3, nullable4, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable3 = null;
				nullable4 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, HeaderImageId = columnModel2, HeaderMobileImageId = columnModel3, MailImageId = columnModel4, InvoiceImageId = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable3, nullable4, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}