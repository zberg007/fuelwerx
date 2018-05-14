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
	public sealed class AuditLog_Made_MayHaveTenant : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(AuditLog_Made_MayHaveTenant));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201506141245516_AuditLog_Made_MayHaveTenant";
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

		public AuditLog_Made_MayHaveTenant()
		{
		}

		public override void Down()
		{
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_AuditLog_MayHaveTenant", new AnnotationValues("EntityFramework.DynamicFilters.DynamicFilterDefinition", null) }
			};
			base.AlterTableAnnotations("dbo.AbpAuditLogs", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				int? nullable2 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel2 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable3, new int?(256), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable5, new int?(256), nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable7 = nullable1;
				nullable1 = null;
				bool? nullable8 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(nullable7, new int?(1024), nullable8, nullable1, null, null, null, null, null);
				ColumnModel columnModel6 = c.DateTime(new bool?(false), null, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel7 = c.Int(new bool?(false), false, nullable2, null, null, null, null);
				nullable1 = null;
				bool? nullable9 = nullable1;
				nullable1 = null;
				bool? nullable10 = nullable1;
				nullable1 = null;
				ColumnModel columnModel8 = c.String(nullable9, new int?(64), nullable10, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable11 = nullable1;
				nullable1 = null;
				bool? nullable12 = nullable1;
				nullable1 = null;
				ColumnModel columnModel9 = c.String(nullable11, new int?(128), nullable12, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable13 = nullable1;
				nullable1 = null;
				bool? nullable14 = nullable1;
				nullable1 = null;
				ColumnModel columnModel10 = c.String(nullable13, new int?(256), nullable14, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable15 = nullable1;
				nullable1 = null;
				bool? nullable16 = nullable1;
				nullable1 = null;
				return new { Id = columnModel, TenantId = columnModel1, UserId = columnModel2, ServiceName = columnModel3, MethodName = columnModel4, Parameters = columnModel5, ExecutionTime = columnModel6, ExecutionDuration = columnModel7, ClientIpAddress = columnModel8, ClientName = columnModel9, BrowserInfo = columnModel10, Exception = c.String(nullable15, new int?(2000), nullable16, nullable1, null, null, null, null, null) };
			}, strs, null);
		}

		public override void Up()
		{
			Dictionary<string, AnnotationValues> strs = new Dictionary<string, AnnotationValues>()
			{
				{ "DynamicFilter_AuditLog_MayHaveTenant", new AnnotationValues(null, "EntityFramework.DynamicFilters.DynamicFilterDefinition") }
			};
			base.AlterTableAnnotations("dbo.AbpAuditLogs", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				bool? nullable1 = null;
				int? nullable2 = null;
				ColumnModel columnModel1 = c.Int(nullable1, false, nullable2, null, null, null, null);
				nullable1 = null;
				nullable = null;
				ColumnModel columnModel2 = c.Long(nullable1, false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable3 = nullable1;
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				ColumnModel columnModel3 = c.String(nullable3, new int?(256), nullable4, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable5, new int?(256), nullable6, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable7 = nullable1;
				nullable1 = null;
				bool? nullable8 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(nullable7, new int?(1024), nullable8, nullable1, null, null, null, null, null);
				ColumnModel columnModel6 = c.DateTime(new bool?(false), null, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel7 = c.Int(new bool?(false), false, nullable2, null, null, null, null);
				nullable1 = null;
				bool? nullable9 = nullable1;
				nullable1 = null;
				bool? nullable10 = nullable1;
				nullable1 = null;
				ColumnModel columnModel8 = c.String(nullable9, new int?(64), nullable10, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable11 = nullable1;
				nullable1 = null;
				bool? nullable12 = nullable1;
				nullable1 = null;
				ColumnModel columnModel9 = c.String(nullable11, new int?(128), nullable12, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable13 = nullable1;
				nullable1 = null;
				bool? nullable14 = nullable1;
				nullable1 = null;
				ColumnModel columnModel10 = c.String(nullable13, new int?(256), nullable14, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable15 = nullable1;
				nullable1 = null;
				bool? nullable16 = nullable1;
				nullable1 = null;
				return new { Id = columnModel, TenantId = columnModel1, UserId = columnModel2, ServiceName = columnModel3, MethodName = columnModel4, Parameters = columnModel5, ExecutionTime = columnModel6, ExecutionDuration = columnModel7, ClientIpAddress = columnModel8, ClientName = columnModel9, BrowserInfo = columnModel10, Exception = c.String(nullable15, new int?(2000), nullable16, nullable1, null, null, null, null, null) };
			}, strs, null);
		}
	}
}