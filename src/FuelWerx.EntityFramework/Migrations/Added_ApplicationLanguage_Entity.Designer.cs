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
	public sealed class Added_ApplicationLanguage_Entity : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Added_ApplicationLanguage_Entity));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201510270717447_Added_ApplicationLanguage_Entity";
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

		public Added_ApplicationLanguage_Entity()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.AbpLanguages", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.AbpLanguages", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(10), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(64), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable4, new int?(128), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				long? nullable6 = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable1 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable1 = null;
				nullable6 = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, DisplayName = columnModel3, Icon = columnModel4, IsActive = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable6, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}