using FuelWerx.Administrative.Titles.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Administrative.Titles.Exporting
{
	public interface ITitleListExcelExporter
	{
		FileDto ExportToFile(List<TitleListDto> titleListDtos);
	}
}