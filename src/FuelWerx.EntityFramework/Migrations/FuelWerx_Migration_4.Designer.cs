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
	public sealed class FuelWerx_Migration_4 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_4));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602030419287_FuelWerx_Migration_4";
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

		public FuelWerx_Migration_4()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxTitles", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Title_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Title_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxContacts", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Contact_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Contact_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Contact_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Contact_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxContacts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				int? nullable2 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel2 = c.Guid(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(600), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable2 = null;
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(nullable5, nullable2, nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel10 = c.DateTime(nullable1, nullable7, nullable8, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ImageId = columnModel2, Title = columnModel3, Email = columnModel4, Description = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Title_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Title_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTitles", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel2 = c.Guid(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(255), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable4, nullable5, null, null, null, null);
				nullable1 = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable4, nullable5, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ImageId = columnModel2, Name = columnModel3, Type = columnModel4, IsActive = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}