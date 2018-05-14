using Abp.Application.Editions;
using Abp.Zero.EntityFramework;
using FuelWerx.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FuelWerx.Migrations.Seed
{
	public class DefaultEditionCreator
	{
		private readonly FuelWerxDbContext _context;

		public DefaultEditionCreator(FuelWerxDbContext context)
		{
			this._context = context;
		}

		public void Create()
		{
			this.CreateEditions();
		}

		private void CreateEditions()
		{
			if (this._context.Editions.FirstOrDefault<Edition>((Edition e) => e.Name == "Standard") == null)
			{
				Edition edition = new Edition()
				{
					Name = "Standard",
					DisplayName = "Standard"
				};
				this._context.Editions.Add(edition);
				this._context.SaveChanges();
			}
		}
	}
}