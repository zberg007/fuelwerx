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
	public sealed class FuelWerxMigration_3000 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerxMigration_3000));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201608210505285_FuelWerxMigration_3000";
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

		public FuelWerxMigration_3000()
		{
		}

		public override void Down()
		{
			base.DropTable("dbo.FuelWerxInvoicePayments", new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoicePayment_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_InvoicePayment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			}, null);
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_InvoicePayment_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_InvoicePayment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxInvoicePayments", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(nullable3, new int?(12), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(nullable5, new int?(12), nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable7, new int?(70), nullable8, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(nullable9, new int?(40), nullable10, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(nullable11, new int?(75), nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable13 = nullable2;
				nullable2 = null;
				bool? nullable14 = nullable2;
				nullable2 = null;
				ColumnModel columnModel9 = c.String(nullable13, new int?(75), nullable14, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable15 = nullable2;
				nullable2 = null;
				bool? nullable16 = nullable2;
				nullable2 = null;
				ColumnModel columnModel10 = c.String(nullable15, new int?(75), nullable16, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable17 = nullable2;
				nullable2 = null;
				bool? nullable18 = nullable2;
				nullable2 = null;
				ColumnModel columnModel11 = c.String(nullable17, new int?(75), nullable18, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable19 = nullable2;
				nullable2 = null;
				bool? nullable20 = nullable2;
				nullable2 = null;
				ColumnModel columnModel12 = c.String(nullable19, new int?(5), nullable20, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable21 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable22 = nullable2;
				nullable2 = null;
				ColumnModel columnModel13 = c.String(nullable21, nullable1, nullable22, nullable2, null, null, null, null, null);
				byte? nullable23 = null;
				DateTime? nullable24 = null;
				ColumnModel columnModel14 = c.DateTime(new bool?(false), nullable23, nullable24, null, null, null, null);
				ColumnModel columnModel15 = c.Decimal(new bool?(false), new byte?((byte)18), new byte?((byte)2), null, null, null, null, false, null);
				ColumnModel columnModel16 = c.Boolean(new bool?(false), new bool?(false), null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel17 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel18 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				nullable23 = null;
				nullable24 = null;
				ColumnModel columnModel19 = c.DateTime(nullable2, nullable23, nullable24, null, null, null, null);
				nullable2 = null;
				nullable23 = null;
				nullable24 = null;
				ColumnModel columnModel20 = c.DateTime(nullable2, nullable23, nullable24, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel21 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable23 = null;
				nullable24 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, InvoiceId = columnModel2, CustomerId = columnModel3, X_Response_Code = columnModel4, X_Response_Reason_Code = columnModel5, X_Response_Reason_Text = columnModel6, X_Auth_Code = columnModel7, X_Trans_Id = columnModel8, P_Exact_Ctr = columnModel9, P_Authorization_Num = columnModel10, P_Customer_Ref = columnModel11, P_Bank_Resp_Code = columnModel12, P_ResponseObject = columnModel13, TransactionDateTime = columnModel14, DollarAmount = columnModel15, ExportedToReporting = columnModel16, IsDeleted = columnModel17, DeleterUserId = columnModel18, DeletionTime = columnModel19, LastModificationTime = columnModel20, LastModifierUserId = columnModel21, CreationTime = c.DateTime(new bool?(false), nullable23, nullable24, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null);
		}
	}
}