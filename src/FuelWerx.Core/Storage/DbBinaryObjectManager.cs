using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace FuelWerx.Storage
{
	public class DbBinaryObjectManager : IBinaryObjectManager, ITransientDependency
	{
		private readonly IRepository<BinaryObject, Guid> _binaryObjectRepository;

		public DbBinaryObjectManager(IRepository<BinaryObject, Guid> binaryObjectRepository)
		{
			this._binaryObjectRepository = binaryObjectRepository;
		}

		public Task DeleteAsync(Guid id)
		{
			return this._binaryObjectRepository.DeleteAsync(id);
		}

		public Task<BinaryObject> GetOrNullAsync(Guid id)
		{
			return this._binaryObjectRepository.FirstOrDefaultAsync(id);
		}

		public Task SaveAsync(BinaryObject file)
		{
			return this._binaryObjectRepository.InsertAsync(file);
		}
	}
}