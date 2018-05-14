using FuelWerx.Assets.Trucks.Dto;
using FuelWerx.Dto;
using System.Collections.Generic;

namespace FuelWerx.Assets.Trucks.Exporting
{
	public interface ITruckListExcelExporter
	{
		FileDto ExportToFile(List<TruckListDto> truckListDtos);
	}
}