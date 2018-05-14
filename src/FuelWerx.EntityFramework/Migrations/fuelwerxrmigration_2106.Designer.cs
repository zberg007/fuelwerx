using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace FuelWerx.Migrations
{
	[GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
	public sealed class fuelwerxrmigration_2106 : DbMigration, IMigrationMetadata
	{
		private readonly ResourceManager Resources = new ResourceManager(typeof(fuelwerxrmigration_2106));

		string System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata.Id
		{
			get
			{
				return "201604181337232_fuelwerxrmigration_2106";
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

		public fuelwerxrmigration_2106()
		{
		}

		public override void Down()
		{
			base.DropForeignKey("dbo.FuelWerxProductSpecificPrices", "ForOrganizationalUnitId", "dbo.AbpOrganizationUnits", null);
			base.DropForeignKey("dbo.FuelWerxProductSpecificPrices", "ForCustomerId", "dbo.FuelWerxCustomers", null);
			base.DropForeignKey("dbo.FuelWerxProductSpecificPrices", "ForCountryId", "dbo.FuelWerxCountries", null);
			base.DropIndex("dbo.FuelWerxProductSpecificPrices", new string[] { "ForCustomerId" }, null);
			base.DropIndex("dbo.FuelWerxProductSpecificPrices", new string[] { "ForOrganizationalUnitId" }, null);
			base.DropIndex("dbo.FuelWerxProductSpecificPrices", new string[] { "ForCountryId" }, null);
		}

		public override void Up()
		{
			base.CreateIndex("dbo.FuelWerxProductSpecificPrices", "ForCountryId", false, null, false, null);
			base.CreateIndex("dbo.FuelWerxProductSpecificPrices", "ForOrganizationalUnitId", false, null, false, null);
			base.CreateIndex("dbo.FuelWerxProductSpecificPrices", "ForCustomerId", false, null, false, null);
			base.AddForeignKey("dbo.FuelWerxProductSpecificPrices", "ForCountryId", "dbo.FuelWerxCountries", "Id", false, null, null);
			base.AddForeignKey("dbo.FuelWerxProductSpecificPrices", "ForCustomerId", "dbo.FuelWerxCustomers", "Id", false, null, null);
			base.AddForeignKey("dbo.FuelWerxProductSpecificPrices", "ForOrganizationalUnitId", "dbo.AbpOrganizationUnits", "Id", false, null, null);
		}
	}
}