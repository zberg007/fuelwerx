using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using FuelWerx.Auditing.Dto;
using FuelWerx.Dto;
using System.Threading.Tasks;

namespace FuelWerx.Auditing
{
	public interface IAuditLogAppService : IApplicationService, ITransientDependency
	{
		Task<PagedResultOutput<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

		Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
	}
}