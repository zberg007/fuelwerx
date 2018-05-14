using Abp.Domain.Entities;
using Abp.EntityFramework;
using FuelWerx.EntityFramework;
using System;

namespace FuelWerx.EntityFramework.Repositories
{
	public abstract class FuelWerxRepositoryBase<TEntity> : FuelWerxRepositoryBase<TEntity, int>
	where TEntity : class, IEntity<int>
	{
		protected FuelWerxRepositoryBase(IDbContextProvider<FuelWerxDbContext> dbContextProvider) : base(dbContextProvider)
		{
		}
	}
}