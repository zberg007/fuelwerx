using FuelWerx.Administrative.Zones.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Administrative.Zones.Exporting
{
	public interface IZoneListExcelExporter
	{
		FileDto ExportToFile(List<ZoneListDto> zoneListDtos);
	}
}