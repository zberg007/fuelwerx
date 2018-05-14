using Abp.Timing;
using FuelWerx.Administrative;
using FuelWerx.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FuelWerx.Migrations.Seed
{
	public class DefaultTitlesCreator
	{
		private readonly FuelWerxDbContext _context;

		public DefaultTitlesCreator(FuelWerxDbContext context)
		{
			this._context = context;
		}

		private void AddTitleIfNotExists(string title, int tenantId = 1)
		{
			if (this._context.Titles.Any<Title>((Title s) => s.Name == title && s.TenantId == tenantId))
			{
				return;
			}
			Title title1 = new Title()
			{
				TenantId = tenantId,
				Name = title,
				Type = (title == "Mr." ? "Male" : "Female"),
				IsActive = true,
				CreationTime = Clock.Now
			};
			this._context.Titles.Add(title1);
			this._context.SaveChanges();
		}

		public void Create()
		{
			string[] strArrays = new string[] { "Mr.", "Mrs.", "Ms." };
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				this.AddTitleIfNotExists(strArrays[i], 1);
			}
		}
	}
}