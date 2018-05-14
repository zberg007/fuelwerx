using FuelWerx.Auditing.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Auditing.Exporting
{
	public interface IAuditLogListExcelExporter
	{
		FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
	}
}