using FuelWerx.EntityFramework;
using FuelWerx.Migrations.Seed;
using System;
using System.Data.Entity.Migrations;

namespace FuelWerx.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<FuelWerxDbContext>
	{
		public Configuration()
		{
			base.AutomaticMigrationsEnabled = false;
			base.ContextKey = "FuelWerx";
		}

		protected override void Seed(FuelWerxDbContext context)
		{
			(new InitialDbBuilder(context)).Create();
		}
	}
}