using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
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
	public sealed class FuelWerx_Migration_31 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_31));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602062002220_FuelWerx_Migration_31";
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

		public FuelWerx_Migration_31()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxEmergencyDeliveryFees", "ZoneId", "dbo.FuelWerxZones", null);
			base.DropIndex("dbo.FuelWerxEmergencyDeliveryFees", new string[] { "ZoneId" }, null);
			base.AlterColumn("dbo.FuelWerxTitles", "TenantId", (ColumnBuilder c) => c.Int(null, false, null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxContacts", "TenantId", (ColumnBuilder c) => c.Int(null, false, null, null, null, null, null), null);
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Title_MayHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") },
				{ "DynamicFilter_Title_MustHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.FuelWerxTitles", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
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
			}, strs, null);
			Dictionary<string, AnnotationValues> strs1 = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Contact_MayHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") },
				{ "DynamicFilter_Contact_MustHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.FuelWerxContacts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				ColumnModel columnModel2 = c.Guid(nullable2, false, null, null, null, null, null);
				nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(600), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable5, nullable1, nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel9 = c.DateTime(nullable2, nullable7, nullable8, null, null, null, null);
				nullable2 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel10 = c.DateTime(nullable2, nullable7, nullable8, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ImageId = columnModel2, Title = columnModel3, Email = columnModel4, Description = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs1, null);
			base.DropTable("dbo.FuelWerxTaxRules", new Dictionary<string, object>()
			{
				{ "DynamicFilter_TaxRule_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TaxRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxTaxes", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Tax_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Tax_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxZones", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Zone_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Zone_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxEmergencyDeliveryFees", new Dictionary<string, object>()
			{
				{ "DynamicFilter_EmergencyDeliveryFee_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EmergencyDeliveryFee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxEmergencyDeliveryFeeRules", new Dictionary<string, object>()
			{
				{ "DynamicFilter_EmergencyDeliveryFeeRule_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EmergencyDeliveryFeeRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_EmergencyDeliveryFeeRule_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EmergencyDeliveryFeeRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxEmergencyDeliveryFeeRules", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable3, new int?(600), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, Caption = columnModel3, IsActive = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_EmergencyDeliveryFee_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_EmergencyDeliveryFee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxEmergencyDeliveryFees", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				ColumnModel columnModel3 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel4 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(nullable3, new int?(600), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel10 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, Fee = columnModel3, ZoneId = columnModel4, Caption = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxZones", (t) => t.ZoneId, false, null, null).Index((t) => t.ZoneId, null, false, false, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Zone_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Zone_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxZones", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable3, new int?(600), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, Caption = columnModel3, IsActive = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs3 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Tax_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Tax_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTaxes", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				ColumnModel columnModel3 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable3, new int?(600), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel7 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel9 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, Rate = columnModel3, Caption = columnModel4, IsActive = columnModel5, IsDeleted = columnModel6, DeleterUserId = columnModel7, DeletionTime = columnModel8, LastModificationTime = columnModel9, LastModifierUserId = columnModel10, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs3, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs4 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_TaxRule_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_TaxRule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxTaxRules", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable3, new int?(600), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel5 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel6 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable5 = null;
				DateTime? nullable6 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable5 = null;
				nullable6 = null;
				ColumnModel columnModel8 = c.DateTime(nullable1, nullable5, nullable6, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel9 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable5 = null;
				nullable6 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Name = columnModel2, Caption = columnModel3, IsActive = columnModel4, IsDeleted = columnModel5, DeleterUserId = columnModel6, DeletionTime = columnModel7, LastModificationTime = columnModel8, LastModifierUserId = columnModel9, CreationTime = c.DateTime(new bool?(false), nullable5, nullable6, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs4, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, AnnotationValues> strs5 = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Contact_MayHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) },
				{ "DynamicFilter_Contact_MustHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.FuelWerxContacts", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				bool? nullable2 = null;
				ColumnModel columnModel2 = c.Guid(nullable2, false, null, null, null, null, null);
				nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(600), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable5, nullable1, nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable7 = null;
				DateTime? nullable8 = null;
				ColumnModel columnModel9 = c.DateTime(nullable2, nullable7, nullable8, null, null, null, null);
				nullable2 = null;
				nullable7 = null;
				nullable8 = null;
				ColumnModel columnModel10 = c.DateTime(nullable2, nullable7, nullable8, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable7 = null;
				nullable8 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ImageId = columnModel2, Title = columnModel3, Email = columnModel4, Description = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs5, null);
			Dictionary<string, AnnotationValues> strs6 = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_Title_MayHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) },
				{ "DynamicFilter_Title_MustHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.FuelWerxTitles", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				bool? nullable1 = null;
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
			}, strs6, null);
			base.AlterColumn("dbo.FuelWerxContacts", "TenantId", (ColumnBuilder c) => c.Int(new bool?(false), false, null, null, null, null, null), null);
			base.AlterColumn("dbo.FuelWerxTitles", "TenantId", (ColumnBuilder c) => c.Int(new bool?(false), false, null, null, null, null, null), null);
		}
	}
}