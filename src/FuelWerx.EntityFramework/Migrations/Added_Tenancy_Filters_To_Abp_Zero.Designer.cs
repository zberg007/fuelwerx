using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class Added_Tenancy_Filters_To_Abp_Zero : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(Added_Tenancy_Filters_To_Abp_Zero));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201503291617289_Added_Tenancy_Filters_To_Abp_Zero";
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

		public Added_Tenancy_Filters_To_Abp_Zero()
		{
		}

		public override void Down()
		{
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "Abp_SoftDelete", new AnnotationValues(null, "True") },
				{ "DynamicFilter_Tenant_SoftDelete", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.AbpTenants", (ColumnBuilder c) => {
				ColumnModel columnModel = c.Int(new bool?(false), true, null, null, null, null, null);
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				ColumnModel columnModel1 = c.String(new bool?(false), new int?(64), nullable1, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(128), nullable2, nullable, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				long? nullable3 = null;
				ColumnModel columnModel5 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel6 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel7 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable3 = null;
				ColumnModel columnModel8 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable = null;
				nullable3 = null;
				return new { Id = columnModel, TenancyName = columnModel1, Name = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable, false, nullable3, null, null, null, null) };
			}, strs, null);
			Dictionary<string, AnnotationValues> strs1 = new Dictionary<string, AnnotationValues>()
			{
				{ "Abp_SoftDelete", new AnnotationValues(null, "True") },
				{ "DynamicFilter_User_MayHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) },
				{ "DynamicFilter_User_SoftDelete", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.AbpUsers", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel1 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel2 = c.Int(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(32), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(32), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(32), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel6 = c.String(new bool?(false), new int?(128), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), new int?(256), nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				bool? nullable7 = nullable1;
				nullable1 = null;
				bool? nullable8 = nullable1;
				nullable1 = null;
				ColumnModel columnModel9 = c.String(nullable7, new int?(16), nullable8, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable9 = nullable1;
				nullable1 = null;
				bool? nullable10 = nullable1;
				nullable1 = null;
				ColumnModel columnModel10 = c.String(nullable9, new int?(32), nullable10, nullable1, null, null, null, null, null);
				nullable1 = null;
				byte? nullable11 = null;
				DateTime? nullable12 = null;
				ColumnModel columnModel11 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel12 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel13 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel14 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				nullable11 = null;
				nullable12 = null;
				ColumnModel columnModel15 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				nullable11 = null;
				nullable12 = null;
				ColumnModel columnModel16 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel17 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable11 = null;
				nullable12 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ShouldChangePasswordOnNextLogin = columnModel1, TenantId = columnModel2, Name = columnModel3, Surname = columnModel4, UserName = columnModel5, Password = columnModel6, EmailAddress = columnModel7, IsEmailConfirmed = columnModel8, EmailConfirmationCode = columnModel9, PasswordResetCode = columnModel10, LastLoginTime = columnModel11, IsActive = columnModel12, IsDeleted = columnModel13, DeleterUserId = columnModel14, DeletionTime = columnModel15, LastModificationTime = columnModel16, LastModifierUserId = columnModel17, CreationTime = c.DateTime(new bool?(false), nullable11, nullable12, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null);
			Dictionary<string, AnnotationValues> strs2 = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Role_MayHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.AbpRoles", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(32), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(64), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable4, nullable5, null, null, null, null);
				nullable1 = null;
				long? nullable6 = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable1 = null;
				nullable6 = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, DisplayName = columnModel3, IsStatic = columnModel4, LastModificationTime = columnModel5, LastModifierUserId = columnModel6, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable6, null, null, null, null) };
			}, strs2, null);
		}

		public override void Up()
		{
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Role_MayHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.AbpRoles", (ColumnBuilder c) => {
				int? nullable = null;
				ColumnModel columnModel = c.Int(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				nullable = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(32), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(64), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel5 = c.DateTime(nullable1, nullable4, nullable5, null, null, null, null);
				nullable1 = null;
				long? nullable6 = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable6, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable1 = null;
				nullable6 = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, DisplayName = columnModel3, IsStatic = columnModel4, LastModificationTime = columnModel5, LastModifierUserId = columnModel6, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable6, null, null, null, null) };
			}, strs, null);
			Dictionary<string, AnnotationValues> strs1 = new Dictionary<string, AnnotationValues>()
			{
				{ "Abp_SoftDelete", new AnnotationValues("True", null) },
				{ "DynamicFilter_User_MayHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") },
				{ "DynamicFilter_User_SoftDelete", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.AbpUsers", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel1 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel2 = c.Int(nullable1, false, null, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(32), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(32), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(32), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel6 = c.String(new bool?(false), new int?(128), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), new int?(256), nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				bool? nullable7 = nullable1;
				nullable1 = null;
				bool? nullable8 = nullable1;
				nullable1 = null;
				ColumnModel columnModel9 = c.String(nullable7, new int?(16), nullable8, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable9 = nullable1;
				nullable1 = null;
				bool? nullable10 = nullable1;
				nullable1 = null;
				ColumnModel columnModel10 = c.String(nullable9, new int?(32), nullable10, nullable1, null, null, null, null, null);
				nullable1 = null;
				byte? nullable11 = null;
				DateTime? nullable12 = null;
				ColumnModel columnModel11 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel12 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel13 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel14 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				nullable11 = null;
				nullable12 = null;
				ColumnModel columnModel15 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				nullable11 = null;
				nullable12 = null;
				ColumnModel columnModel16 = c.DateTime(nullable1, nullable11, nullable12, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel17 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable11 = null;
				nullable12 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ShouldChangePasswordOnNextLogin = columnModel1, TenantId = columnModel2, Name = columnModel3, Surname = columnModel4, UserName = columnModel5, Password = columnModel6, EmailAddress = columnModel7, IsEmailConfirmed = columnModel8, EmailConfirmationCode = columnModel9, PasswordResetCode = columnModel10, LastLoginTime = columnModel11, IsActive = columnModel12, IsDeleted = columnModel13, DeleterUserId = columnModel14, DeletionTime = columnModel15, LastModificationTime = columnModel16, LastModifierUserId = columnModel17, CreationTime = c.DateTime(new bool?(false), nullable11, nullable12, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null);
			Dictionary<string, AnnotationValues> strs2 = new Dictionary<string, AnnotationValues>()
			{
				{ "Abp_SoftDelete", new AnnotationValues("True", null) },
				{ "DynamicFilter_Tenant_SoftDelete", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.AbpTenants", (ColumnBuilder c) => {
				ColumnModel columnModel = c.Int(new bool?(false), true, null, null, null, null, null);
				bool? nullable = null;
				bool? nullable1 = nullable;
				nullable = null;
				ColumnModel columnModel1 = c.String(new bool?(false), new int?(64), nullable1, nullable, null, null, null, null, null);
				nullable = null;
				bool? nullable2 = nullable;
				nullable = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(128), nullable2, nullable, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable, null, null, null, null);
				nullable = null;
				long? nullable3 = null;
				ColumnModel columnModel5 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable = null;
				byte? nullable4 = null;
				DateTime? nullable5 = null;
				ColumnModel columnModel6 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable4 = null;
				nullable5 = null;
				ColumnModel columnModel7 = c.DateTime(nullable, nullable4, nullable5, null, null, null, null);
				nullable = null;
				nullable3 = null;
				ColumnModel columnModel8 = c.Long(nullable, false, nullable3, null, null, null, null);
				nullable4 = null;
				nullable5 = null;
				nullable = null;
				nullable3 = null;
				return new { Id = columnModel, TenancyName = columnModel1, Name = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable4, nullable5, null, null, null, null), CreatorUserId = c.Long(nullable, false, nullable3, null, null, null, null) };
			}, strs2, null);
		}
	}
}