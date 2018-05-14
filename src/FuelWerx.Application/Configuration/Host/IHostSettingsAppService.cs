using Abp.Application.Services;
using Abp.Dependency;
using FuelWerx.Configuration.Host.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Configuration.Host
{
	public interface IHostSettingsAppService : IApplicationService, ITransientDependency
	{
		Task<HostSettingsEditDto> GetAllSettings();

		Task UpdateAllSettings(HostSettingsEditDto input);
	}
}