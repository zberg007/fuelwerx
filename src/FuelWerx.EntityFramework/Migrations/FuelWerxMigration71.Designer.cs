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
	public sealed class FuelWerxMigration71 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration71));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602230417061_FuelWerxMigration71";
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

		public FuelWerxMigration71()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProjectResources", "ProjectId", "dbo.FuelWerxProjects", null);
			base.DropForeignKey("dbo.FuelWerxProjectTeamMembers", "TeamMemberId", "dbo.AbpUsers", null);
			base.DropForeignKey("dbo.FuelWerxProjectTeamMembers", "ProjectId", "dbo.FuelWerxProjects", null);
			base.DropForeignKey("dbo.FuelWerxProjectTasks", "ProjectId", "dbo.FuelWerxProjects", null);
			base.DropForeignKey("dbo.FuelWerxProjects", "CustomerId", "dbo.FuelWerxCustomers", null);
			base.DropForeignKey("dbo.FuelWerxProjectResources", "BinaryObjectId", "dbo.AppBinaryObjects", null);
			base.DropIndex("dbo.FuelWerxProjectTeamMembers", new string[] { "TeamMemberId" }, null);
			base.DropIndex("dbo.FuelWerxProjectTeamMembers", new string[] { "ProjectId" }, null);
			base.DropIndex("dbo.FuelWerxProjectTasks", new string[] { "ProjectId" }, null);
			base.DropIndex("dbo.FuelWerxProjects", new string[] { "CustomerId" }, null);
			base.DropIndex("dbo.FuelWerxProjectResources", new string[] { "BinaryObjectId" }, null);
			base.DropIndex("dbo.FuelWerxProjectResources", new string[] { "ProjectId" }, null);
			base.DropTable("dbo.FuelWerxProjectTeamMembers", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectTeamMember_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProjectTasks", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProjectTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProjects", new Dictionary<string, object>()
			{
				{ "DynamicFilter_Project_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Project_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
			base.DropTable("dbo.FuelWerxProjectResources", new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProjectResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectResource_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProjectResource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjectResources", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel2 = c.String(new bool?(false), nullable1, nullable3, nullable2, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				ColumnModel columnModel4 = c.Guid(new bool?(false), false, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(255), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable5, new int?(1200), nullable6, nullable2, null, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), nullable1, nullable7, nullable2, null, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(new bool?(false), nullable1, nullable8, nullable2, null, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.String(new bool?(false), nullable1, nullable9, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel10 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel11 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel12 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable10 = null;
				DateTime? nullable11 = null;
				ColumnModel columnModel13 = c.DateTime(nullable2, nullable10, nullable11, null, null, null, null);
				nullable2 = null;
				nullable10 = null;
				nullable11 = null;
				ColumnModel columnModel14 = c.DateTime(nullable2, nullable10, nullable11, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel15 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable10 = null;
				nullable11 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, Category = columnModel2, ProjectId = columnModel3, BinaryObjectId = columnModel4, Name = columnModel5, Description = columnModel6, FileName = columnModel7, FileExtension = columnModel8, FileSize = columnModel9, IsActive = columnModel10, IsDeleted = columnModel11, DeleterUserId = columnModel12, DeletionTime = columnModel13, LastModificationTime = columnModel14, LastModifierUserId = columnModel15, CreationTime = c.DateTime(new bool?(false), nullable10, nullable11, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.AppBinaryObjects", (t) => t.BinaryObjectId, true, null, null).ForeignKey("dbo.FuelWerxProjects", (t) => t.ProjectId, true, null, null).Index((t) => t.ProjectId, null, false, false, null).Index((t) => t.BinaryObjectId, null, false, false, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Project_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Project_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjects", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(155), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(38), nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel6 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(nullable8, new int?(99), nullable9, nullable2, null, null, null, null, null);
				decimal? nullable10 = null;
				ColumnModel columnModel8 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel9 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel10 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel11 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel12 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable10 = null;
				ColumnModel columnModel13 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), nullable10, null, null, null, false, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel14 = c.String(nullable11, nullable1, nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel15 = c.String(nullable13, new int?(50), nullable14, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable2 = null;
				ColumnModel columnModel16 = c.Boolean(nullable15, nullable2, null, null, null, null);
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable2 = null;
				ColumnModel columnModel17 = c.String(nullable16, nullable1, nullable17, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable19 = nullable2;
				nullable2 = null;
				ColumnModel columnModel18 = c.String(nullable18, nullable1, nullable19, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel19 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel20 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel21 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel22 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel23 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel24 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, CustomerId = columnModel2, Label = columnModel3, BillingType = columnModel4, Number = columnModel5, Date = columnModel6, PONumber = columnModel7, Discount = columnModel8, Rate = columnModel9, Hours = columnModel10, HoursActual = columnModel11, Tax = columnModel12, LineTotal = columnModel13, Terms = columnModel14, CurrentStatus = columnModel15, LogDataAndTasksVisibleToCustomer = columnModel16, TimeEntryLog = columnModel17, Description = columnModel18, IsActive = columnModel19, IsDeleted = columnModel20, DeleterUserId = columnModel21, DeletionTime = columnModel22, LastModificationTime = columnModel23, LastModifierUserId = columnModel24, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCustomers", (t) => t.CustomerId, true, null, null).Index((t) => t.CustomerId, null, false, false, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_ProjectTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjectTasks", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				ColumnModel columnModel1 = c.Int(new bool?(false), false, null, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				bool? nullable2 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable2, nullable1, null, null, null, null, null);
				nullable1 = null;
				decimal? nullable3 = null;
				ColumnModel columnModel4 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				nullable3 = null;
				ColumnModel columnModel5 = c.Decimal(nullable1, new byte?((byte)18), new byte?((byte)2), nullable3, null, null, null, false, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel6 = c.String(nullable4, new int?(1200), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel8 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel10 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel11 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel12 = c.DateTime(nullable1, nullable6, nullable7, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel13 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, ProjectId = columnModel2, Name = columnModel3, Cost = columnModel4, Discount = columnModel5, Comment = columnModel6, IsComplete = columnModel7, IsActive = columnModel8, IsDeleted = columnModel9, DeleterUserId = columnModel10, DeletionTime = columnModel11, LastModificationTime = columnModel12, LastModifierUserId = columnModel13, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProjects", (t) => t.ProjectId, true, null, null).Index((t) => t.ProjectId, null, false, false, null);
			Dictionary<string, object> strs3 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_ProjectTeamMember_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxProjectTeamMembers", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel1 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable1 = null;
				ColumnModel columnModel3 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel4 = c.Boolean(new bool?(false), nullable1, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel5 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				byte? nullable2 = null;
				DateTime? nullable3 = null;
				ColumnModel columnModel6 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable2 = null;
				nullable3 = null;
				ColumnModel columnModel7 = c.DateTime(nullable1, nullable2, nullable3, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable3 = null;
				nullable1 = null;
				nullable = null;
				return new { Id = columnModel, ProjectId = columnModel1, TeamMemberId = columnModel2, IsActive = columnModel3, IsDeleted = columnModel4, DeleterUserId = columnModel5, DeletionTime = columnModel6, LastModificationTime = columnModel7, LastModifierUserId = columnModel8, CreationTime = c.DateTime(new bool?(false), nullable2, nullable3, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs3, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxProjects", (t) => t.ProjectId, true, null, null).ForeignKey("dbo.AbpUsers", (t) => t.TeamMemberId, true, null, null).Index((t) => t.ProjectId, null, false, false, null).Index((t) => t.TeamMemberId, null, false, false, null);
		}
	}
}