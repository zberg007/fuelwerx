using EntityFramework.DynamicFilters;
using FuelWerx.EntityFramework;
using System;
using System.Data.Entity;

namespace FuelWerx.Migrations.Seed
{
	public class InitialDbBuilder
	{
		private readonly FuelWerxDbContext _context;

		public InitialDbBuilder(FuelWerxDbContext context)
		{
			this._context = context;
		}

		public void Create()
		{
			this._context.DisableAllFilters();
			(new DefaultEditionCreator(this._context)).Create();
			(new DefaultLanguagesCreator(this._context)).Create();
			(new DefaultTenantRoleAndUserCreator(this._context)).Create();
			(new DefaultSettingsCreator(this._context)).Create();
			(new DefaultCountriesAndCountryRegionsCreator(this._context)).Create();
			(new DefaultUSFIPCodesCreator(this._context)).Create();
			(new DefaultTitlesCreator(this._context)).Create();
			this._context.SaveChanges();
		}
	}
}