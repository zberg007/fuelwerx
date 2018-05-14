using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using FuelWerx.EntityFramework;
using System;

namespace FuelWerx.EntityFramework.Repositories
{
	public abstract class FuelWerxRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<FuelWerxDbContext, TEntity, TPrimaryKey>
	where TEntity : class, IEntity<TPrimaryKey>
	{
		protected FuelWerxRepositoryBase(IDbContextProvider<FuelWerxDbContext> dbContextProvider) : base(dbContextProvider)
		{
		}
	}
}