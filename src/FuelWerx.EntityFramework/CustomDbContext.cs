using FuelWerx.Storage;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace FuelWerx.EntityFramework
{
	public class CustomDbContext : DbContext
	{
		public virtual IDbSet<BinaryObject> BinaryObjects
		{
			get;
			set;
		}

		public CustomDbContext() : base("Default")
		{
		}

		public CustomDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
		{
			((IObjectContextAdapter)this).ObjectContext.CommandTimeout = new int?(360);
		}

		public CustomDbContext(DbConnection dbConnection) : base(dbConnection, true)
		{
		}
	}
}