using Abp.Domain.Entities;
using EntityFramework.Seeder;
using FuelWerx.EntityFramework;
using FuelWerx.Generic;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations.Seed
{
	public class DefaultCountriesAndCountryRegionsCreator
	{
		private readonly FuelWerxDbContext _context;

		public DefaultCountriesAndCountryRegionsCreator(FuelWerxDbContext context)
		{
			this._context = context;
		}

		public void Create()
		{
			this.CreateCountriesAndCountryRegions();
		}

		private void CreateCountriesAndCountryRegions()
		{
			this._context.Countries.SeedFromResource<Country>("FuelWerx.Migrations.Seed.Data.countries.csv", (Country c) => c.Code, new CsvColumnMapping<Country>[0]);
			this._context.SaveChanges();
			this._context.CountryRegions.SeedFromResource<CountryRegion>("FuelWerx.Migrations.Seed.Data.countryregions.csv", (CountryRegion p) => p.Code, new CsvColumnMapping<CountryRegion>[] { new CsvColumnMapping<CountryRegion>("CountryCode", (CountryRegion state, object countryCode) => {
				state.Country = this._context.Countries.Single<Country>((Country c) => c.Code == countryCode.ToString());
				state.CountryId = state.Country.Id;
			}) });
		}
	}
}