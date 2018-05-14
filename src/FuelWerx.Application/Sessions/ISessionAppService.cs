using Abp.Application.Services;
using Abp.Dependency;
using FuelWerx.Sessions.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Sessions
{
	public interface ISessionAppService : IApplicationService, ITransientDependency
	{
		Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
	}
}