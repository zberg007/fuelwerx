using EntityFramework.Seeder;
using FuelWerx.EntityFramework;
using FuelWerx.Generic;
using System;
using System.Data.Entity;
using System.Linq.Expressions;

namespace FuelWerx.Migrations.Seed
{
	public class DefaultUSFIPCodesCreator
	{
		private readonly FuelWerxDbContext _context;

		public DefaultUSFIPCodesCreator(FuelWerxDbContext context)
		{
			this._context = context;
		}

		public void Create()
		{
			this.CreateDefaultUSFIPCodes();
		}

		private void CreateDefaultUSFIPCodes()
		{
			this._context.FIPs.SeedFromResource<FIP>("FuelWerx.Migrations.Seed.Data.us_fips.csv", (FIP c) => c.FIPsStateCounty, new CsvColumnMapping<FIP>[0]);
			this._context.SaveChanges();
		}
	}
}