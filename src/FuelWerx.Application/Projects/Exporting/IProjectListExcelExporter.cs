using FuelWerx.Dto;
using FuelWerx.Projects.Dto;
using System.Collections.Generic;

namespace FuelWerx.Projects.Exporting
{
	public interface IProjectListExcelExporter
	{
		FileDto ExportToFile(List<ProjectListDto> projectListDtos);
	}
}