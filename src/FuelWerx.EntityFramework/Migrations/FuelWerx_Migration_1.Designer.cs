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
	public sealed class FuelWerx_Migration_1 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(FuelWerx_Migration_1));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201602030222103_FuelWerx_Migration_1";
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

		public FuelWerx_Migration_1()
		{
		}

		public override void Down()
		{
		}

		public override void Up()
		{
			Dictionary<string, object> strs = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Address_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Address_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxAddresses", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), nullable1, nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(50), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(255), nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(nullable6, new int?(255), nullable7, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), new int?(255), nullable8, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				bool? nullable10 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(nullable9, new int?(255), nullable10, nullable2, null, null, null, null, null);
				nullable1 = null;
				ColumnModel columnModel9 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable2 = null;
				nullable1 = null;
				ColumnModel columnModel10 = c.Int(nullable2, false, nullable1, null, null, null, null);
				nullable2 = null;
				bool? nullable11 = nullable2;
				nullable2 = null;
				bool? nullable12 = nullable2;
				nullable2 = null;
				ColumnModel columnModel11 = c.String(nullable11, new int?(255), nullable12, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel12 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel13 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel14 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable13 = null;
				DateTime? nullable14 = null;
				ColumnModel columnModel15 = c.DateTime(nullable2, nullable13, nullable14, null, null, null, null);
				nullable2 = null;
				nullable13 = null;
				nullable14 = null;
				ColumnModel columnModel16 = c.DateTime(nullable2, nullable13, nullable14, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel17 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable13 = null;
				nullable14 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, OwnerId = columnModel2, OwnerType = columnModel3, Type = columnModel4, PrimaryAddress = columnModel5, SecondaryAddress = columnModel6, City = columnModel7, PostalCode = columnModel8, CountryId = columnModel9, CountryRegionId = columnModel10, ContactName = columnModel11, IsActive = columnModel12, IsDeleted = columnModel13, DeleterUserId = columnModel14, DeletionTime = columnModel15, LastModificationTime = columnModel16, LastModifierUserId = columnModel17, CreationTime = c.DateTime(new bool?(false), nullable13, nullable14, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxCountries", (t) => t.CountryId, true, null, null).ForeignKey("dbo.FuelWerxCountryRegions", (t) => t.CountryRegionId, false, null, null).Index((t) => t.CountryId, null, false, false, null).Index((t) => t.CountryRegionId, null, false, false, null);
			Dictionary<string, object> strs1 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Bank_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Bank_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxBanks", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), nullable1, nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(50), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(255), nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(new bool?(false), new int?(255), nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), new int?(255), nullable7, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				bool? nullable9 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(nullable8, new int?(600), nullable9, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel10 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable10 = null;
				DateTime? nullable11 = null;
				ColumnModel columnModel12 = c.DateTime(nullable2, nullable10, nullable11, null, null, null, null);
				nullable2 = null;
				nullable10 = null;
				nullable11 = null;
				ColumnModel columnModel13 = c.DateTime(nullable2, nullable10, nullable11, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel14 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable10 = null;
				nullable11 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, OwnerId = columnModel2, OwnerType = columnModel3, AccountType = columnModel4, Name = columnModel5, AccountNumber = columnModel6, RoutingNumber = columnModel7, Note = columnModel8, IsActive = columnModel9, IsDeleted = columnModel10, DeleterUserId = columnModel11, DeletionTime = columnModel12, LastModificationTime = columnModel13, LastModifierUserId = columnModel14, CreationTime = c.DateTime(new bool?(false), nullable10, nullable11, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs1, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs2 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Customer_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Customer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxCustomers", (ColumnBuilder c) => {
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
				ColumnModel columnModel3 = c.String(new bool?(false), new int?(255), nullable3, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable4 = nullable1;
				nullable1 = null;
				bool? nullable5 = nullable1;
				nullable1 = null;
				ColumnModel columnModel4 = c.String(nullable4, new int?(255), nullable5, nullable1, null, null, null, null, null);
				nullable1 = null;
				bool? nullable6 = nullable1;
				nullable1 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(255), nullable6, nullable1, null, null, null, null, null);
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
				return new { Id = columnModel, TenantId = columnModel1, FirstName = columnModel2, LastName = columnModel3, BusinessName = columnModel4, Email = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable7, nullable8, null, null, null, null), CreatorUserId = c.Long(nullable1, false, nullable, null, null, null, null) };
			}, strs2, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs3 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Phone_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Phone_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxPhones", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel3 = c.String(new bool?(false), nullable1, nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), new int?(50), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(16), nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel6 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel7 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel8 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable6 = null;
				DateTime? nullable7 = null;
				ColumnModel columnModel9 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				nullable6 = null;
				nullable7 = null;
				ColumnModel columnModel10 = c.DateTime(nullable2, nullable6, nullable7, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable6 = null;
				nullable7 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, OwnerId = columnModel2, OwnerType = columnModel3, Type = columnModel4, PhoneNumber = columnModel5, IsActive = columnModel6, IsDeleted = columnModel7, DeleterUserId = columnModel8, DeletionTime = columnModel9, LastModificationTime = columnModel10, LastModifierUserId = columnModel11, CreationTime = c.DateTime(new bool?(false), nullable6, nullable7, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs3, null).PrimaryKey((t) => t.Id, null, true, null);
			Dictionary<string, object> strs4 = new Dictionary<string, object>()
			{
				{ "DynamicFilter_Service_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
				{ "DynamicFilter_Service_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" }
			};
			base.CreateTable("dbo.FuelWerxServices", (ColumnBuilder c) => {
				long? nullable = null;
				ColumnModel columnModel = c.Long(new bool?(false), true, nullable, null, null, null, null);
				int? nullable1 = null;
				ColumnModel columnModel1 = c.Int(new bool?(false), false, nullable1, null, null, null, null);
				nullable = null;
				ColumnModel columnModel2 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable = null;
				ColumnModel columnModel3 = c.Long(new bool?(false), false, nullable, null, null, null, null);
				nullable1 = null;
				bool? nullable2 = null;
				bool? nullable3 = nullable2;
				nullable2 = null;
				ColumnModel columnModel4 = c.String(new bool?(false), nullable1, nullable3, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable4 = nullable2;
				nullable2 = null;
				ColumnModel columnModel5 = c.String(new bool?(false), new int?(255), nullable4, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable5 = nullable2;
				nullable2 = null;
				ColumnModel columnModel6 = c.String(new bool?(false), new int?(255), nullable5, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable6 = nullable2;
				nullable2 = null;
				ColumnModel columnModel7 = c.String(new bool?(false), new int?(1200), nullable6, nullable2, null, null, null, null, null);
				nullable2 = null;
				bool? nullable7 = nullable2;
				nullable1 = null;
				nullable2 = null;
				bool? nullable8 = nullable2;
				nullable2 = null;
				ColumnModel columnModel8 = c.String(nullable7, nullable1, nullable8, nullable2, null, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel9 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				ColumnModel columnModel10 = c.Boolean(new bool?(false), nullable2, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel11 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable2 = null;
				byte? nullable9 = null;
				DateTime? nullable10 = null;
				ColumnModel columnModel12 = c.DateTime(nullable2, nullable9, nullable10, null, null, null, null);
				nullable2 = null;
				nullable9 = null;
				nullable10 = null;
				ColumnModel columnModel13 = c.DateTime(nullable2, nullable9, nullable10, null, null, null, null);
				nullable2 = null;
				nullable = null;
				ColumnModel columnModel14 = c.Long(nullable2, false, nullable, null, null, null, null);
				nullable9 = null;
				nullable10 = null;
				nullable2 = null;
				nullable = null;
				return new { Id = columnModel, TenantId = columnModel1, OwnerId = columnModel2, AddressId = columnModel3, OwnerType = columnModel4, Name = columnModel5, Type = columnModel6, RequestedServices = columnModel7, Note = columnModel8, IsActive = columnModel9, IsDeleted = columnModel10, DeleterUserId = columnModel11, DeletionTime = columnModel12, LastModificationTime = columnModel13, LastModifierUserId = columnModel14, CreationTime = c.DateTime(new bool?(false), nullable9, nullable10, null, null, null, null), CreatorUserId = c.Long(nullable2, false, nullable, null, null, null, null) };
			}, strs4, null).PrimaryKey((t) => t.Id, null, true, null).ForeignKey("dbo.FuelWerxAddresses", (t) => t.AddressId, true, null, null).Index((t) => t.AddressId, null, false, false, null);
		}
	}
}